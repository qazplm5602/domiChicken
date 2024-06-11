using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct DomiShopItem {
    public DomiItem item;
    public int price;    
}

[System.Serializable]
public struct DomiShopMenu {
    public string name;
    public DomiShopItem[] sellItems;
}

public class DomiShopSystem : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] DomiShopMenu[] menus;

    [Space]
    [SerializeField] Transform menuSection;
    [SerializeField] Button menuTemplate;

    [Space]
    [SerializeField] Transform itemSection;
    [SerializeField] GameObject itemBox;

    [Space]
    [SerializeField] GameObject itemListSection;
    [SerializeField] GameObject basketScreen;
    [SerializeField] Button basketBtn;

    DomiShopBasket basket;
    Dictionary<DomiItem, int> cachePrice;
    
    private void Awake() {
        basket = GetComponent<DomiShopBasket>();
        basket.Init();

        basketBtn.onClick.AddListener(OpenBasket);
        
        cachePrice = new();

        int i = 0;
        foreach (var item in menus) {
            foreach (var item2 in item.sellItems) {
                cachePrice[item2.item] = item2.price;
            }

            // 메뉴 추가
            int idx = i;
            var menu = Instantiate(menuTemplate, menuSection);
            menu.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
            menu.onClick.AddListener(() => Open(idx));

            i++;
        }

        Open(0);
    }

    private void Start() {
        // TEST
        // foreach (var item in menus)
        //     foreach (var item2 in item.sellItems) {
        //         basket.Add(item2.item);
        //         basket.Add(item2.item);
        //         basket.Add(item2.item);
        //         basket.Add(item2.item);
        //         basket.Add(item2.item);
        //         basket.Add(item2.item);
        //         basket.Add(item2.item);
        //         basket.Add(item2.item);
        //         basket.Add(item2.item);
        //         basket.Add(item2.item);
        //     }
    }

    public int GetPriceItem(DomiItem item) {
        return cachePrice[item];
    }

    public void Open(int idx) {
        itemListSection.SetActive(true);
        basketScreen.SetActive(false);

        // 클리어
        for (int i = 0; i < itemSection.childCount; i++)
            Destroy(itemSection.GetChild(i).gameObject);

        foreach (var data in menus[idx].sellItems)
        {
            var box = Instantiate(itemBox, itemSection).transform;
            box.Find("IconContainer/Image").GetComponent<Image>().sprite = data.item.GetImage();
            box.Find("Title").GetComponent<TextMeshProUGUI>().text = data.item.GetName();
            box.Find("Desc").GetComponent<TextMeshProUGUI>().text = data.item.GetDesc();
            box.Find("Button/Price").GetComponent<TextMeshProUGUI>().text = $"{data.price:N0}";
            
            // 버튼
            box.Find("Button").GetComponent<Button>().onClick.AddListener(() => basket.Add(data.item));
        }
    }

    private void OpenBasket()
    {
        itemListSection.SetActive(false);
        basketScreen.SetActive(true);
    }
}
