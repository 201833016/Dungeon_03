using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterC : MonoBehaviour
{
    Player player;
    public GameObject bullet;   // 총알 오브젝트
    public Transform bulletPos; // 총알 발사 위치

    public SOMonster soMonster; // 몬스터 정보
    private TestLevel testLevel;    // 스테이지 레벨
    [HideInInspector] public float currentHP, maxHP;    // 몬스터 현재, 최대 체력
    [HideInInspector] public float DMG, Shd;   // 몬스터 공격력

    private float timer;    // 총알 발사 쿨타임
    private float speed;
    private WayPoints wayPoints;
    private int wayIndex;   // 도착 목표의 좌표

    [SerializeField] private DestructImpact destructImpact;
    private SpriteRenderer sprite;  // 사망시 이미지 비활성화
    private CircleCollider2D crCollider;   // 사망시 콜라이더 충돌 안되게
    private Movement2D movement2D;  // 이동속도
    private MovePoint check;
    Transform MonsterObj, Map;  // 자체 맵으로 좌표 가져오기 위해
    [SerializeField] MonsterHPSlider monsterHPBar;
    private bool dieCheck;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        MonsterObj = gameObject.transform.parent;
        Map = MonsterObj.transform.parent;

        wayPoints = Map.Find("WayPoints").GetComponent<WayPoints>();
        check = GameObject.FindGameObjectWithTag("MapFollwed").GetComponent<MovePoint>();
        testLevel = GameObject.Find("TestLevel").GetComponent<TestLevel>();

        sprite = GetComponent<SpriteRenderer>();
        crCollider = GetComponent<CircleCollider2D>();
        movement2D = GetComponent<Movement2D>();

        maxHP = testLevel.level * soMonster.CON;    // 몬스터 최대 체력 = 레벨 X 생명력 계수
        DMG = testLevel.level * soMonster.STR;   // 몬스터 공격력 = 레벨 X 공격력 계수  
        Shd = testLevel.level * soMonster.END;  // 몬스터 방어력 = 레벨 X 빙어력 계수

        currentHP = maxHP;  // 시작시 현재 HP는 최대 HP롸 같게
        monsterHPBar.UpdateHPBar(currentHP, maxHP);
    }

    private void Update()
    {
        speed = movement2D.moveSpeed;
        if (!check.playerArriveCheck)   // 움직이기
        {
            movement2D.moveSpeed = 10f;
            transform.position = Vector2.MoveTowards(transform.position, wayPoints.wayPoints[wayIndex].position, speed * Time.deltaTime);   // 몬스터 이동

            if (Vector2.Distance(transform.position, wayPoints.wayPoints[wayIndex].position) < 0.1f)
            {
                wayIndex++;
                if (wayIndex >= wayPoints.wayPoints.Length)
                {
                    wayIndex = 0;
                }
            }

            float distance = Vector2.Distance(transform.position, player.transform.position);   // Player와 발사대의 거리  

            if (distance < 20)
            {
                timer += Time.deltaTime;
                if (timer > 0.5)   // 총알 발사 쿨타임 0.5초
                {
                    timer = 0;
                    Shoot();
                }
            }
        }
        else
        {
            movement2D.moveSpeed = 0;   // 이동 금지
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
            currentHP -= 1f;
            monsterHPBar.UpdateHPBar(currentHP, maxHP);
        }

        if (currentHP <= 0)
        {
            StartCoroutine(MonsterC_Death());
        }
        return currentHP;
    }


    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            if (!player.isHurt)
            {
                float Tackle = DMG / 2;
                other.GetComponent<Player>().Reduce(Tackle);    //플레이어 HP깍기
            }
        }
    }

    IEnumerator MonsterC_Death()
    {
        movement2D.moveSpeed = 0f;
        speed = movement2D.moveSpeed;

        sprite.enabled = false;
        crCollider.enabled = false;
        dieCheck = true;

        CameraShake.instance.CamShake();
        soMonster.itemDropTable.ItemDrop(transform.position);   // 몬스터 죽은 위치에 아이템 드랍
        destructImpact.DeathImpact();
        GameObject canvas = gameObject.transform.GetChild(2).gameObject;    // HP바 안보이게
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
