using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryCutsceneManager : MonoBehaviour
{
    [SerializeField] StoryScene[] scenes;

    bool isFinish = false; // 끝?
    int currentSceneIdx = 0;
    
    private void Start() {
        isFinish = true;
        currentSceneIdx = -1;
        NextScene(); // 강제적으로 함
    }

    private void Update() {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0)) {
            if (StoryTalkBox.Instance.IsTalking) {
                StoryTalkBox.Instance.ImmediatelyStopTalk();
                return;
            }

            if (isFinish) NextScene();
            else scenes[currentSceneIdx].InteractScene();
        }
    }

    public void NextScene() {
        if (scenes.Length <= currentSceneIdx) return; // 다 함
        
        currentSceneIdx ++;
        print("NextScene: "+currentSceneIdx );
        isFinish = false;

        var scene = scenes[currentSceneIdx];
        scene._manager = this;
        scene.StartScene();
    }

    public void EndScene() {
        isFinish = true;
    }
}
