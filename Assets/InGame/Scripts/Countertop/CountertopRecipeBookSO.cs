using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CountertopRecipe
{
    [field: SerializeField] public DomiItem item1 { get; private set; }
    [field: SerializeField] public DomiItem item2 { get; private set; }
    [field: SerializeField] public DomiItem finalItem { get; private set; }
}

[CreateAssetMenu(menuName = "SO/Recipe")]
public class CountertopRecipeBookSO : ScriptableObject
{
    public CountertopRecipe[] recipes;
}