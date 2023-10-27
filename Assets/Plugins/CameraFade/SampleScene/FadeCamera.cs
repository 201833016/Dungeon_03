using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CameraFading;

public class FadeCamera : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leveltext;
    [SerializeField] private TextMeshProUGUI pressEntertext;
    

    int level = 1;
    private void Start()
    {
        CameraFade.Out(0f);
        leveltext.color = new Color(255, 255, 255, 0);
        pressEntertext.color = new Color(255, 255, 255, 0);
        StartCoroutine(FadeLevelTextStart());
        StartCoroutine(FadeTextToFullAlpha());
        pressEntertext.text = "Space바를 눌러주세요";
    }

    private void Update()
    {
        if (level > 3)
        {
            leveltext.text = "끝까지 옴, 종료";
        }
        else
        {
            leveltext.text = $"레벨 {level}";
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine(FadeLevelTextEnd());

            StopCoroutine(FadeTextToFullAlpha());
            StopCoroutine(FadeTextToZero());
            leveltext.gameObject.SetActive(false);
            pressEntertext.gameObject.SetActive(false);
            Invoke("FadeInMode", 0.1f);
        }
    }

    public void FadeInMode()
    {
        CameraFade.In(1.5f);
    }

    IEnumerator FadeTextToFullAlpha() // 알파값 0에서 1로 전환
    {
        pressEntertext.color = new Color(pressEntertext.color.r, pressEntertext.color.g, pressEntertext.color.b, 0);
        while (pressEntertext.color.a < 1.0f)
        {
            pressEntertext.color = new Color(pressEntertext.color.r, pressEntertext.color.g, pressEntertext.color.b, pressEntertext.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
        StartCoroutine(FadeTextToZero());
    }

    IEnumerator FadeTextToZero()  // 알파값 1에서 0으로 전환
    {
        pressEntertext.color = new Color(pressEntertext.color.r, pressEntertext.color.g, pressEntertext.color.b, 1);
        while (pressEntertext.color.a > 0.0f)
        {
            pressEntertext.color = new Color(pressEntertext.color.r, pressEntertext.color.g, pressEntertext.color.b, pressEntertext.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        StartCoroutine(FadeTextToFullAlpha());
    }

    IEnumerator FadeLevelTextStart()
    {
        leveltext.color = new Color(leveltext.color.r, leveltext.color.g, leveltext.color.b, 0);
        while (leveltext.color.a < 1.0f)
        {
            leveltext.color = new Color(leveltext.color.r, leveltext.color.g, leveltext.color.b, leveltext.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
    }

    IEnumerator FadeLevelTextEnd()  // 알파값 1에서 0으로 전환
    {
        leveltext.color = new Color(leveltext.color.r, leveltext.color.g, leveltext.color.b, 1);
        while (leveltext.color.a > 0.0f)
        {
            leveltext.color = new Color(leveltext.color.r, leveltext.color.g, leveltext.color.b, leveltext.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
    }
}
