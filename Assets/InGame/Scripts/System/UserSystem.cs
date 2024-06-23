using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

struct OrderData {
    public Dictionary<DomiItem, int> items;
    public int price;
    public float created;
}

public class UserSystem : MonoSingleton<UserSystem>
{
    [SerializeField] DeliverySystem delivery;
    [SerializeField] AssessSystem assess;

    // 아이템
    [SerializeField] ChickenSellSO chickenSell; // 치킨ㄴ
    [SerializeField] DrinkSellSO drinkSell; // 음료수.
    [SerializeField] OtherSellSO otherSell; // 무, 치즈볼 같은것

    Dictionary<int, OrderData> orderList = new();


    bool isWaitOrder = true; // 주문 받습니다.ㅇ=
    float nextCallTime = 0;

    private void Start() {
        // CallOrder();
    }

    private void Update() {
        if (!isWaitOrder) return;
        
        if (nextCallTime <= 0) {
            CallOrder();

            nextCallTime = Random.Range(0, 10);
            // nextCallTime = 0.2f;
        } else nextCallTime -= Time.deltaTime;
    }
    
    // 주문 ㄱㄱ
    [SerializeField] string[] placeNames;
    [SerializeField] string[] placeTypes;
    [SerializeField] string[] firstNames;
    [SerializeField] string[] lastNames;
    public void CallOrder() {
        int ID = Random.Range(100000, 999999);
        string address = $"{placeNames[Random.Range(0, placeNames.Length)]} {placeTypes[Random.Range(0, placeTypes.Length)]} {Random.Range(100, 999)}동 {Random.Range(100, 999)}동";
        // string name = $"{firstNames[Random.Range(0, firstNames.Length)]}{lastNames[Random.Range(0, lastNames.Length)]}";
        Dictionary<DomiItem, int> orderItems = new();

        int price = 0;
        bool isBig = Random.Range(0, 6) == Random.Range(0, 6); // 대용량으로 많이 시킬건가?? (아니면 1~3개만 시킴 대량이면 10 ~ 20)
        
        // 치킨 종류 정하기
        int chickenTypeAmount = isBig ? Random.Range(1, chickenSell.items.Length) : (Random.Range(0, 1) == Random.Range(0, 1) ? Random.Range(1, 3) : 1);
        
        List<ItemSellData<ChickenItem>> chickens = chickenSell.items.ToList();
        ArrayRandomSort(chickens);

        for (int i = 0; i < chickenTypeAmount; i++) {
            int amount = Random.Range(1, isBig ? 5 : 3);

            orderItems[chickens[i].item] = amount;
            price += chickens[i].price * amount;
        }

        // 음료수 종류 (선택)
        int drinkTypeAmount = isBig ? Random.Range(0, drinkSell.items.Length) : Random.Range(0, 3);

        List<ItemSellData<DrinkItem>> drinks = drinkSell.items.ToList();
        ArrayRandomSort(drinks);
        
        for (int i = 0; i < drinkTypeAmount; i++) {
            int amount = Random.Range(1, isBig ? 5 : 3);

            orderItems[drinks[i].item] = Random.Range(1, isBig ? 10 : 3);
            price += drinks[i].price * amount;
        }

        orderList[ID] = new OrderData() { items = orderItems, price = price, created = Time.time };
        delivery.Add(address, 0, price, orderItems, (List<DomiItem> e) => CheckFood(ID, e));
    }

    void ArrayRandomSort<T>(List<T> list) {
        for (int i = 0; i < 10; i++)
        {
            int idx1 = Random.Range(0, list.Count);
            int idx2 = Random.Range(0, list.Count);

            (list[idx1], list[idx2]) = (list[idx2], list[idx1]);
        }
    }
    
    string[] goodWords = new string[] {
        "아니",
        "근데",
        "진짜",
        "이거"
    };

    void CheckFood(int orderId, List<DomiItem> items) {
        if (!orderList.TryGetValue(orderId, out var orderData)) return;

        Dictionary<DomiItem, int> indexingItem = new();
        foreach (var item in items)
        {
            int amount = -1;
            DomiItem indexItem = null;

            foreach (var item2 in indexingItem)
            {
                if (item.ItemEquals(item2.Key)) {
                    amount = item2.Value;
                    indexItem = item2.Key;
                    break;
                }
            }

            if (indexItem) {
                indexingItem[indexItem] = amount + 1;
            } else {
                indexingItem[item] = 1;
            }
        }
    
        // 포함하지 않은 음식
        List<DomiItem> excludeItems = new();

        // 시키지도 않았는데 있는 으ㅁ시ㄱ
        List<DomiItem> overItems = new();
        
        // 갯수가 틀린 음식 (차이)
        Dictionary<DomiItem, int> wrongItems = new();

        // 포함하지 않은 음식 / 갯수 틀린 음식
        foreach (var item in orderData.items)
        {
            bool include = false;
            int amountDiff = 0;
            foreach (var item2 in indexingItem)
            {
                if (item.Key.ItemEquals(item2.Key)) {
                    include = true;
                    amountDiff = item.Value - item2.Value;
                    break;
                }
            }

            if (!include) {
                excludeItems.Add(item.Key);
                continue;
            }

            if (amountDiff != 0) {
                wrongItems[item.Key] = amountDiff;
            }
        }

        print("------------- orderData.items");
        foreach (var item in orderData.items)
        {
            print("item: " + item.Key.GetName() + " " + item.Value);
        }

        print("------------- indexingItem");
        foreach (var item in indexingItem)
        {
            print("item: " + item.Key.GetName() + " " + item.Value);
        }

        print("------------- list<domiItme>");
        foreach (var item in items)
        {
            print("item: " + item.GetName());
        }
        
        // 시키지 않은 음식 확인
        foreach (var item in indexingItem) {
            bool include = false;
            foreach (var item2 in orderData.items) {
                if (item.Key.ItemEquals(item2.Key)) {
                    include = true;
                    break;
                }
            }

            if (!include) {
                overItems.Add(item.Key);
            }
        }
    
        float score = 5;
        score = Mathf.Clamp(score, 1, 5);
        StringBuilder content = new StringBuilder();
        

        if (excludeItems.Count > 0) {
            content.Append(goodWords[Random.Range(0, goodWords.Length)]).Append(" ");
            
            if (excludeItems.Count > 1) {
                score = 1;

                content.Append(System.String.Join(", ", excludeItems.Select(v => v.GetName())));
                content.Append("가 안왔는데 ㄹㅇ 너무 하네.");
            } else {
                score -= 1;
                content.Append(excludeItems[0].GetName());
                content.Append("가 안왔지만 솔직히 봐준다.");
            }

            content.Append(" ");
        }

        bool wrongTextStart = false;
        foreach (var item in wrongItems)
        {
            if (item.Value > 0) {
                score -= 0.5f;

                if (!wrongTextStart) {
                    wrongTextStart = true;
                    content.Append(content.Length > 0 ? "그리고" : "근데");
                }
                 
                content.Append(" ");
                content.Append(item.Key.GetName());
                content.Append(" ");
                content.Append($"{item.Value}개");
            }
        }

        if (wrongTextStart) {
            content.Append(" 가 빠졋네용.");
        }

        assess.Add(new AssessData() {
            content = content.ToString(),
            name = firstNames[Random.Range(0, firstNames.Length)] + lastNames[Random.Range(0, lastNames.Length)],
            score = score
        });

        MoneySystem.Instance.GiveMoney(orderData.price);

        // 마무리
        orderList.Remove(orderId);
    }
}
