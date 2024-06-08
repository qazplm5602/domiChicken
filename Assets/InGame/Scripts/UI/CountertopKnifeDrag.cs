using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CountertopKnifeDrag : DragRequest
{
    RectTransform iconTrm;

    private new void Awake() {
        base.Awake();
        iconTrm = transform.Find("Icon") as RectTransform;
    }

    protected override RectTransform CreateDragItem()
    {
        return iconTrm;
    }

    protected override void OnDropEvent(bool winBoth, bool result)
    {
        iconTrm.anchoredPosition = Vector2.zero;
    }
}
