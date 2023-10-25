using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameover : MonoBehaviour
{
    Animator anim;
    [SerializeField]private Player player;
    public void End_GameOver()
    {
        GameObject panel = this.gameObject.transform.GetChild(2).gameObject;
        panel.SetActive(true);
        
        StartCoroutine(GameOver_End());
    }

    IEnumerator GameOver_End()
    {
        anim = GetComponent<Animator>();
        Debug.Log("오버 1");
        Time.timeScale = 1f;
        anim.SetBool("isOver", true);
        yield return new WaitForSeconds(1f);
        Debug.Log("오버 2");
        player.Overbool = false;
        SceneMove.instance.MoveStartScene();    // 씬 이동
        yield return null;
    }
}
