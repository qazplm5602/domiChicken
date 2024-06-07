using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChickenItemType {
    Default,
    Seasoned, // 양념
    Soy, // 간장
    Soy_RiceCake, // 간장 & 떡
    WTF // 탄 치킨
}

[CreateAssetMenu(menuName = "SO/Item/Chicken")]
public class ChickenItem : DomiItem
{
    [SerializeField] ChickenItemType chickenType;
    
    public ChickenItemType GetChickenType() => chickenType;
}
