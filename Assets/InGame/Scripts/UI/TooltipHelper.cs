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
}
