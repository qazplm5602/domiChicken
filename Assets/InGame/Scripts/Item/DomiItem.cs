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
}