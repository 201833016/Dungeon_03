using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraFading;

public class GameOutFade : MonoBehaviour    // 오브젝트 : UIManager
{
    public static GameOutFade instance;
    private void Awake() {
        instance = this;
    }
    
    public void CameraFadeOut(float time)
    {
        CameraFade.Out(time);
    }

    public void CameraFadeIn(float time)
    {
        CameraFade.In(time);
    }
}
