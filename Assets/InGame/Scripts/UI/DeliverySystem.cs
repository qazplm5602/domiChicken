using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// struct DeliveryData {
//     public Transform box;
//     public System.Action<DomiItem>
// }

public class DeliverySystem : MonoBehaviour
{
    [SerializeField] GameObject boxTemplate;
    [SerializeField] GameObject itemTemplate;

    [SerializeField] Transform section;

    private void Awake() {
        // Add("밍", 1000, 99999, new Dictionary<DomiItem, int>() {  }, (List<DomiItem> items) => print("헉 " + items));
    }

    // 주문 들어옴
    public Transform Add(string address, int time, int price, Dictionary<DomiItem, int> items, System.Action<List<DomiItem>> cb) {
        var box = Instantiate(boxTemplate, section).transform;
        box.GetComponentInChildren<DeliveryDrop>().Init(cb);

        box.Find("Info/Address").GetComponent<TextMeshProUGUI>().text = address;
        box.Find("Info/Time").GetComponent<TextMeshProUGUI>().text = $"주문시각: {time}";
        box.Find("Price").GetComponent<TextMeshProUGUI>().text = $"{price:N0}";

        
        var itemSection = box.Find("Items");
        foreach (var item in items)
        {
            var box2 = Instantiate(itemTemplate, itemSection).transform;

            box2.Find("Icon").GetComponent<Image>().sprite = item.Key.GetImage();
            box2.Find("Amount").GetComponentInChildren<TextMeshProUGUI>().text = item.Value.ToString();
        }

        return box;
    }
}
