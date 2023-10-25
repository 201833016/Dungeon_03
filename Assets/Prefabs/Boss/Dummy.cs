using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    BossCanvas bossCanvas;
    BGMScript bgm;
    private void Awake() {
        bossCanvas = GameObject.Find("BossCanvas").GetComponent<BossCanvas>();
        bgm = GameObject.Find("BGM_Manager").GetComponent<BGMScript>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bgm.End_Audio();    // 브금 종료
            bossCanvas.playerin = true; // 보스 애니메이션을 위한 bool
        }
    }
}
