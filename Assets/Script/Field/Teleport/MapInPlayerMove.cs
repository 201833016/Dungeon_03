using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapInPlayerMove : MonoBehaviour
{
    Player player;
    MapposMove mappos;  // 맵이동시 카메라 이동
    MinimapPos miniPoint;    // 미니 맵 표시
    MovePoint movePoint;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();  // 플레이어 정보 가져오기
        mappos = GameObject.Find("MapPos").GetComponent<MapposMove>();
        movePoint = GameObject.FindGameObjectWithTag("MapFollwed").GetComponent<MovePoint>();
        miniPoint = GameObject.FindGameObjectWithTag("MiniPos").GetComponent<MinimapPos>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !movePoint.playerArriveCheck) // 플레이어와 텔레포트가 부딪혔을 경우
        {
            switch (this.gameObject.name)   // 해당 텔레포트의 이름을 가져옴
            {
                case "Teleport_Top":
                    // 이거를 TempTest의 PlayerMoveDotween을 Player에세 주고 PlayerMovePoint를 가져오기
                    Vector3 TP_top = player.transform.position;   // Player 위치 초기화
                    player.transform.position = new Vector3(TP_top.x, TP_top.y + 16f, TP_top.z); // Player 위치 이동

                    mappos.TopMap();   // 맵 위치 카메라 대상 위치 이동
                    miniPoint.MapMovePoint_Up();    // 미니맵 이동
                    break;
                case "Teleport_Bottom":
                    Vector3 TP_bot = player.transform.position;   // Player 위치 초기화
                    player.transform.position = new Vector3(TP_bot.x, TP_bot.y - 16f, TP_bot.z); // Player 위치 이동

                    mappos.BottomMap();   // 맵 위치 카메라 대상 위치 이동
                    miniPoint.MapMovePoint_Down();  // 미니맵 이동
                    break;
                case "Teleport_Left":
                    Vector3 TP_left = player.transform.position;
                    player.transform.position = new Vector3(TP_left.x - 28f, TP_left.y, TP_left.z); // Player 위치 이동

                    mappos.LeftMap();   // 맵 위치 카메라 대상 위치 이동
                    miniPoint.MapMovePoint_Left();  // 미니맵 이동
                    break;
                case "Teleport_Right":
                    Vector3 TP_rht = player.transform.position;
                    player.transform.position = new Vector3(TP_rht.x + 28f, TP_rht.y, TP_rht.z);    // Player 위치 이동

                    mappos.RightMap();   // 맵 위치 카메라 대상 위치 이동
                    miniPoint.MapMovePoint_Right(); // 미니맵 이동
                    break;
                case "Teleport_Zero":
                    player.transform.position = new Vector3(0, 0, player.transform.position.z);    // Player 위치 이동

                    mappos.ZeroMap();
                    miniPoint.MapMovePoint_Zero();
                    break;
                default:
                    break;
            }
        }
    }
}
