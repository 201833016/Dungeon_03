using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    public static BGMScript instance;
    private void Awake() {
        BGMScript.instance = this;
    }
    [SerializeField] private AudioSource[] audioSource;   // 음원 파일
    [SerializeField] private AudioSource[] bossBGM;   // 음원 파일
    float duration = 3f;    // 3초에 걸쳐 소리 줄이기
    private int num;
    private int bossNum;
    
    private void Start() {
        num = Random.Range(0, audioSource.Length);   // 0 ~ 음원갯수
        bossNum = Random.Range(0, bossBGM.Length);   // 0 ~ 음원갯수
    }
    public void StartAudio()
    {
        StartCoroutine(Audio(num));
    }
    public void End_Audio()
    {
        StartCoroutine(Audio_End(num));
    }

    public void Start_StandByScreen_BGM_On()
    {
        StartCoroutine(StandByScreen_BGM_On());
    }
    public void End_StandByScreen_BGM_Off()
    {
        StartCoroutine(StandByScreen_BGM_Off());
    }

    public void Start_Boss_BGM_On()
    {
        StartCoroutine(Boss_BGM_On(bossNum));
    }
    public void End_Boss_BGM_Off()
    {
        StartCoroutine(Boss_BGM_Off(bossNum));
    }



    IEnumerator Audio(int num)
    {
        audioSource[num].Play();
        audioSource[num].volume = 0.5f;
        switch (num)
        {
            case 0:
                Debug.Log("BGM : [아르고스]");
                break;
            case 1:
                Debug.Log("BGM : [악몽의 그늘]");
                break;
            case 2:
                Debug.Log("BGM : [라우리엘 1페이즈]");
                break;
            case 3:
                Debug.Log("BGM : [습격받은 엘리야비크]");
                break;
            case 4:
                Debug.Log("BGM : [알비온의 분노]");
                break;
            default:
                break;
        }
        yield return null;
    }

    IEnumerator Audio_End(int num)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource[num].volume -= 0.002f;
            yield return null;
        }
        audioSource[num].Stop();
    }

    IEnumerator StandByScreen_BGM_On()  // 대기화면 브금 켜기
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("BGM : [페이튼]");
        audioSource[0].Play();
        audioSource[0].volume = 0.8f;
        yield return null;
    }

    IEnumerator StandByScreen_BGM_Off() // 대기화면 브금 끄기
    {
        float currentTime = 0f;
        while (currentTime < 3)
        {
            currentTime += Time.deltaTime;
            audioSource[0].volume -= 0.005f;
            yield return null;
        }
        audioSource[0].Stop();
    }

    IEnumerator Boss_BGM_On(int bossNum)
    {
        bossBGM[bossNum].Play();
        bossBGM[bossNum].volume = 0.5f;
        switch (bossNum)
        {
            case 0:
                Debug.Log("BGM : [발탄]");
                break;
            case 1:
                Debug.Log("BGM : [일리아칸]");
                break;
            default:
                break;
        }
        yield return null;
    }

    IEnumerator Boss_BGM_Off(int bossNum)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            bossBGM[bossNum].volume -= 0.002f;
            yield return null;
        }
        bossBGM[bossNum].Stop();
    }


}
