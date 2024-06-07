using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RefrigeratorDrag : DragRequest, IDomiItemHandler
{
    DomiItem domiItem;
    RefrigeratorSystem _manager;
    public DomiItem GetDomiItem() => domiItem;

    public void Init(RefrigeratorSystem manager, DomiItem item) {
        domiItem = item; // 아이템 저장
        _manager = manager;
    }

    protected override RectTransform CreateDragItem()
    {
        Sprite icon = transform.Find("Icon").GetComponent<Image>().sprite;
        
        var dragObj = new GameObject("drag");
        var dragTrm = dragObj.AddComponent<RectTransform>();
        // var dragTrm = Instantiate(new GameObject("drag"));

        var dragImg = dragTrm.AddComponent<Image>();
        dragImg.sprite = icon;
        dragImg.raycastTarget = false;

        dragTrm.SetParent(transform.root);
        dragTrm.position = Input.mousePosition;
        return dragTrm;
    }

    protected override void OnDropEvent(bool winBoth, bool result)
    {
        Destroy(dragItem.gameObject);
        print($"OnDropEvent {winBoth} {result}");
        if (winBoth || !result) return; // 같은건 안받슴니다.
        
        _manager.RemoveItem(domiItem);
    }
}
