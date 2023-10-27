using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerStat : MonoBehaviour
{
    public static UIPlayerStat instance;
    private void Awake() {
        instance = this;
    }
    [SerializeField] private TextMeshProUGUI[] playerStatText;
    [SerializeField] private Health health;

    private void Start()
    {
        StartStat();
    }
    public void StartStat() // 스텟 UI 표시
    {
        playerStatText[0].text = $"{health.currentHP.ToString("F0")} / {health.maxHP.ToString("F0")}";
        playerStatText[1].text = $"{health.attack.ToString("F0")}";
        playerStatText[2].text = $"{health.defence.ToString("F0")}";
        playerStatText[3].text = $"{health.speedMove.ToString("F0")}";
    }
}
