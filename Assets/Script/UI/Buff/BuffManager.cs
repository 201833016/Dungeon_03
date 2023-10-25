using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;
    private void Awake() {
        instance = this;
    }
    public GameObject buffPrefab;
    public bool onATK, onDEF;

    private void Start() {
        onATK = false;
        onDEF = false;
    }

    public void CreateBuff(string type, float per, float dur, Sprite icon)
    {
        GameObject go = Instantiate(buffPrefab, transform); // 버프 아이콘 생성
        go.GetComponent<BaseBuff>().Init(type, per, dur);   // 시작 값
        go.GetComponent<UnityEngine.UI.Image>().sprite = icon;     // 이미지 보여주기
    }

}
