using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StorySceneComputer : StoryScene
{
    [SerializeField] RectTransform _shiroko;
    [SerializeField] Image _computerBG;
    
    [SerializeField] RectTransform mainImage1;
    [SerializeField] RectTransform mainImage2;
    [SerializeField] RectTransform _screenBlack;

    Image shiroko_image;

    Image mainImage1_img;
    Image mainImage2_img;
    Image blackImage;

    
    Sequence sequence;

    int part = 0;

    private void Awake() {
        mainImage1_img = mainImage1.GetComponent<Image>();
        mainImage2_img = mainImage2.GetComponent<Image>();
        shiroko_image = _shiroko.GetComponent<Image>();
        blackImage = _screenBlack.GetComponent<Image>();
    }

    public override void StartScene()
    {
        StoryTalkBox.Instance.CloseTalk(StoryTalkBox.Animation.Fade);
        _shiroko.DOJumpAnchorPos(_shiroko.anchoredPosition, -150, 1, 0.8f).OnComplete(() => {
            part = 1;
            _computerBG.DOFade(1, 0.5f).OnComplete(() => {
                part = 2;
                StoryTalkBox.Instance.SetTalk(StoryTalkBox.Animation.Fade, "시로코", "시스템 엔지니어", "메일을 확인해볼까.");
            });
        });
    }
    
    public override void InteractScene()
    {
        if (part == 2 && !StoryTalkBox.Instance.IsPlaying) {
            part = 3;

            mainImage1.localScale = new Vector3(0.8f, 0.8f, 1);
            mainImage1_img.color = new Color(1, 1, 1, 0);
            
            sequence = DOTween.Sequence();
            sequence.Join(mainImage1.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuart));
            sequence.Join(mainImage1_img.DOFade(1, 0.3f).SetEase(Ease.OutQuart));
            sequence.Join(_shiroko.DOJumpAnchorPos(_shiroko.anchoredPosition, 100, 2, 1));
            sequence.OnComplete(() => part = 4);
            StoryTalkBox.Instance.SetTalk(StoryTalkBox.Animation.None, "시로코", "시스템 엔지니어", "??????????");
        } else if (part == 4) {
            part = 5;
            StoryTalkBox.Instance.SetTalk(StoryTalkBox.Animation.None, "시로코", "시스템 엔지니어", "분명 AWS 결제한지 별로 지나지 않았는데...");
        } else if (part == 5) {
            part = 6;
            StoryTalkBox.Instance.SetTalk(StoryTalkBox.Animation.Left, "시로코", "시스템 엔지니어", "일단 확인해보자.");
        } else if (part == 6 && !StoryTalkBox.Instance.IsPlaying) {
            part = 7;
            StoryTalkBox.Instance.CloseTalk(StoryTalkBox.Animation.Fade);

            mainImage2.localScale = new Vector3(0.8f, 0.8f, 1);

            sequence = DOTween.Sequence();
            sequence.Join(shiroko_image.DOFade(0, 0.5f));
            sequence.Join(mainImage1_img.DOFade(0, 0.3f).SetEase(Ease.Linear));

            sequence.Append(mainImage2_img.DOFade(1, 0.5f).SetEase(Ease.OutQuart));
            sequence.Join(mainImage2.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutQuart));

            sequence.OnComplete(() => part = 8);
        } else if (part == 8) {
            part = 9;
            StoryTalkBox.Instance.SetTalk(StoryTalkBox.Animation.Fade, "시로코", "시스템 엔지니어", "이렇게 많이 쓸리가...");
            shiroko_image.DOFade(1, 0.5f);
        } else if (part == 9 && !StoryTalkBox.Instance.IsPlaying) {
            part = 10;
            StoryTalkBox.Instance.SetTalk(StoryTalkBox.Animation.None, "시로코", "시스템 엔지니어", "내 AWS 키를 모르고 깃허브에 공개로 올렸다..");
            _shiroko.DOShakeAnchorPos(0.5f, fadeOut: false).OnComplete(() => part = 11);
        } else if (part == 11) {
            part = 12;
            StoryTalkBox.Instance.SetTalk(StoryTalkBox.Animation.Left, "시로코", "시스템 엔지니어", "일단 이 돈을 어떻게 해결하지....");
        } else if (part == 12) {
            blackImage.color = new Color(0,0,0,0);
            blackImage.transform.GetChild(0).gameObject.SetActive(false);
            _screenBlack.SetAsLastSibling();
            
            blackImage.DOFade(1, 2).OnComplete(() => SceneManager.LoadScene("ChickenTutorial"));
            _manager.EndScene();
        }
    }
}
