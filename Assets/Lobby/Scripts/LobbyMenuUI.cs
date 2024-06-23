using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyMenuUI : MonoBehaviour
{
    [SerializeField] Button startBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] Button domiWeb;

    private void Awake() {
        startBtn.onClick.AddListener(StartScene);
        exitBtn.onClick.AddListener(Exit);
        domiWeb.onClick.AddListener(OpenURL);
    }

    void StartScene() {
        SceneManager.LoadScene("StartStoryScene");
    }

    void Exit() {
        Application.Quit();
    }

    void OpenURL() {
        Application.OpenURL("https://domi.kr");
    }
}
