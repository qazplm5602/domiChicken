using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Image[] mainStar;
    [SerializeField] TextMeshProUGUI mainText;

    [SerializeField] Transform section;
    [SerializeField] GameObject chatTemplate;

    private void Awake() {
        Add(new AssessData() {
            content = "밍",
            name = "도미",
            score = 3
        });
        Add(new AssessData() {
            content = "밍",
            name = "도미222",
            score = 3.5f
        });
        Add(new AssessData() {
            content = "밍sdadsad",
            name = "도미3333",
            score = 4.8f
        });
        Add(new AssessData() {
            content = "밍asdad",
            name = "도미6666",
            score = 4.2f
        });
    }

    public void Add(AssessData data) {
        var box = Instantiate(chatTemplate, section).transform;
        
        box.Find("UserName").GetComponent<TextMeshProUGUI>().text = data.name;
        box.Find("Content").GetComponent<TextMeshProUGUI>().text = data.content;
        box.Find("StarT").GetComponent<TextMeshProUGUI>().text = $"평점 {data.score}";
        
        var starSection = box.Find("StarSection");
        for (int i = 1; i <= 5; i++)
            starSection.GetChild(i - 1).Find("Fill").localScale = i <= data.score ? Vector3.one : (i <= data.score + 0.5f ? new Vector3(0.5f, 1, 1) : Vector3.zero);
    }
}
