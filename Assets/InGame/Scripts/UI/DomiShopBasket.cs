using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DomiShopBasket : MonoBehaviour
{
    [SerializeField] GameObject _mention;
    [SerializeField] Transform _section;
    [SerializeField] GameObject _boxTemplate;
    [SerializeField] TextMeshProUGUI _priceSumT;
    TextMeshProUGUI _mentionT;

    Dictionary<DomiItem, int> baskets;
    Dictionary<DomiItem, Transform> basketUI;
    DomiShopSystem _system;

    public void Init()
    {
        _system = GetComponent<DomiShopSystem>();
    }
 
    private void Awake() {
        baskets = new Dictionary<DomiItem, int>();
        basketUI = new();
        _mentionT = _mention.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Add(DomiItem item) {
        if (!baskets.TryGetValue(item, out var value)) { // 없으면 셋팅
            var box = Instantiate(_boxTemplate, _section).transform;
            basketUI[item] = box;

            box.Find("Icon").Find("Image").GetComponent<Image>().sprite = item.GetImage();
            box.Find("Title").GetComponent<TextMeshProUGUI>().text = item.GetName();
            box.Find("Desc").GetComponent<TextMeshProUGUI>().text = item.GetDesc();
        
            // 버튼
            box.Find("Plus").GetComponent<Button>().onClick.AddListener(() => Add(item));
            box.Find("Minus").GetComponent<Button>().onClick.AddListener(() => Remove(item));
            box.Find("Close").GetComponent<Button>().onClick.AddListener(() => Remove(item, true));
        }

        baskets[item] = value + 1;
        
        UpdateBox(item);
    }

    public void Remove(DomiItem item, bool all = false) {
        if (!baskets.TryGetValue(item, out var value)) return;
        
        if (all || value - 1 <= 0) {
            Destroy(basketUI[item].gameObject);
            basketUI.Remove(item);
            baskets.Remove(item);
            MentionUpdate();
            return;
        }

        baskets[item]--;

        UpdateBox(item);
    }

    void UpdateBox(DomiItem item) {
        MentionUpdate();

        var box = basketUI[item];
        int amount = baskets[item];
        int price = _system.GetPriceItem(item) * amount;
        box.Find("Price").GetComponent<TextMeshProUGUI>().text = $"{price:N0}원";
        box.Find("Amount").GetComponent<TextMeshProUGUI>().text = amount.ToString();
    }
    
    void MentionUpdate() {
        SumUpdate();

        if (baskets.Count == 0) {
            _mention.SetActive(false);
            return;
        }

        _mentionT.text = baskets.Count.ToString();
        _mention.SetActive(true);
    }

    void SumUpdate() {
        int sum = 0;
        foreach (var item in baskets) {
            sum += _system.GetPriceItem(item.Key) * item.Value;
        }

        _priceSumT.text = $"총 금액 {sum:N0}원";
    }
}
