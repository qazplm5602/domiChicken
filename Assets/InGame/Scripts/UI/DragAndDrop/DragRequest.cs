using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragRequest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] WindowType windowType;

    protected RectTransform dragItem = null;
    Canvas _canvas;
    
    protected void Awake() {
        _canvas = FindObjectOfType<Canvas>();
    }

    public WindowType GetWindowType() => windowType;

    protected virtual RectTransform CreateDragItem() => transform as RectTransform;
    
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        dragItem = CreateDragItem();
        DragDropManager.Instance.SetResponseData(WindowType.None, false); // 초기화
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragItem.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DropResponseData data = DragDropManager.Instance.GetResponseData();
        OnDropEvent(data.window == windowType, data.result);

        dragItem = null;
    }

    protected virtual void OnDropEvent(bool winBoth, bool result) {
        print($"OnDropEvent / winBoth: {winBoth} / result: {result}");
    }
}
