using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropResponse : MonoBehaviour, IDropHandler
{
    [SerializeField] WindowType windowType;

    protected virtual bool RequestItemDrop(bool winBoth, DragRequest dragRequest) {
        print($"OnDropEvent / winBoth: {winBoth} / dragRequest: {dragRequest}");
        return false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        print($"OnDrop {gameObject}");
        bool hasRequest = eventData.pointerDrag.TryGetComponent<DragRequest>(out var request);
        if (!hasRequest) return;

        bool result = RequestItemDrop(request.GetWindowType() == windowType, request);

        DragDropManager.Instance.SetResponseData(windowType, result);
    }
}
