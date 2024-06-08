using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CountertopTableDrag : DragRequest
{
    CountertopTableSystem _system;

    private new void Awake() {
        base.Awake();
        _system = GetComponent<CountertopTableSystem>();
    }

    protected override RectTransform CreateDragItem()
    {
        Sprite icon = _system.GetDomiItem()?.GetImage();
        if (icon == null) return null;
        
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
        if (dragItem)
            Destroy(dragItem.gameObject);

        if (!result) return;

        _system.Remove();
    }
}
