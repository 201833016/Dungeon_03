using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleShoot : MonoBehaviour
{
    Player target; //플레이어 위치
    public Transform firePosition_Enemy; // - 총구위치
    public GameObject bulletPackageFactory; // - 총알공장
    private MovePoint check;
    private float timer;
    private float attackDelay = 1.5f;  // 몬스터 공격 속도
    private BossMoveArea area;
    BossCanvas bossCanvas;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        check = GameObject.FindGameObjectWithTag("MapFollwed").GetComponent<MovePoint>();
        area = GetComponentInChildren<BossMoveArea>();
        bossCanvas = GameObject.Find("BossCanvas").GetComponent<BossCanvas>();
    }

    private void Update()
    {
        if (!check.playerArriveCheck && area.playerIn && bossCanvas.isEntry)
        {
            timer += Time.deltaTime;
            if(timer > attackDelay)   // 총알 발사 쿨타임 2초
            {
                timer = 0;
                Shoot();
            }
        }
    }
    public void Shoot()
    {
        GameObject bulletPackage = Instantiate(bulletPackageFactory); //반환값은 게임오브젝트

        bulletPackage.transform.position = firePosition_Enemy.position; //총알 묶음이 나갈 곳은 총구 위치
        bulletPackage.transform.up = (target.transform.position - firePosition_Enemy.position).normalized; //앞 방향을 타겟을 향해 조정(플레이어를 보고 발사하기 위한 코드)

        Transform[] childs = bulletPackage.GetComponentsInChildren<Transform>(); //총알 묶음에서 자식 5개를 가져옴

        for (int i = 0; i < childs.Length; i++) //각 총알의 부모-자식 관계를 끊어줌
        {
            childs[i].parent = null;
        }
        Destroy(bulletPackage); //총알을 묶고 있던 빈 오브젝트를 삭제
    }
}
