using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private void Awake() {
        instance = this;
    }
    public Animator camAnim;
    public void CamShake()
    {
        camAnim.SetTrigger("isShake");  
    }
}
