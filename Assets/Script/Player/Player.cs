using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraFading;

public class Player : MonoBehaviour     // 오브젝트 : Player
{
    public static Player Instance { get; private set; }
    Rigidbody2D rigid;
    //public int speedPlayer;   // 플레이어 이동 속도 
    public Movement2D movement2D;
    //private Vector2 speedVec;   // 가속도
    [SerializeField] private MinimapPos miniPoint;    // 미니 맵 표시
    SpriteRenderer spriteRenderer;
    public bool isHurt;

    [HideInInspector] public float currnetHeath, maxHealth, attackPlayer, defencePlayer, speedPlayer, speedAttackPlayer;
    [HideInInspector] public int bulletMode;
    public Health health;
    public PlayerHPBar playerHPBar;

    public Animator animator;
    private ParticleSystem breakParticel;   // 사망시 파괴 파티클  
    private TestLevel testLevel;    // 스테이지 레벨
    float x, y;
    [SerializeField] private UIManager uiMa;
    public bool Overbool = false;
    private void Awake()
    {
        Instance = this;
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement2D = GetComponent<Movement2D>();
        breakParticel = GetComponentInChildren<ParticleSystem>();
        testLevel = GameObject.Find("TestLevel").GetComponent<TestLevel>();
        
        if (testLevel.level == 1)   // 게임 시작시 상태 초기화
        {
            health.maxHP = 200f;
            health.currentHP = 200f;
            health.attack = 20f;
            health.defence = 10f;
            health.speedMove = 5f;
            health.speedAttack = 5f;
            health.shootMode = 1;
        }
        PlayerStateUpdate();
        

    }

    private void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        bool xDown = Input.GetButtonDown("Horizontal");
        bool xUp = Input.GetButtonUp("Horizontal");

        bool yDown = Input.GetButtonDown("Vertical");
        bool yUp = Input.GetButtonUp("Vertical");


        // 애니메이션
        if (animator.GetInteger("hAxisRaw") != x)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("hAxisRaw", (int)x);
        }
        else if (animator.GetInteger("vAxisRaw") != y)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("vAxisRaw", (int)y);
        }
        else
        {
            animator.SetBool("isChange", false);
        }

    }

    private void FixedUpdate()
    {
        movement2D.MoveTo(new Vector3(x, y, 0));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Maps")) // 맵 이동시, 바닥을 밟으면
        {
            string roomName = other.transform.parent.name;  // 넘어간 맵의 이름 알기
            int miniTest;
            Debug.Log($"이름: {roomName}");
            switch (roomName)
            {
                // 문 1개 미니맵
                case "Map_B(Clone)":
                    miniTest = 0;
                    miniPoint.MinimapOne(miniTest);
                    break;
                case "Map_L(Clone)":
                    miniTest = 1;
                    miniPoint.MinimapOne(miniTest);
                    break;
                case "Map_R(Clone)":
                    miniTest = 2;
                    miniPoint.MinimapOne(miniTest);
                    break;
                case "Map_T(Clone)":
                    miniTest = 3;
                    miniPoint.MinimapOne(miniTest);
                    break;

                // 문 2개 미니맵
                case "Map_LB(Clone)":
                    miniTest = 0;
                    miniPoint.MinimapTwo(miniTest);
                    break;
                case "Map_LR(Clone)":
                    miniTest = 1;
                    miniPoint.MinimapTwo(miniTest);
                    break;
                case "Map_TL(Clone)":
                    miniTest = 2;
                    miniPoint.MinimapTwo(miniTest);
                    break;
                case "Map_RB(Clone)":
                    miniTest = 3;
                    miniPoint.MinimapTwo(miniTest);
                    break;
                case "Map_TR(Clone)":
                    miniTest = 4;
                    miniPoint.MinimapTwo(miniTest);
                    break;
                case "Map_TB(Clone)":
                    miniTest = 5;
                    miniPoint.MinimapTwo(miniTest);
                    break;

                case "ClosedRoom(Clone)":
                    miniPoint.MinimapPointClose();
                    break;
                default:
                    Debug.Log("기본 맵");
                    break;
            }
        }

    }

    public void Reduce(float damage)  // 체력 감소 시
    {
        if (!isHurt)
        {
            float real_damage = damage - defencePlayer;
            if (real_damage > 0)
            {
                health.currentHP -= real_damage;
                breakParticel.Play();   // 파티클 실행
                isHurt = true;
                StartCoroutine(HurtRoutine());
                StartCoroutine(AlphaBlink());
            }
            else
            {
                health.currentHP -= 1f;
            }
        }

        //CreateHitFeedback();
        PlayerStateUpdate();
        if (currnetHeath <= 0)
        {
            Debug.Log("사망");
            health.currentHP = health.maxHP;
            PlayerStateUpdate();
            GameOut();
        }
    }


    public void PlayerStateUpdate()  // 플레이어 상태 업데이트
    {
        maxHealth = health.maxHP;    // 최대 체력
        currnetHeath = health.currentHP; // 현재 체력
        attackPlayer = health.attack;   // 공격력
        defencePlayer = health.defence; // 방어력
        speedPlayer = health.speedMove;   // 이동 속도
        speedAttackPlayer = health.speedAttack; // 공격 속도
        bulletMode = health.shootMode;  // 총알 갯수

        movement2D.moveSpeed = health.speedMove; // 이동 속도 가져오기
        playerHPBar.UpdateHPBar(currnetHeath, maxHealth);
    }

    public void GameOut()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator HurtRoutine()   // 피격 효과 시간
    {
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }
    IEnumerator AlphaBlink()    // 피격시 깜빡임
    {
        while (isHurt)
        {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 0);

            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    IEnumerator GameOver()
    {
        uiMa.TestHide();
        Overbool = true;
        Time.timeScale = 0; // 일시정지

        SceneMove.instance.StartFade(); // UI 오브젝트 가리기
        SceneMove.instance.GameOverFade();
  
        Debug.Log("플레이어 사망");
        
        testLevel.level = 1;

        yield return null;
        
    }
}
