using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CameraFading;
public class StatueZone : MonoBehaviour
{
    TestLevel testLevel;    // 스테이지 레벨
    float fadeTime = 1.2f;  // 카메라 꺼지는 시간 1.2 초
    BGMScript bgm;  // 배경음악 소리 줄이기 위한 배경음악 파일
    public Health player_health;   // 캐릭터 스텟 변경
    private void Awake()
    {
        testLevel = GameObject.Find("TestLevel").GetComponent<TestLevel>();
        bgm = GameObject.Find("BGM_Manager").GetComponent<BGMScript>();
    }
    private void Start() {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bgm.End_Boss_BGM_Off();
            CameraFade.Out(fadeTime);
            if (testLevel.level <= 3)
            {
                testLevel.level++;
                PlayerBuffData.instance.ChooseBuff("ATK");
                PlayerBuffData.instance.ChooseBuff("DEF");

                player_health.attack = player_health.attackCnt;
                player_health.defence = player_health.defenceCnt;
                StartCoroutine(SceneReload());
            }
           
        }
    }

    private IEnumerator SceneReload()
    {
        yield return new WaitForSeconds(fadeTime + 0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 씬 새로고침
    }

}
