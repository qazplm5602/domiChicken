using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

struct OrderData {
    public Dictionary<DomiItem, int> items;
    public int price;
}

public class UserSystem : MonoSingleton<UserSystem>
{
    [SerializeField] DeliverySystem delivery;
    [SerializeField] AssessSystem assess;

    // 아이템
    [SerializeField] ChickenSellSO chickenSell; // 치킨ㄴ
    [SerializeField] DrinkSellSO drinkSell; // 음료수.
    [SerializeField] OtherSellSO otherSell; // 무, 치즈볼 같은것

    Dictionary<int, OrderData> orderList;


    bool isWaitOrder = true; // 주문 받습니다.ㅇ=
    float nextCallTime = 0;

    private void Start() {
        CallOrder();
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

        orderList[ID] = new OrderData() { items = orderItems, price = price };
        delivery.Add(address, 0, price, orderItems, (List<DomiItem> e) => {});
    }

    void ArrayRandomSort<T>(List<T> list) {
        for (int i = 0; i < 10; i++)
        {
            int idx1 = Random.Range(0, list.Count);
            int idx2 = Random.Range(0, list.Count);

            (list[idx1], list[idx2]) = (list[idx2], list[idx1]);
        }
    }
}
