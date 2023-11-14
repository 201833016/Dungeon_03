using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapposMove : MonoBehaviour // 포탈타면 먼저 가있는 오브젝트
{
    [SerializeField] private MovePoint movePoint;

    public void TopMap()
    {
        Vector3 nowPosition = this.transform.position;  // mappos의 위치 초기화
        transform.position = new Vector3(nowPosition.x, nowPosition.y + 22f, nowPosition.z);  // 위치 이동
        movePoint.MoveDG();
    }

    public void BottomMap()
    {
        Vector3 nowPosition = this.transform.position;
        transform.position = new Vector3(nowPosition.x, nowPosition.y - 22f, nowPosition.z);  // 위치 이동
        movePoint.MoveDG();
    }

    public void LeftMap()
    {
        Vector3 nowPosition = this.transform.position;
        transform.position = new Vector3(nowPosition.x - 46f, nowPosition.y, nowPosition.z);  // 위치 이동
        movePoint.MoveDG();
    }

    public void RightMap()
    {
        Vector3 nowPosition = this.transform.position;
        transform.position = new Vector3(nowPosition.x + 46f, nowPosition.y, nowPosition.z);  // 위치 이동
        movePoint.MoveDG();
    }

    public void ZeroMap()
    {
        transform.position = new Vector3(0, 0, transform.position.z);  // 위치 이동
        movePoint.MoveDG();
    }
}
