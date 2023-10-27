using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMove : MonoBehaviour
{
    private Movement2D move2D;
    Transform player;

    private void Awake()
    {
        move2D = GetComponent<Movement2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update() {
        PlayerChase();
    }
    
    private void PlayerChase()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, move2D.moveSpeed * Time.deltaTime);
    }
}
