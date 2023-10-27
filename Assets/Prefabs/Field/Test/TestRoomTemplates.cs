using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TestRoomTemplates : MonoBehaviour  // 오브젝트 : TestRoomTemplates
{    
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject player;
    public GameObject closedRoom;
    private float dist;
    [SerializeField] private TextMeshProUGUI distText;
    public List<GameObject> rooms;  // 생성되는 Rooms의 리스트

    public float waittime;
    private bool spawnedBoss;
    public int maxRooms = 20;
    public GameObject boss;

    //[HideInInspector] public Vector3 instZero;  // 보스 처리시 신상 등장

    private void Update() {
        if (waittime <= 0f && spawnedBoss == false)
        {
            boss = Instantiate(boss, rooms[rooms.Count -1].transform.position, Quaternion.identity);    // 보스 생성
            Debug.Log($"보스방 이름: {rooms[rooms.Count -1].name}"); // 보스가 놓일 Room을 부모로 삼기위한 확인
            boss.transform.parent = rooms[rooms.Count -1].transform;    // 보스가 놓일 Room에 Boss 오브젝트 넣기

            //instZero = rooms[rooms.Count -1].transform.position;
            spawnedBoss = true; // 보스 출현 확인
            
        }
        else
        {
            waittime -= Time.deltaTime;
        }
        
        if(waittime <= 0f)
        {
            if (boss != null)
            {
                dist = Vector2.Distance(player.transform.position, boss.transform.position);    // 보스와 플레이어의 거리
            }
            
        }

    }
    
    private void LateUpdate() {
        if (dist >= 300)
        {
            distText.text = $"보스와의 거리: 0";
        }
        else
        {
            float testA = dist / 20f;
            distText.text = $"보스와의 거리: {testA.ToString("F0")}";
        }
        
    }

}
