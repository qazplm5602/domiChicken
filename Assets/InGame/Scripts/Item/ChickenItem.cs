using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChickenItemType {
    Default,
    Seasoned, // 양념
    SeasonedHalfDefault, // 양념 반 후라이드
    Soy, // 간장
    Soy_RiceCake, // 간장 & 떡
    Garlic, // 마늘
    Garlic_RiceCake, // 마늘 & 떡
    WTF // 탄 치킨
}

[CreateAssetMenu(menuName = "SO/Item/Chicken")]
public class ChickenItem : DomiItem
{
    [SerializeField] ChickenItemType chickenType;
    
    public ChickenItemType GetChickenType() => chickenType;

    public override bool ItemEquals(DomiItem target)
    {
        ChickenItem item = target as ChickenItem;
        if (item == null) return false;

        return chickenType == item.chickenType;
    }
}
