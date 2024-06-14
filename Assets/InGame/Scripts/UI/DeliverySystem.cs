using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Add("밍", 1000, 99999, new List<DomiItem>() { }, (List<DomiItem> items) => print("헉 " + items));
    }

    // 주문 들어옴
    public void Add(string address, int time, int price, List<DomiItem> items, System.Action<List<DomiItem>> cb) {
        var box = Instantiate(boxTemplate, section);
        box.GetComponentInChildren<DeliveryDrop>().Init(cb);
    }
}
