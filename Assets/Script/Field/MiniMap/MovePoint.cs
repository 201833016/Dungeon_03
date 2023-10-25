using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovePoint : MonoBehaviour  // 오브젝트 : MapFollwed 미니맵에 보여줄 오브젝트 따라서 이동
{
    public Transform target;    // 먼저 간 Map pos
    public bool playerArriveCheck;  // 카메라가 다음맵에 도착 완료를 확인, 도착 전까지 몬스터 이동, 총발사 금지
    public bool testAA;
    public void MoveDG()
    {
        transform.DOMove(target.position, 2);
        StartCoroutine(LogCheck());
    }

    private IEnumerator LogCheck()
    {
        playerArriveCheck = true;
        testAA = true;
        yield return new WaitForSeconds(2.5f);
        testAA = false;
        yield return new WaitForSeconds(1f);
        playerArriveCheck = false;
    }
}
