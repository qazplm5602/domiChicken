using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientItemType {
    RawChicken,
    Garlic, // 마늘
    Garlic_Slice, // 얇게 썬 마늘
    RiceCake // 떡
}

[CreateAssetMenu(menuName = "SO/Item/Ingredient")]
public class IngredientItem : DomiItem
{
    [field: SerializeField] public IngredientItemType ingredientType { get; private set; }

    public override bool ItemEquals(DomiItem target)
    {
        IngredientItem item = target as IngredientItem;
        if (item == null) return false;

        return ingredientType == item.ingredientType;
    }
}
