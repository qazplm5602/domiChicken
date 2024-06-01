using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T _instance = null;
    private static bool IsDestoryed = false;

    public static T Instance {
        get {
            if (IsDestoryed) {
                _instance = null;
            }
            
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<T>();
                if (_instance == null) {
                    Debug.LogWarning($"{typeof(T).Name} singleton is not exits");
                } else {
                    IsDestoryed = false;
                }
            }

            return _instance;
        }
    }

    private void OnDisable() {
        IsDestoryed = true;    
    }
}
