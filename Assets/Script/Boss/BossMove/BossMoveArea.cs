using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveArea : MonoBehaviour
{
    public static BossMoveArea instance;
    private void Awake() {
        instance = this;
    }
    Rigidbody2D rb;
    Transform parentObj;
    public bool playerIn = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        parentObj = GetComponentInParent<BossMove>().transform;
    }

    private void Update() {
        this.gameObject.transform.position = parentObj.position;    // 몬스터 인지 영역 유지
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 사정거리에 들어오면
        if (collision.CompareTag("Player"))
        {
            playerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 사정거리를 벗어나면
        /*          
        if (collision.CompareTag("Player"))     // 이 부분을 빼면 들어오는숙나 쫓아가기만하고 멈추지 않음
        {
            playerIn = false;
        }  */
    }
}
