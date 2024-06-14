using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    IDomiItemHandler itemHandler;
    private void Awake() {
        itemHandler = GetComponent<IDomiItemHandler>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var item = itemHandler.GetDomiItem();
        if (item == null) return;

        TooltipManager.Instance.Show(gameObject, item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!itemHandler.GetDomiItem()) return;
        TooltipManager.Instance.Hide(gameObject);
    }

    public void Refresh() {
        if (TooltipManager.Instance.GetTarget() != gameObject) return;
        OnPointerEnter(null); // 마우스 올린척
    }

    public void Remove()
    {
        if (TooltipManager.Instance.GetTarget() == gameObject)
            TooltipManager.Instance.Hide(gameObject);
    }
}
