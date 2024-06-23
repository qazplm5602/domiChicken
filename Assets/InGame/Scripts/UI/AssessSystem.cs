using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct AssessData {
    public float score;
    public string name;
    public string content;
}

public class AssessSystem : MonoBehaviour
{
    [SerializeField] Transform[] mainStar;
    [SerializeField] TextMeshProUGUI mainText;

    [SerializeField] Transform section;
    [SerializeField] GameObject chatTemplate;
    
    List<AssessData> assessDatas;

    private void Awake() {
        assessDatas = new();
    }

    public void Add(AssessData data) {
        var box = Instantiate(chatTemplate, section).transform;
        
        box.Find("UserName").GetComponent<TextMeshProUGUI>().text = data.name;
        box.Find("Content").GetComponent<TextMeshProUGUI>().text = data.content;
        box.Find("StarT").GetComponent<TextMeshProUGUI>().text = $"평점 {data.score}";
        
        var starSection = box.Find("StarSection");
        for (int i = 1; i <= 5; i++)
            starSection.GetChild(i - 1).Find("Fill").localScale = i <= data.score ? Vector3.one : (i <= data.score + 0.5f ? new Vector3(0.5f, 1, 1) : Vector3.zero);
    
        assessDatas.Add(data);
        MainStarUpdate();
    }

    void MainStarUpdate() {
        float averge = assessDatas.Sum(v => v.score) / assessDatas.Count;
        averge = Mathf.Floor(averge * 10f) /  10f; // 소수점 1자리만

        for (int i = 1; i <= 5; i++)
            mainStar[i - 1].localScale = i <= averge ? Vector3.one : (i <= averge + 0.5f ? new Vector3(0.5f, 1, 1) : Vector3.zero);
        
        mainText.text = $"평점: {averge}";
    }
}
