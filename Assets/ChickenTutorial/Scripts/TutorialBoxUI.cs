using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TutorialScreen {
    public string title;
    public RectTransform transform;
}

public class TutorialBoxUI : MonoBehaviour
{
    [SerializeField] RectTransform _screen;
    [SerializeField] TutorialScreen[] list;
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _pageT;
    [SerializeField] Transform circleList;
    [SerializeField] GameObject circle;

    int page = -1;
    Sequence sequence;

    private void Awake() {
        for (int i = 0; i < list.Length; i++)
            Instantiate(circle, circleList);
    }

    private void Start() {
        SetPage(true);
    }

    public void SetPage(bool next) {
        if ((!next && page == 0) || (next && page >= list.Length - 1)) return;

        if (sequence != null) {
            sequence.Kill();
            sequence.onComplete?.Invoke();
        }

        sequence = DOTween.Sequence();

        if (page != -1) {
            TutorialScreen nowScreen = list[page];
            ScreenHide(nowScreen.transform, next);
            CircleWidth(page, false);
            sequence.onComplete += () => {
                nowScreen.transform.gameObject.SetActive(false);
            };
        }

        page += 1 * (next ? 1 : -1);
        
        TutorialScreen nextScreen = list[page];
        _title.text = nextScreen.title;
        _pageT.text = $"{page + 1} 페이지";

        CircleWidth(page, true);
        var canvasGroup2 = nextScreen.transform.GetComponent<CanvasGroup>();
        nextScreen.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        nextScreen.transform.anchoredPosition = new Vector2(next ? 200 : -200, 0);
        canvasGroup2.alpha = 0;

        nextScreen.transform.gameObject.SetActive(true);
        sequence.Join(nextScreen.transform.DOScale(Vector3.one, 0.3f));
        sequence.Join(nextScreen.transform.DOAnchorPosX(0, 0.3f));
        sequence.Join(canvasGroup2.DOFade(1, 0.3f));
    }

    void ScreenHide(RectTransform transform, bool left) {
        var canvasGroup = transform.GetComponent<CanvasGroup>();
        sequence.Join(transform.DOScale(new Vector3(0.9f, 0.9f, 1), 0.3f));
        sequence.Join(transform.DOAnchorPosX(left ? -200 : 200, 0.3f));
        sequence.Join(canvasGroup.DOFade(0, 0.3f));
    }
    
    void CircleWidth(int idx, bool width) {
        RectTransform trm = circleList.GetChild(idx) as RectTransform;

        trm.DOKill();
        trm.DOSizeDelta(new Vector2(width ? 60 : 15, 15), 0.3f);
    }

    public void Show() {
        _screen.DOAnchorPosY(0, 0.5f);
    }
}
