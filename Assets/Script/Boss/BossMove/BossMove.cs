using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;
    BossMoveArea area;
    BossAttackLine atkLine;
    public GameObject bosshp;
    [Header("목표와의 간격")][SerializeField][Range(0f, 6f)] float contactDistance = 0f;   // 목표와 거리가 이정도 가까워 지면 멈춤
    private BossPhase bossPhase;
    private Movement2D move2D;
    private MovePoint check;
    BossCanvas bossCanvas;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        move2D = GetComponent<Movement2D>();
        bossPhase = GetComponent<BossPhase>();
        area = GetComponentInChildren<BossMoveArea>();
        atkLine = GetComponentInChildren<BossAttackLine>();
        check = GameObject.FindGameObjectWithTag("MapFollwed").GetComponent<MovePoint>();
        bossCanvas = GameObject.Find("BossCanvas").GetComponent<BossCanvas>();
    }

    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (!check.playerArriveCheck)   // 플레이어가 도착 하면 실행
        {
            if (!bossCanvas.isEntry)
            {
                move2D.moveSpeed = 0.0f;
            }
            else
            {
                if (Vector2.Distance(transform.position, target.position) > contactDistance && area.playerIn)
                {
                    //Debug.Log("보스 피 열기");
                    //GameObject.Find("Canvas").transform.Find("BossHPBar").gameObject.SetActive(true);
                    //GameObject.Find("Canvas").transform.GetChild(6).gameObject.SetActive(true);
                    move2D.moveSpeed = 2.0f;
                    bosshp.SetActive(true);
                    transform.position = Vector2.MoveTowards(transform.position, target.position, move2D.moveSpeed * Time.deltaTime);
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }

                if (atkLine.NowAttack)   // 전체 공격시 이동 멈춤
                {
                    move2D.moveSpeed = 0.0f;
                }
                else
                {
                    move2D.moveSpeed = 2.0f;
                }
            }

        }

    }

}
