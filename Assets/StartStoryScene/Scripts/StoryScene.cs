using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryScene : MonoBehaviour
{
    public StoryCutsceneManager _manager;
    
    public abstract void StartScene();
    public abstract void InteractScene(); // 씬 진행이 안끝났는데 누른 경우
}
