using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoSingleton<TooltipManager>
{
    [SerializeField] GameObject main;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI desc;

    CanvasGroup _group;
    GameObject requestTarget = null;

    private void Awake() {
        _group = main.GetComponent<CanvasGroup>();
    }

    private void Update() { // 어차피 active 끄면 안함 ㅅㄱ
        main.transform.position = Input.mousePosition;
    }
    
    public void Show(GameObject id, DomiItem item) {
        requestTarget = id;
        
        title.text = item.GetName();
        desc.text = item.GetDesc();
    
        // 레전드 코드
        title.GetComponent<ContentSizeFitter>().SetLayoutVertical();
        desc.GetComponent<ContentSizeFitter>().SetLayoutVertical();
        main.GetComponent<VerticalLayoutGroup>().CalculateLayoutInputVertical();
        main.GetComponent<ContentSizeFitter>().SetLayoutVertical();

        main.SetActive(true);
        _group.DOKill();
        _group.DOFade(1, 0.2f);
    }

    public void Hide(GameObject id) {
        if (requestTarget != id) return;

        requestTarget = null;

        _group.DOKill();
        _group.DOFade(0, 0.2f).OnComplete(() => main.SetActive(false));
    }

    public GameObject GetTarget() => requestTarget;
}
