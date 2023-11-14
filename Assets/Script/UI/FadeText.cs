using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraFading;
using TMPro;

public class FadeText : MonoBehaviour
{
    private TestLevel testLevel;    // 스테이지 레벨
    [SerializeField] private TextMeshProUGUI leveltext; // 스테이지 레벨 표시 텍스트
    [SerializeField] private TextMeshProUGUI pressEntertext;    // 스페이스바 눌러주세요 텍스트
    [SerializeField] private GameObject playerHp;   // 플레이어 HP UI
    [SerializeField] private GameObject buff;       // 버프 창 UI
    [SerializeField] private GameObject bossCanvas; // 보스 등장 애니메이션 UI
    public bool startGame = true;
    private void Awake()
    {
        testLevel = GameObject.Find("TestLevel").GetComponent<TestLevel>();
    }
    private void Start()
    {
        CameraFade.Out(0f);
        leveltext.color = new Color(255, 255, 255, 0);
        pressEntertext.color = new Color(255, 255, 255, 0);
        for (int i = 3; i < this.gameObject.transform.parent.childCount; i++)
        {
            GameObject aaa = this.gameObject.transform.parent.GetChild(i).gameObject;
            aaa.SetActive(false);
        }
        StartCoroutine(FadeLevelTextStart());
        StartCoroutine(FadeTextToFullAlpha());
        pressEntertext.text = "Space바를 눌러주세요";
    }

    private void Update()
    {
        if (testLevel.level > 3)
        {
            leveltext.text = "게임 클리어";
        }
        else
        {
            leveltext.text = $"레벨 {testLevel.level}";
        }

        if (Input.GetKeyDown(KeyCode.Space) && startGame)
        {
            StopCoroutine(FadeLevelTextEnd());

            StopCoroutine(FadeTextToFullAlpha());
            StopCoroutine(FadeTextToZero());
            leveltext.gameObject.SetActive(false);
            pressEntertext.gameObject.SetActive(false);
            if (testLevel.level > 3)
            {
                SceneMove.instance.MoveStartScene();    // 씬 이동
            }
            Invoke("FadeInMode", 0.1f);
            startGame = false;
        }
    }

    public void FadeInMode()
    {
        CameraFade.In(1.5f);

        playerHp.SetActive(true);
        buff.SetActive(true);
        bossCanvas.SetActive(true);
        Debug.Log("플레이어 HP, 버프 창 활성화");
        BGMScript.instance.StartAudio();

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

    IEnumerator FadeLevelTextStart()    // 레벨 Fade 시작
    {
        leveltext.color = new Color(leveltext.color.r, leveltext.color.g, leveltext.color.b, 0);
        while (leveltext.color.a < 1.0f)
        {
            leveltext.color = new Color(leveltext.color.r, leveltext.color.g, leveltext.color.b, leveltext.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
    }

    IEnumerator FadeLevelTextEnd()  // 레벨 Fade 끝
    {
        leveltext.color = new Color(leveltext.color.r, leveltext.color.g, leveltext.color.b, 1);
        while (leveltext.color.a > 0.0f)
        {
            leveltext.color = new Color(leveltext.color.r, leveltext.color.g, leveltext.color.b, leveltext.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
    }
}
