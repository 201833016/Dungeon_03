using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    //[SerializeField] private float damage = 1f;
    Player player;
    private float timer = 4f;    // 총알 유지 시간
    private float bulletDamage;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //bossbullet = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        bulletDamage = GameObject.Find("Boss").GetComponent<Boss>().BossAttack;
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

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)   // 총알 3초이상 유지 못하게
        {
            Destroy(gameObject);
        }
    }
}
