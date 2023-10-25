using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPos : MonoBehaviour
{
    [SerializeField] private List<GameObject> Room_1;   // 문 1개
    [SerializeField] private List<GameObject> Room_2;   // 문 2개
    [SerializeField] private GameObject Closed_Room;

    private GameObject miniObj; // 생성할 미니맵 오브젝트
    [SerializeField] private GameObject parentObj;  // 부모 오브젝트


    public void MinimapOne(int roomIndex)    // swich case로 문 1개 연결
    {
        switch (roomIndex)
        {
            case 0:     // Room_B
                MinimapPointOne(0);
                break;
            case 1:     // Room_L
                
                MinimapPointOne(1);
                break;
            case 2:     // Room_R
                
                MinimapPointOne(2);
                break;
            case 3:     // Room_T
                MinimapPointOne(3);
                break;
            default:
                break;
        }
    }

    public void MinimapTwo(int roomIndex)    // swich case로 문 2개 연결
    {
        switch (roomIndex)
        {
            case 0:     // Room_LB
                MinimapPointTwo(0);
                break;
            case 1:     // Room_LR
                MinimapPointTwo(1);
                break;
            case 2:     // Room_LT          
                MinimapPointTwo(2);
                break;
            case 3:     // Room_RB              
                MinimapPointTwo(3);
                break;
            case 4:     // Room_RT
                MinimapPointTwo(4);
                break;
            case 5:     // Room_TB
                MinimapPointTwo(5);
                break;
            default:
                break;
        }
    }

    public void MapMovePoint_Up()    // 위로 이동
    {
        Vector3 TestPosition = this.transform.position;   // Player 위치 초기화
        this.transform.position = new Vector3(TestPosition.x, TestPosition.y + 10f, TestPosition.z); // Player 위치 이동
    }

    public void MapMovePoint_Right()    // 오른쪽으로 이동
    {
        Vector3 TestPosition = this.transform.position;   // Player 위치 초기화
        this.transform.position = new Vector3(TestPosition.x + 10f, TestPosition.y, TestPosition.z); // Player 위치 이동
    }

    public void MapMovePoint_Left()    // 왼쪽으로 이동
    {
        Vector3 TestPosition = this.transform.position;   // Player 위치 초기화
        this.transform.position = new Vector3(TestPosition.x - 10f, TestPosition.y, TestPosition.z); // Player 위치 이동
    }

    public void MapMovePoint_Down()    // 아래로 이동
    {
        Vector3 TestPosition = this.transform.position;   // Player 위치 초기화
        this.transform.position = new Vector3(TestPosition.x, TestPosition.y - 10f, TestPosition.z); // Player 위치 이동
    }

    public void MapMovePoint_Zero()
    {
        this.transform.position = new Vector3(-500f, -500f, this.transform.position.z); // Player 위치 이동
    }

    private void MinimapPointOne(int roomIndex) // 문 1개 미니맵 생성
    {
        miniObj = Instantiate(Room_1[roomIndex], this.transform.position, Quaternion.identity);  // 오브젝트 생성
        miniObj.transform.parent = parentObj.transform; // 자식 오브젝트로 집어 넣기
    }

    private void MinimapPointTwo(int roomIndex) // 문 2개 미니맵 생성
    {
        miniObj = Instantiate(Room_2[roomIndex], this.transform.position, Quaternion.identity);  // 오브젝트 생성
        miniObj.transform.parent = parentObj.transform; // 자식 오브젝트로 집어 넣기
    }

    public void MinimapPointClose()
    {
        miniObj = Instantiate(Closed_Room, this.transform.position, Quaternion.identity);  // 오브젝트 생성
        miniObj.transform.parent = parentObj.transform; // 자식 오브젝트로 집어 넣기
    }
}
