using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerHPSlider : MonoBehaviour
{
    [SerializeField] private Slider slider; // HP 슬라이더
    [SerializeField] private Transform player;  // HP 대상 몬스터 위치 기준 오브젝트
    [SerializeField] private Vector3 offset;    // HP의 위치 조정 
    [SerializeField] private Image healthBar;   // 슬라이더 표시 이미지
    [SerializeField] private TextMeshProUGUI healthText;    // 현재, 최대 체력 텍스트

    public void UpdateHPBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
        healthText.text = $"{currentValue} / {maxValue}";
        if (slider.value >= 0.66f)
        {
            // 초록색
            healthBar.color = Color.green;
        }
        else if(slider.value >= 0.33f)
        {
            // 노란색
            healthBar.color = Color.yellow;
        }
        else
        {
            // 빨간색
            healthBar.color = Color.red;
        } 
    }
}
