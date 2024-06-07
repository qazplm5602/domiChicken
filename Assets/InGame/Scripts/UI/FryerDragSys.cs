using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FryerDragSys : DragRequest
{
    FryerSystem _system;
    private new void Awake() {
        base.Awake();
        _system = GetComponent<FryerSystem>();
    }

    protected override RectTransform CreateDragItem()
    {
        Sprite icon = _system.GetDomiItem().GetImage();
        
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
        if (!result) return;

        _system.Reset();
    }
}
