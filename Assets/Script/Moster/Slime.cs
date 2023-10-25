using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    Player player;

    public SOMonster soMonster; // 몬스터 정보
    private TestLevel testLevel;    // 스테이지 레벨
    [SerializeField] private DestructImpact destructImpact;
    [SerializeField] private SpriteRenderer sprite;  // 사망시 이미지 비활성화
    private BoxCollider2D bxCollider;   // 사망시 콜라이더 충돌 안되게
    private Movement2D move2D;
    [HideInInspector] public float currentHP, maxHP;    // 몬스터 현재, 최대 체력
    [HideInInspector] public float DMG;   // 몬스터 공격력
    float attackDelay;  // 몬스터 공격 속도
    [SerializeField] MonsterHPSlider monsterHPBar;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        testLevel = GameObject.Find("TestLevel").GetComponent<TestLevel>();
        //sprite = GetComponent<SpriteRenderer>();
        bxCollider = GetComponent<BoxCollider2D>();
        move2D = GetComponent<Movement2D>();
    }
    private void Start()
    {
        maxHP = testLevel.level * soMonster.CON;    // 몬스터 최대 체력 = 레벨 X 생명력 계수
        DMG = testLevel.level * soMonster.STR;   // 몬스터 공격력 = 레벨 X 공격력 계수
        attackDelay = 1f / ((soMonster.AGI + 1) * 0.5f);    // 몬스터 공격 속도 = 1 / ((속도 계수 + 1) * 1/2);

        currentHP = maxHP;  // 시작시 현재 HP는 최대 HP롸 같게
        monsterHPBar.UpdateHPBar(currentHP, maxHP);
    }

    public float GetHit(float damage)   // 몬스터 피격시
    {
        currentHP -= damage;
        monsterHPBar.UpdateHPBar(currentHP, maxHP);
        if (currentHP <= 0)
        {
            StartCoroutine(SlimeDeath());
        }

        return currentHP;
    }


    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            if (!player.isHurt)
            {
                other.GetComponent<Player>().Reduce(DMG);    //플레이어 HP깍기
            }
        }
    }
    IEnumerator SlimeDeath()
    {
        sprite.enabled = false;
        bxCollider.enabled = false; 
        move2D.moveSpeed = 0f;
        CameraShake.instance.CamShake();
        soMonster.itemDropTable.ItemDrop(transform.position);   // 몬스터 죽은 위치에 아이템 드랍
        destructImpact.DeathImpact();
        GameObject canvas = gameObject.transform.GetChild(2).gameObject;    // HP바 안보이게
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
