using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;
    //EnemyMoveZone zone;
    [Header("목표와의 간격")][SerializeField][Range(0f, 6f)] float contactDistance = 3f;   // 목표와 거리가 이정도 가까워 지면 멈춤
    private Movement2D move2D;
    private MovePoint check;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        check = GameObject.FindGameObjectWithTag("MapFollwed").GetComponent<MovePoint>();
        move2D = GetComponent<Movement2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        EnemyFollowTarget();
    }

    private void LateUpdate() {
        spriteRenderer.flipX = target.position.x > rb.position.x;   // 플레이어 x좌표와 비교하여 좌우 반전
    }

    private void EnemyFollowTarget()
    {
        if (!check.playerArriveCheck)
        {
            if (Vector2.Distance(transform.position, target.position) > contactDistance)    //&& playerIn
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, move2D.moveSpeed * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }

    }

}
