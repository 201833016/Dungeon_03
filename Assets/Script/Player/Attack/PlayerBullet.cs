using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Health playerData;
    private float bulletDamage; // 총알 데미지
    private float speed = 10f; // 총알 속도
    private float distance;  // 총알의 ray 길이
    public LayerMask isLayer;   // layer 구분 (벽, 근거리 몬스터, 원거리 몬스터)



    void Start()
    {
        Invoke("DestroyBullet", 2f); // 총알 유지 시간 2초 
    }

    void Update()
    {
        bulletDamage = playerData.attack;   // 총알 데미지 = 플레이어 공격력
        transform.Translate(Vector2.right * speed * Time.deltaTime);    // 총알 발사후 위치

        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if(ray.collider != null)    // raycast에 무언가 닿음녀
        {
            switch (ray.collider.tag)
            {
                case "Monster":
                    // MonsterA
                    ray.collider.GetComponent<Slime>().GetHit(bulletDamage);
                    break;
                case "MonsterCanon":
                    // MonsterCanon
                    ray.collider.GetComponent<Canon>().GetHit(bulletDamage);
                    break;
                case "Monster_C":
                    // MonsterCanon
                    ray.collider.GetComponent<MonsterC>().GetHit(bulletDamage);
                    break;                    
                case "Boss":
                    ray.collider.GetComponent<Boss>().Reduce(bulletDamage);
                    break;
                case "HurdleObj":
                    ray.collider.GetComponent<AttackObj>().GetHit(1);
                    break;
                default:
                    break;
            }

            if(ray.collider.tag != "Player")    // 총알이 플레이어에 안 막히게
            {
                DestroyBullet();
            }
        }
    }

    void DestroyBullet()    // 총알이 닿으면 파괴
    {
        Destroy(gameObject);
    }


}
