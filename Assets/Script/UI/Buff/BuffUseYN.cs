using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffUseYN : MonoBehaviour
{
    public static BuffUseYN instance;
    private void Awake()
    {
        instance = this;
    }
    public Image timePrefab;    // 테두리 오브젝트 이미지   [enable = false 상태 유지] 
    public TextMeshProUGUI ATK_Text;    // 공격 버프 사용시 현재 시간 텍스트
    public TextMeshProUGUI DEF_Text;    // 방어 버프 사용시 현재 시간 텍스트


    private void Start()    // 시작시 텍스트 비활성화
    { 
        AllEnabledFalse();
    }

    public void AllEnabledFalse()
    {
        ATK_Text.enabled = false;
        DEF_Text.enabled = false;
    }

    public void ATKEnabled(bool yesno)  // 공격 버프 비/활성화
    {
        //timePrefab.enabled = yesno;
        ATK_Text.enabled = yesno;
    }

    public void DEFEnabled(bool yesno)  // 방어 버프 비/활성화
    {
        //timePrefab.enabled = yesno;
        DEF_Text.enabled = yesno;
    }



}
