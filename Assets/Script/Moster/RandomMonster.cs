using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMonster : MonoBehaviour
{
    public GameObject[] monster;
    public Transform spawnPos;
    [SerializeField] private MapPlayerIn playerCheck;
    [SerializeField]private GameObject monsterObj;
    private int RandomNum;
    private GameObject sumonMonster;
    private void Awake() {
        //playerCheck = GameObject.FindGameObjectWithTag("Maps").GetComponent<MapPlayerIn>();
        //monsterObj = GameObject.Find("Monster").GetComponent<MonsterCount>();
        RandomNum = Random.Range(0, 4); // 0 ~ 3
    }
    private void Update() 
    {
        if (playerCheck.PlayerIn)
        {
            RandomSpawn();
        }
        
    }

    public void RandomSpawn()
    {
            switch (RandomNum)
            {
                case 0:
                    // Debug.Log("Slime 소환");
                    sumonMonster = Instantiate(monster[RandomNum], spawnPos.position, Quaternion.identity);
                    //sumonMonster.transform.parent = this.transform;
                    sumonMonster.transform.SetParent(monsterObj.transform, true);
                    break;
                case 1:
                    // Debug.Log("Canon 소환");
                    sumonMonster = Instantiate(monster[RandomNum], spawnPos.position, Quaternion.identity);   // 총알 clone생성 (이미지, 발사위치, 회전)
                    sumonMonster.transform.SetParent(monsterObj.transform, true);
                    break;
                case 2:
                    // Debug.Log("MoveShoot 소환");
                    sumonMonster = Instantiate(monster[RandomNum], spawnPos.position, Quaternion.identity);
                    sumonMonster.transform.SetParent(monsterObj.transform, true);
                    break;                
                default:
                    // Debug.Log("땡, 몬스터 소환 실패");
                    break;
            }
            Destroy(gameObject);
    }
}
