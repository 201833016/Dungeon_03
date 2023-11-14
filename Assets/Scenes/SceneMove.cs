using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CameraFading;
public class SceneMove : MonoBehaviour
{
    public static SceneMove instance;
    private void Awake() {
        instance = this;
    }
    float fadeTime = 0.5f;

    public void MoveTestScene()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void MoveStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void MoveSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void MoveReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 씬 새로고침
    }

    public void GameStartScene()
    {
        CameraFade.Out(fadeTime);
        BGMScript.instance.End_StandByScreen_BGM_Off();
        StartFade();
        StartCoroutine(GameStart());
    }
    public void GameOut()
    {
        // 게임 프로그램 나가기
        Application.Quit();
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(fadeTime + 0.1f);
        SceneManager.LoadScene("SampleScene");
    }

    public void StartFade()
    {
        for (int i = 2; i < this.gameObject.transform.parent.childCount; i++)
        {
            GameObject aaa = this.gameObject.transform.parent.GetChild(i).gameObject;
            aaa.SetActive(false);
        }
    }

    public void GameOverFade()
    {
        GameObject gameover_Panel = this.gameObject.transform.parent.GetChild(this.gameObject.transform.parent.childCount -1 ).gameObject;   // canvas 마지막-1인 GameoverPanel을 활성화
        gameover_Panel.SetActive(true);
    }
}
