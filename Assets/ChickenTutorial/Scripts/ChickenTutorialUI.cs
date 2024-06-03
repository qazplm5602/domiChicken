using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ChickenTutorialUI : MonoBehaviour
{
    [SerializeField] TutorialBoxUI _helpBox;
    [SerializeField] Image _black;
    [SerializeField] Volume _volume;
    
    private void Start() {
        _black.DOFade(0, 1);
        // field.focusDistance      
        StartCoroutine(ShowPopUp());  
    }

    IEnumerator ShowPopUp() {
        yield return new WaitForSeconds(2);
        
        _helpBox.Show();

        // 화면 블러처리
        _volume.profile.TryGet<DepthOfField>(out var field);

        float time = 0;
        while (time < 1) {
            time += 0.5f / Time.deltaTime;
            field.focusDistance.value = Mathf.Lerp(5, 1, time);
            yield return null;
        }
    }
}
