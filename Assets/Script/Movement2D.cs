using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public static Movement2D instance;
    private void Awake() {
        instance = this;
    }
    public float moveSpeed = 0.0f;
    public Vector3 moveDiretion = Vector3.zero;

    private void Update() {
        transform.position += moveDiretion * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 dir)
    {
        moveDiretion = dir;
    }
}
