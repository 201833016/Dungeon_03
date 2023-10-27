using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject bullet;   // 총알 오브젝트
    public Transform bulletPos; // 총알 발사 위치
    private float timer;    // 총알 발사 쿨타임
    private Player player;
    MovePoint check;
    public SOMonster soMonster; // 몬스터 정보
    private TestLevel testLevel;    // 스테이지 레벨
    [SerializeField] private DestructImpact destructImpact; // 사망시 파괴 파티클  
    private SpriteRenderer sprite;  // 사망시 이미지 비활성화
    private BoxCollider2D bxCollider;   // 사망시 콜라이더 충돌 안되게
    private bool dieCheck;  // 파티클 실해을 위한 총알 발사 금지
  
    [HideInInspector] public float  currentHP, maxHP;    // 몬스터 현재, 최대 체력
    [HideInInspector] public float DMG, Shd;   // 몬스터 공격력, 방어력

    private float attackDelay;  // 몬스터 공격 속도
    [SerializeField] MonsterHPSlider monsterHPBar;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        check = GameObject.FindGameObjectWithTag("MapFollwed").GetComponent<MovePoint>();
        testLevel = GameObject.Find("TestLevel").GetComponent<TestLevel>();
        monsterHPBar = GetComponentInChildren<MonsterHPSlider>();
        
        sprite = GetComponent<SpriteRenderer>();
        bxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start() 
    {
        maxHP = testLevel.level * soMonster.CON;    // 몬스터 최대 체력 = 레벨 X 생명력 계수
        DMG = testLevel.level * soMonster.STR;   // 몬스터 공격력 = 레벨 X 공격력 계수
        Shd = testLevel.level * soMonster.END;  // 몬스터 방어력 = 레벨 X 빙어력 계수
        attackDelay = 1f / ((soMonster.AGI + 1) * 0.5f);    // 몬스터 공격 속도 = 1 / ((속도 계수 + 1) * 1/2);

        currentHP = maxHP;  // 시작시 현재 HP는 최대 HP롸 같게
        monsterHPBar.UpdateHPBar(currentHP, maxHP);
    }

    private void Update() {
        float distance = Vector2.Distance(transform.position, player.transform.position);   // Player와 발사대의 거리  

        if(distance < 15)   
        {
            timer += Time.deltaTime;
            if(timer > attackDelay)   // 총알 발사 쿨타임 2초
            {
                timer = 0;
                Shoot();
            }  
        }
    }

    private void Shoot()
    {
        if (!check.playerArriveCheck && !dieCheck)
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);   // 총알 clone생성 (이미지, 발사위치, 회전)
        }
    }

    public float GetHit(float damage)   // 몬스터 피격시
    {
        float real_damage = damage - Shd;
        if (real_damage > 0)
        {
            currentHP -= damage;
            monsterHPBar.UpdateHPBar(currentHP, maxHP);
        }
        else
        {
            currentHP -= 5f;
            monsterHPBar.UpdateHPBar(currentHP, maxHP);
        }

        if (currentHP <= 0)
        {
            CameraShake.instance.CamShake();
            soMonster.itemDropTable.ItemDrop(transform.position);   // 몬스터 죽은 위치에 아이템 드랍
            StartCoroutine(Break());
        }
        return currentHP;
    }

    IEnumerator Break()
    {
        sprite.enabled = false;
        bxCollider.enabled = false; 
        destructImpact.DeathImpact();
        dieCheck = true;
        GameObject canvas = gameObject.transform.GetChild(2).gameObject;    // HP바 안보이게
        canvas.SetActive(false);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
