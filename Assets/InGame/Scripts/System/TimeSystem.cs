using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeT;
    [SerializeField] Image icon;
    
    [Space]
    [SerializeField] Sprite dayI;
    [SerializeField] Sprite nightI;
    
    private void OnEnable() {
        StartCoroutine(Timer());
    }
    
    private void OnDisable() {
        StopAllCoroutines();
    }

    IEnumerator Timer() {
        while (true) {
            DateTime now = DateTime.Now;
            
            timeT.text = now.ToString("hh : mm");
            icon.sprite = now.Hour >= 21 || now.Hour <= 9 ? nightI : dayI;

            yield return new WaitForSeconds(29f);
        }
    }
}
