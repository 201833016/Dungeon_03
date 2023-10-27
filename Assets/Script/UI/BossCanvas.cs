using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossCanvas : MonoBehaviour
{
    Player player;
    Transform pos;
    public Animator anim;       // 보스 애니메이션 1 ~ 5
    public Animator anim2;
    public Animator anim3;
    public Animator anim4;
    public Animator anim5;
    [SerializeField] private TextMeshProUGUI bossNametext;
    [SerializeField] private TextMeshProUGUI pressEntertext;

    [SerializeField] private GameObject playerHp;   
    [SerializeField] private GameObject buff;

    public bool isEntry = false;
    public bool playerin;
    BGMScript bgm;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pos = GameObject.Find("PlayerSpawn").GetComponent<Transform>();
        bgm = GameObject.Find("BGM_Manager").GetComponent<BGMScript>();
        bossNametext.color = new Color(255, 255, 255, 0);
        pressEntertext.color = new Color(255, 255, 255, 0);
        
        bossNametext.text = "보스 몬스터";
        pressEntertext.text = "Space바를 눌러주세요";
    }
    private void Update()
    {
        if (playerin)
        {
            PlayerPos();
            StartCanvas();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerin = false;
                EndCanvas();
            }
        }


    }

    private void LateUpdate()
    {
        if (!isEntry && !playerin)
        {
            bossNametext.color = new Color(255, 255, 255, 0);
            pressEntertext.color = new Color(255, 255, 255, 0);
        }
    }
    public void StartCanvas()
    {
        StartCoroutine(BossCanvasStart());
    }

    public void EndCanvas()
    {
        StartCoroutine(BossCanvasOut());
        playerHp.SetActive(true);
        buff.SetActive(true);
    }


    private IEnumerator BossCanvasStart()
    {
        for (int i = 3; i < this.gameObject.transform.parent.childCount; i++)
        {
            GameObject aaa = this.gameObject.transform.parent.GetChild(i).gameObject;
            aaa.SetActive(false);
        }
        bossNametext.color = new Color(255, 255, 255, 0);
        pressEntertext.color = new Color(255, 255, 255, 0);
        anim.SetBool("inBC", true);
        yield return new WaitForSeconds(1f);
        anim2.SetBool("inBC", true);
        yield return new WaitForSeconds(1f);
        anim3.SetBool("inBC", true);
        yield return new WaitForSeconds(1f);
        anim4.SetBool("inBC", true);
        anim5.SetBool("inBC", true);
    }

    IEnumerator BossCanvasOut()
    {
        anim.SetBool("outBC", true);
        anim2.SetBool("outBC", true);
        anim3.SetBool("outBC", true);
        bossNametext.color = new Color(255, 255, 255, 0);
        pressEntertext.color = new Color(255, 255, 255, 0);

        yield return new WaitForSeconds(1.5f);
        bgm.Start_Boss_BGM_On();
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            GameObject thisObj = this.gameObject.transform.GetChild(i).gameObject;
            thisObj.SetActive(false);
        }
        yield return new WaitForSeconds(2f);
        
        isEntry = true;
        BossAttackLine.instance.TestTT();
    }

    public void PlayerPos()
    {
        player.transform.position = new Vector3(pos.position.x, pos.position.y, pos.position.z);
    }

}
