using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestItemDragRecevie : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        print("Recevie DROP!");
        // print(eventData.pointerEnter.gameObject);
        print(eventData.pointerDrag.gameObject); // 드래그 중인 target
        // print(eventData.pointerCurrentRaycast.gameObject);
    }
}
