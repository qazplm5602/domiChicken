using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SourceItemType {
    Soy, // 간장
    Seasoned, // 양념
}

[CreateAssetMenu(menuName = "SO/Item/Source")]
public class SourceItem : DomiItem
{
    [field: SerializeField] SourceItemType sourceType;
    [SerializeField] int maxSize;
    int currentSize;

    private void OnEnable() {
        currentSize = maxSize;
    }

    int GetMaxSize() => maxSize;
    int GetCurrentSize() => currentSize;

    public override bool ItemEquals(DomiItem target)
    {
        SourceItem source = target as SourceItem;
        if (source == null) return false;

        return sourceType == source.sourceType;
    }
}