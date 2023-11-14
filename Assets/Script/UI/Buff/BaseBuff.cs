using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseBuff : MonoBehaviour
{
    public string type;         // 버프 종류
    public float percentage;
    public float duration;      // 전체 시간
    public float current_Time;  // 남은 시간
    public Image icon;          // 버프 아이콘
    [SerializeField] private TextMeshProUGUI coolTime_Text; // 쿨타임 표시, 아이콘 위
    public BuffUseYN buffyn;    // 해당 종류의 버프를 사용중인가
    bool endATK, endDEF = false;    // 버프 아이템이 공격인가, 방어인가

    private void Awake()
    {
        buffyn.ATK_Text = GameObject.Find("ATKCheckText").GetComponent<TextMeshProUGUI>();
        buffyn.DEF_Text = GameObject.Find("DEFCheckText").GetComponent<TextMeshProUGUI>();
    }

    public void Init(string type, float per, float dur) // 초기화
    {
        this.type = type;
        percentage = per;
        duration = dur;
        current_Time = duration;
        icon.fillAmount = 1;
        coolTime_Text = GetComponentInChildren<TextMeshProUGUI>();

        Excute();
    }
    WaitForSeconds seconds = new WaitForSeconds(0.1f);
    public void Excute()    // 버프 시간 시작시
    {
        PlayerBuffData.instance.onBuff.Add(this);
        PlayerBuffData.instance.ChooseBuff(type);
        StartCoroutine(Activation());
    }

    IEnumerator Activation()
    {
        while (current_Time > 0)
        {
            current_Time -= 0.1f;
            icon.fillAmount = current_Time / duration;
            coolTime_Text.text = current_Time.ToString("F0");   // 소수점 없이 남은 시간 표시
            if (type == "ATK")  // 공격 아이템 사용시
            {
                BuffManager.instance.onATK = true;
                buffyn.ATK_Text.text = "재사용 시간: " + current_Time.ToString("F1") + "초";  // 인벤토리에서 소수점 1자리로 표시
                if (buffyn.ATK_Text.text == "재사용 시간: 0.0초")
                {
                    endATK = true;
                }

            }
            if (type == "DEF")  // 방어 아이템 사용시
            {
                BuffManager.instance.onDEF = true;
                buffyn.DEF_Text.text = "재사용 시간: " + current_Time.ToString("F1") + "초";
                if (buffyn.DEF_Text.text == "재사용 시간: 0.0초")
                {
                    endDEF = true;
                }

            }
            yield return seconds;
        }
        if (BuffManager.instance.onATK == true && endATK == true)
        {
            endATK = false;
            BuffManager.instance.onATK = false;
            BuffUseYN.instance.ATKEnabled(false);
        }

        if (BuffManager.instance.onDEF == true && endDEF == true)
        {
            endDEF = false;
            BuffManager.instance.onDEF = false;
            BuffUseYN.instance.DEFEnabled(false);
        }

        icon.fillAmount = 0;
        current_Time = 0;
        coolTime_Text.enabled = false;

        DeActivation();
    }

    public void DeActivation()  // 버프 시간 종료시
    {
        PlayerBuffData.instance.onBuff.Remove(this);
        PlayerBuffData.instance.ChooseBuff(type);
        Destroy(gameObject);
    }


}
