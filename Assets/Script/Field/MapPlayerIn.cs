using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapPlayerIn : MonoBehaviour    // 오브젝트 : (모든) Map_ - Floor
{
    //Player player;
    //Transform pos;
    public bool PlayerIn = false;   // 플레이어가 해당 Room에 있는지
    public bool BossIn = false;
    [SerializeField] private GameObject blessBoxPrefab;
    [SerializeField] private GameObject teleportPrefab;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private MonsterCount monsterCount;
    [SerializeField] private BossCanvas bossCanvas;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //pos = GameObject.Find("PlayerSpawn").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        FieldTile.instance.Start_Floor(spriteRenderer);   // 바닥 타일깔기
        if (monsterPrefab != null)
        {
            monsterPrefab.SetActive(false);
        }
        
    }
    private void Update()
    {
        if (PlayerIn)
        {
            MonsterAppear();

        }
        else
        {
            if (blessBoxPrefab != null)
            {
                blessBoxPrefab.SetActive(false);
            }

        }

        /*if (PlayerIn && BossIn)
        {
            // 플레이어 보스방으로 이동
            if (!bossCanvas.playerin)
            {
                bossCanvas.playerin = true; 
            }
        }*/
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIn = true;
        }

        if (other.CompareTag("Boss"))   // 보스가 있으면. 상자 + 잡몹 삭제
        {
            BossIn = true;
            Destroy(blessBoxPrefab);
            Destroy(monsterPrefab);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어가 해당 Room에 들어 왔을경우, 몬스터 가시화
            MonsterAppear();
        }

        if (other.CompareTag("Boss"))
        {
            BossIn = true;
            //itemBoxPrefab.SetActive(false);
            teleportPrefab.SetActive(false);
            //Monster.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerIn = false;
        }

        if (other.CompareTag("Boss"))
        {
            teleportPrefab.SetActive(true);
            BossIn = false;
        }
    }
    private void MonsterAppear()
    {
        // 몬스터 가시화
        if (monsterPrefab != null)
        {
            monsterPrefab.SetActive(true);   // 몬스터 가시화
        }
        PlayerIn = true;
    }
}
