using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DrinkItemType {
    Water,
    Cola,
    Sidar,
    Fanta // 환타
}

[CreateAssetMenu(menuName = "SO/Item/Drink")]
public class DrinkItem : DomiItem
{
    [field: SerializeField] public DrinkItemType drinkType { get; private set; }

    public override bool ItemEquals(DomiItem target)
    {
        DrinkItem drinkItem = target as DrinkItem;
        if (drinkItem == null) return false;

        return drinkItem.drinkType == drinkType;
    }
}
