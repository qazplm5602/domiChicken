using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSystem : MonoSingleton<UserSystem>
{
    [SerializeField] DeliverySystem delivery;
    [SerializeField] AssessSystem assess;

    // 아이템
    [SerializeField] ChickenItem[] chickenItems; // 치킨ㄴ
    [SerializeField] DrinkItem[] drinkItems; // 음료수.
    [SerializeField] OtherItem[] otherItems; // 무, 치즈볼 같은것


    bool isWaitOrder = true; // 주문 받습니다.ㅇ=

    private void Update() {
        
    }
    
    // 주문 ㄱㄱ
    [SerializeField] string[] placeNames;
    [SerializeField] string[] placeTypes;
    [SerializeField] string[] firstNames;
    [SerializeField] string[] lastNames;
    public void CallOrder() {
        string address = $"{placeNames[Random.Range(0, placeNames.Length)]} {placeTypes[Random.Range(0, placeTypes.Length)]} {Random.Range(100, 999)}동 {Random.Range(100, 999)}동";
        // string name = $"{firstNames[Random.Range(0, firstNames.Length)]}{lastNames[Random.Range(0, lastNames.Length)]}";
        
        bool isBig = Random.Range(0, 6) == Random.Range(0, 6); // 대용량으로 많이 시킬건가?? (아니면 1~3개만 시킴 대량이면 10 ~ 20)
        
        // 치킨 정하기
        for (int i = 0; i < Random.Range(isBig ? 5 : 0, isBig ? 30 : 5); i++)
        {
            
        }

        // delivery.Add(address, 0, );
    }
}
