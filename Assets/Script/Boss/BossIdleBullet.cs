using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleBullet : MonoBehaviour
{
    public float speed = 10;
    public Player player;
    private float bulletDamage;
    
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        bulletDamage = GameObject.Find("Boss").GetComponent<Boss>().BossAttack;
    }
    void Update()
    {
        Vector3 dir = transform.up;
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!player.isHurt)
            {
                other.GetComponent<Player>().Reduce(bulletDamage);    //플레이어 HP깍기
            }
            
            Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
