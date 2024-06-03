using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCam : MonoBehaviour
{
    [SerializeField, Range(1, 100)] float speed = 10;
    private void Update() {
        transform.Rotate(Vector3.down * speed * Time.deltaTime);
    }
}
