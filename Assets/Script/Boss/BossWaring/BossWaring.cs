using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaring : MonoBehaviour // 오브젝트 : BossRoom - Floor
{
    private bool isAttacking = false;   // 플레이어 감지
    [SerializeField] Boss boss;
    Player player;   // 플레이어 오브젝트
    Vector3 playerPos;  // 플레이어 위치
    Vector3 whereToAtk; // 소환할 위치
    public GameObject warning;  // 전조 그림자
    SpriteRenderer shadow_Sprite;   // 그림자 Fade를 위한 이미지
    public GameObject Atk1; // 공격 오브젝트    // EnemyProjectile 넣어서 하기
    Transform shoadow_trans;
    private SpriteRenderer roomSprite;  // 보스 방의 스프라이트

    bool playerIn = false;  // 플레이어가 해당 방에 있는지 확인
    private MovePoint check;
    BossCanvas bossCanvas;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        check = GameObject.FindGameObjectWithTag("MapFollwed").GetComponent<MovePoint>();
        bossCanvas = GameObject.Find("BossCanvas").GetComponent<BossCanvas>();
        roomSprite = GetComponent<SpriteRenderer>();
        
    }
    private void Start() {
        FieldTile.instance.Start_Floor(roomSprite);   // 바닥 타일깔기
    }
    private void Update()
    {
        playerPos = player.transform.position;
        if (playerIn && boss.BossIn)
        {
            if (bossCanvas.isEntry)
            {
                StartCoroutine(PlayerTime());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = true;
        }

    }

    
    IEnumerator PlayerTime()
    {
        if (!check.playerArriveCheck)
        {
            yield return new WaitForSeconds(2.0f);
            StartCoroutine(BeforeAttack());
        }
    }


    // OntriggerStay는 프레임 단위로 실행되기 때문에 코루틴을 이용
    // isAttacking 변수를 이용해 false일때만 공격을 하도록 설정
    IEnumerator BeforeAttack()
    {
        Quaternion rotation = Quaternion.Euler(70f, 0f, 90f);  // 그림자 회전 각도
        if (isAttacking == false)
        {
            isAttacking = true;
            GameObject Shadow_Obj = Instantiate(warning, playerPos, rotation);
            shoadow_trans = GameObject.Find("WarningShadow(Clone)").GetComponent<Transform>();
            yield return null;
            StartCoroutine(FadeShadow(Shadow_Obj));
        }
    }

    IEnumerator Attack()
    {
        whereToAtk = shoadow_trans.position;
        GameObject ATK_Obj = Instantiate(Atk1, whereToAtk, transform.rotation);
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

    IEnumerator FadeShadow(GameObject obj)
    {
        shadow_Sprite = obj.GetComponent<SpriteRenderer>();
        shadow_Sprite.color = new Color(shadow_Sprite.color.r, shadow_Sprite.color.g, shadow_Sprite.color.b, 0f);
        while (shadow_Sprite.color.a < 1.0f)
        {
            shadow_Sprite.color = new Color(shadow_Sprite.color.r, shadow_Sprite.color.g, shadow_Sprite.color.b, shadow_Sprite.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }

        if (shadow_Sprite.color.a >= 1.0f)
        {
            Destroy(obj);
            StartCoroutine(Attack());
        }
    }
}
