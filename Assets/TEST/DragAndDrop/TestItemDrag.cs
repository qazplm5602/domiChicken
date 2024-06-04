using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Image image;
    Vector2 originPos;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("OnBeginDrag");
        image.raycastTarget = false; // 무조건 해야함
        originPos = (transform as RectTransform).anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // (trnasform as RectTransform).anchoredPosition += eventData.delta / 
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        (transform as RectTransform).anchoredPosition = originPos;
    }
}
