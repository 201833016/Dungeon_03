using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI healthText;

    public void UpdateHPBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
        float bosshp = slider.value * 100;
        healthText.text = $"{bosshp.ToString("F1")} % - ({currentValue.ToString()})";
    }

}
