using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StorySceneHome : StoryScene
{
    [SerializeField] RectTransform _shiroko;
    [SerializeField] Image _houseBG;

    int part = 0;

    public override void StartScene()
    {
        part = 0;
        _houseBG.DOFade(1, .25f).SetEase(Ease.Linear).OnComplete(ShirokoShow);
    }

    public override void InteractScene()
    {
        if (part == 2) {
            part = 3;
            StoryTalkBox.Instance.SetTalk(StoryTalkBox.Animation.Left, "시로코", "시스템 엔지니어", "이제 컴퓨터 앞에 앉아서 일이나 해볼까.");
        } else if (part == 3 && !StoryTalkBox.Instance.IsPlaying) {
            _manager.NextScene();
        }
    }

    void ShirokoShow() {
        part = 1;
        Image character = _shiroko.GetComponent<Image>();
        character.color = new Color(1, 1, 1, 0);
        _shiroko.anchoredPosition = new Vector2(222, -1622);
        
        _shiroko.DOAnchorPosY(-1422, 0.5f);
        character.DOFade(1, 0.5f).OnComplete(() => part = 2);
        StoryTalkBox.Instance.SetTalk(StoryTalkBox.Animation.Fade, "시로코", "시스템 엔지니어", "아 잘잤다.");
    }
}
