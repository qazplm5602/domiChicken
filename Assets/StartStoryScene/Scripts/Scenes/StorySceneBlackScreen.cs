using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class StorySceneBlackScreen : StoryScene
{
    [SerializeField] TextMeshProUGUI _text;
    
    public override void StartScene()
    {
        _text.DOFade(1, 3).SetEase(Ease.Linear).OnComplete(() => {
            _manager.NextScene();
        });
    }

    public override void InteractScene()
    {

    }
}
