using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

struct RefrigeratorCategory {
    public string title;
    public ItemType item;
}

public class RefrigeratorSystem : MonoBehaviour
{
    [SerializeField] List<DomiItem> inventory;
    RefrigeratorCategory[] categories = new RefrigeratorCategory[] {
        new RefrigeratorCategory() { title = "전체", item = ItemType.Unknown },
        new RefrigeratorCategory() { title = "닭", item = ItemType.Ingredient },
        new RefrigeratorCategory() { title = "재료", item = ItemType.Ingredient },
        new RefrigeratorCategory() { title = "소스", item = ItemType.Source },
        new RefrigeratorCategory() { title = "기타", item = ItemType.Other },
    };

    [Header("Category")]
    [SerializeField] Transform categorySection;
    [SerializeField] Button categoryTempleate;

    private void Awake() {
        // SO 복사
        if (inventory != null) {
            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i] = Instantiate(inventory[i]); // 복사
            }
        }
        
        
    }



}