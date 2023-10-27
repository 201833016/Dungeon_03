using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    public void UpdateHPBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
        healthText.text = $"{currentValue.ToString("F0")} / {maxValue}";
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
