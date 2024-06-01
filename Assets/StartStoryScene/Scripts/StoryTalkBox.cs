using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class StoryTalkBox : MonoSingleton<StoryTalkBox>
{
    public enum Animation { None, Fade, Left, Right }

    [SerializeField] RectTransform _screen;
    [SerializeField] TextMeshProUGUI username;
    [SerializeField] TextMeshProUGUI job;
    [SerializeField] TextMeshProUGUI content;

    CanvasGroup _group;
    public bool IsPlaying {
        get => sequence != null;
    }
    public bool IsTalking {
        get => processContent != null;
    }
    Sequence sequence;

    Coroutine processContent;
    string lastContent;
    
    private void Awake() {
        _screen = transform as RectTransform;
        _group = _screen.GetComponent<CanvasGroup>();
    }

    private void Start() {
        
    }

    public void Reset() {
        _group.alpha = 0;
        _screen.anchoredPosition = new Vector2(-329, 52);
    }

    public void SetTalk(Animation anim, string name, string job, string content) {
        sequence = DOTween.Sequence();
        
        switch (anim)
        {
            case Animation.Fade:
                sequence.Append(_group.DOFade(1, .5f));
                break;
            case Animation.Left:
            case Animation.Right:
                sequence.Append(_screen.DOAnchorPosX(anim == Animation.Left ? -800 : -329, .5f).SetEase(Ease.OutQuart));
                break;
            default:
                break;
        }
        sequence.OnComplete(() => sequence = null);
        
        username.text = name;
        this.job.text = job;
        this.content.text = "";
        processContent = StartCoroutine(WriteContent(content));
    }

    public void CloseTalk(Animation anim) {
        sequence = DOTween.Sequence();
        
        switch (anim)
        {
            case Animation.Fade:
                sequence.Append(_group.DOFade(0, .5f));
                break;
            default:
                break;
        }
        sequence.OnComplete(() => {
            sequence = null;
            Reset();
        });
    }

    public void ImmediatelyStopTalk() {
        StopCoroutine(processContent);
        processContent = null;
        content.text = lastContent;
    }

    IEnumerator WriteContent(string content) {
        lastContent = content;
        foreach (var item in content)
        {
            this.content.text += item;
            yield return new WaitForSeconds(0.1f);
        }
        
        processContent = null;
    }
}