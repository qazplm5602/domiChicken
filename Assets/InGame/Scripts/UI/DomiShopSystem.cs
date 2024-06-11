using System.Collections;
using System.Collections.Generic;
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
    
    DomiShopBasket basket;
    Dictionary<DomiItem, int> cachePrice;
    
    private void Awake() {
        basket = GetComponent<DomiShopBasket>();
        basket.Init();
        
        cachePrice = new();

        foreach (var item in menus)
            foreach (var item2 in item.sellItems) {
                cachePrice[item2.item] = item2.price;
            }
    }

    private void Start() {
        // TEST
        foreach (var item in menus)
            foreach (var item2 in item.sellItems) {
                basket.Add(item2.item);
                basket.Add(item2.item);
                basket.Add(item2.item);
                basket.Add(item2.item);
                basket.Add(item2.item);
                basket.Add(item2.item);
                basket.Add(item2.item);
                basket.Add(item2.item);
                basket.Add(item2.item);
                basket.Add(item2.item);
            }
    }

    public int GetPriceItem(DomiItem item) {
        return cachePrice[item];
    }
}
