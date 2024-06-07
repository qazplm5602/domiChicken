using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Unknown,
    Chicken,
    Ingredient,
    Source,
    Other
}

public class DomiItem : ScriptableObject
{
    [SerializeField] ItemType type;
    
    [SerializeField] string id;
    [SerializeField] new string name;
    [SerializeField] string description;
    [SerializeField] Sprite image;

    public virtual string GetName() => name; // 함수를 바꿔서 (차가운) 후라이드 치킨 <--- 이런식으로 커스텀 가능
    public ItemType GetItemType() => type;
    public Sprite GetImage() => image;
}