using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OtherItemType {
    Knife,
}

[CreateAssetMenu(menuName = "SO/Item/Other")]
public class OtherItem : DomiItem
{
    [field: SerializeField] public OtherItemType otherType { get; private set; }

    public override bool ItemEquals(DomiItem target)
    {
        OtherItem item = target as OtherItem;
        if (item == null) return false;

        return otherType == item.otherType;
    }
}
