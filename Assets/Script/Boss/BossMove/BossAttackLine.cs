using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackLine : MonoBehaviour
{
    public static BossAttackLine instance;
    private void Awake()
    {
        instance = this;
    }

    Rigidbody2D rb;
    Transform parentObj;
    public bool NowAttack = false;
    private BossPhase bossPhase;
    private MovePoint check;
    BossCanvas bossCanvas;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parentObj = GetComponentInParent<BossMove>().transform;
        bossPhase = GetComponentInParent<BossPhase>();
        check = GameObject.FindGameObjectWithTag("MapFollwed").GetComponent<MovePoint>();
        bossCanvas = GameObject.Find("BossCanvas").GetComponent<BossCanvas>();
    }

    private void Update()
    {
        this.gameObject.transform.position = parentObj.position;    // 몬스터 인지 영역 유지
        if (!bossCanvas.isEntry)
        {
            bossPhase.StopFiring(AttackType.CircleFire);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 사정거리에 들어오면
        if (collision.CompareTag("Player"))
        {
            if (!check.playerArriveCheck)
            {
                NowAttack = true;
            }
            //bossPhase.StartFiring(AttackType.CircleFire);
        }
    }

    public void TestTT()
    {
        bossPhase.StartFiring(AttackType.CircleFire);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        // 사정거리를 벗어나면
        if (collision.CompareTag("Player"))
        {
            if (!check.playerArriveCheck)
            {
                NowAttack = false;
            }
            bossPhase.StopFiring(AttackType.CircleFire);
        }
    }

}
