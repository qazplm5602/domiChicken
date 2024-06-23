using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

struct RefrigeratorCategory {
    public string title;
    public ItemType item;
}

public class RefrigeratorSystem : MonoSingleton<RefrigeratorSystem>
{
    [SerializeField] List<DomiItem> inventory;
    RefrigeratorCategory[] categories = new RefrigeratorCategory[] {
        new RefrigeratorCategory() { title = "전체", item = ItemType.Unknown },
        new RefrigeratorCategory() { title = "닭", item = ItemType.Ingredient },
        new RefrigeratorCategory() { title = "재료", item = ItemType.Ingredient },
        new RefrigeratorCategory() { title = "소스", item = ItemType.Source },
        new RefrigeratorCategory() { title = "음료", item = ItemType.Drink },
        new RefrigeratorCategory() { title = "기타", item = ItemType.Other },
    };

    [Header("Category")]
    [SerializeField] Transform categorySection;
    [SerializeField] Button categoryTempleate;
    int currnetCategoryIdx = 0;

    [SerializeField] Transform content;
    [SerializeField] GameObject contentBox;

    // 소스만 위한 것 ㄹㅇ
    public bool sourceUseDropped = false;

    private void Awake() {
        // SO 복사
        if (inventory != null) {
            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i] = Instantiate(inventory[i]); // 복사
            }
        }
        
        int v = 0;
        foreach (var item in categories)
        {
            int idx = v;
            var button = Instantiate(categoryTempleate, categorySection);
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.title;
            button.onClick.AddListener(() => ShowContent(idx));
            v++;
        }
        
        ShowContent(0);
    }

    public void ClearContent() {
        for (int i = 0; i < content.childCount; i++)
            Destroy(content.GetChild(i).gameObject);
    }

    public void ShowContent(int categoryIdx) {
        ClearContent();
        
        // 카테고리 버튼 색상 바꾸기
        CategoryActive(currnetCategoryIdx, false);
        currnetCategoryIdx = categoryIdx;
        CategoryActive(currnetCategoryIdx, true);

        DomiItem[] renderItems;
        RefrigeratorCategory category = categories[categoryIdx];
        
        if (category.item == ItemType.Ingredient) {
            renderItems = inventory.Where(v => v.GetItemType() == category.item && ((category.title == "닭") == ((v as IngredientItem).ingredientType == IngredientItemType.RawChicken))).ToArray();
        } else if (category.item != ItemType.Unknown) {
            renderItems = inventory.Where(v => v.GetItemType() == category.item).ToArray();
        } else {
            renderItems = inventory.ToArray();
        }

        if (renderItems == null) return;

        foreach (var item in renderItems)
        {
            CreateItem(item);   
        }
    }

    void CreateItem(DomiItem item) {
        var box = Instantiate(contentBox, content);
        box.GetComponent<RefrigeratorDrag>().Init(this, item);
        box.transform.Find("Icon").GetComponent<Image>().sprite = item.GetImage();
        
        // 만약 소스일 경우에
        if (item.GetItemType() == ItemType.Source) {
            SourceItem source = item as SourceItem;
            
            // 바 크기 구함
            float barSize = (float)source.GetCurrentSize() / source.GetMaxSize();
            
            var barTrm = box.transform.Find("Bar");
            barTrm.gameObject.SetActive(true);
            barTrm.Find("BarIn").localScale = new Vector3(barSize, 1, 1);            
        }
    }

    void CategoryActive(int idx, bool active) {
        var btnTrm = categorySection.GetChild(idx);
        btnTrm.GetComponent<Image>().color = active ? Color.black : new Color32(228,228,228,255);
        btnTrm.GetComponentInChildren<TextMeshProUGUI>().color = active ? Color.white : Color.black;
    }

    public void GiveInventory(DomiItem item) {
        inventory.Add(item);
        ShowContent(currnetCategoryIdx);
    }

    public void RemoveItem(DomiItem domiItem)
    {
        inventory.Remove(domiItem);
        ShowContent(currnetCategoryIdx);
    }
}