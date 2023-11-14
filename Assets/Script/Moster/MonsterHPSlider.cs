using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private new Camera camera; // HP바 각도 고정용 기준 카메라
    [SerializeField] private Transform target;  // HP 대상 몬스터 위치 기준 오브젝트
    [SerializeField] private Vector3 offset;    // HP의 위치 조정 
    [SerializeField] private Image image;

    private void Awake() {
        camera = Camera.main;
    }
    public void UpdateHPBar(float currentValue, float maxValue)
    {
        slider.gameObject.SetActive(currentValue < maxValue);   // 피가 100% 일때 안보이게
        slider.value = currentValue / maxValue;
        if (slider.value >= 0.66f)
        {
            // 초록색
            image.color = Color.green;
        }
        else if(slider.value >= 0.33f)
        {
            // 노란색
            image.color = Color.yellow;
        }
        else
        {
            // 빨간색
            image.color = Color.red;
        }
    }

    private void Update() {
        transform.rotation = camera.transform.rotation;
        transform.position = target.transform.position + offset;
    }
}
