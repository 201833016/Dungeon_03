using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomSpawn : MonoBehaviour
{
    public int openingDir;

    private TestRoomTemplates templates;
    private int rand;
    private bool spawned = false;
    public int roomCount;
    private float waitTime = 4f;
    private void Start()
    {
        Destroy(gameObject, waitTime);  // Spawnpoint 전부 삭제
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<TestRoomTemplates>();
        Invoke("Spawn", 0.2f);
    }

    private void Spawn()
    {
        if (spawned == false)
        {
            if (roomCount <= templates.maxRooms)
            {
                if (openingDir == 1)
                {
                    // 아래 방 연결
                    rand = Random.Range(0, templates.bottomRooms.Length);   // 방연결이 가능한 배열 중 랜덤
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);   // 방 생성
                }
                else if (openingDir == 2)
                {
                    // 위 방 연결
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                }
                else if (openingDir == 3)
                {
                    // 왼쪽 방 연결
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                }
                else if (openingDir == 4)
                {
                    // 오른쪽 방 연결
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                }
                spawned = true;
                roomCount++;
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<TestRoomSpawn>().spawned == false && spawned == false)
            {
                templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<TestRoomTemplates>();
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }

    }
}
