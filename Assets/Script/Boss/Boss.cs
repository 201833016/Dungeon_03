using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour   // 오브젝트 : Boss
{
    [HideInInspector] public float BossCurrentHP, BossMaxHP, BossAttack, BossDefence;
    public BossStat bossStat;
    [SerializeField] private GameObject symbolPrefab; // 보스 처치시 등장하는 신상 프리팹
    GameObject bossStatue;
    private TestLevel testLevel;    // 스테이지 레벨
    Vector3 stauePos;   // 신상 위치
    public BossHPBar bossHP;
    public bool BossIn;
    private void Awake()
    {
        bossStatue = GameObject.Find("BossStatue");
        testLevel = GameObject.Find("TestLevel").GetComponent<TestLevel>();
        stauePos = bossStatue.transform.position;
        BossIn = true;
    }

    private void Start() {
        BossMaxHP = testLevel.level * bossStat.maxHP;    // 최대 체력
        BossCurrentHP = testLevel.level * bossStat.currentHP; // 현재 체력
        BossAttack = testLevel.level * bossStat.attack;   // 공격력
        BossDefence = testLevel.level * bossStat.defence; // 방어력

        //bossHP = GameObject.Find("Canvas").transform.Find("BossHPBar").GetComponent<BossHPBar>();
        bossHP.UpdateHPBar(BossCurrentHP, BossMaxHP);
    }

    public float Reduce(float damage)  // 체력 감소 시
    {
        float real_damage = damage - BossDefence;
        if (real_damage > 0)
        {
            BossCurrentHP -= real_damage;
            bossHP.UpdateHPBar(BossCurrentHP, BossMaxHP);
        }
        else
        {
            BossCurrentHP -= 1f;
            bossHP.UpdateHPBar(BossCurrentHP, BossMaxHP);
        }

        if (BossCurrentHP <= 0)
        {   
            BossDie();
        }
        return BossCurrentHP;
    }

    public void BossDie()
    {
        BossIn = false;
        bossHP.gameObject.SetActive(false); // 보스 피 안보이게
        Instantiate(symbolPrefab, stauePos ,Quaternion.identity); // Symbol을 맵 중앙에 소환    
        Destroy(gameObject);
    }
}
