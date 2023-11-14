using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerButton : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private MinimapPos miniPos;
    [SerializeField] MapposMove mappos;  // 맵이동시 카메라 이동
    Dummy target;

    public void SkipBoss_Buttom()
    {
        target = GameObject.Find("Square").GetComponent<Dummy>();
        target.Skip_Motion(player);
        Time.timeScale = 1f;    // 일시정지 해제
    }

    public void ZeroPoint_Buttom()
    {
        player.transform.position = new Vector3(0, 0, player.transform.position.z);    // Player 위치 이동
        mappos.ZeroMap();
        miniPos.MapMovePoint_Zero();
    }
}
