using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDomiItemHandler {
    public DomiItem GetDomiItem();
}

public enum ItemType {
    Unknown,
    Chicken,
    Ingredient,
    Source,
    Drink,
    Other
}

public class DomiItem : ScriptableObject
{
    [SerializeField] ItemType type;
    
    [SerializeField] string id;
    [SerializeField] new string name;
    [SerializeField, TextArea] string description;
    [SerializeField] Sprite image;
    [SerializeField] Sprite image_big;

    public virtual string GetName() => name; // 함수를 바꿔서 (차가운) 후라이드 치킨 <--- 이런식으로 커스텀 가능
    public virtual string GetDesc() => description;
    public ItemType GetItemType() => type;
    public Sprite GetImage(bool big = false) => big ? image_big : image;
    public virtual bool ItemEquals(DomiItem target) => type == target.type;
}