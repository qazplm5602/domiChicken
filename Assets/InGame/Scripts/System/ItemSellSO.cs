using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemSellData<T> {
    public T item;
    public int price;
}


public class ItemSellSO<T> : ScriptableObject where T : DomiItem
{
    public ItemSellData<T>[] items;
}