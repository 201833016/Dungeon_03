using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CameraFading;
public class StatueZone : MonoBehaviour
{
    TestLevel testLevel;
    float fadeTime = 1.2f;
    BGMScript bgm;
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
            testLevel.level++;
            bgm.End_Boss_BGM_Off();
            CameraFade.Out(fadeTime);
            if (testLevel.level != 3)
            {
                StartCoroutine(SceneReload());
            }
            else if(testLevel.level == 3)
            {
                
            }
           
        }
    }

    private IEnumerator SceneReload()
    {
        yield return new WaitForSeconds(fadeTime + 0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 씬 새로고침
    }

}
