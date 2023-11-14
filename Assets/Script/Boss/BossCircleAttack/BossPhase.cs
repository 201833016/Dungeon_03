using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    CircleFire = 0,
}
public class BossPhase : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefeb;   // 총알 프리팹
    
    private void Awake()
    {
        
    }

    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }


    private IEnumerator CircleFire()
    {
        float attackRate = 2f;    // 공격 주기, 1초
        int count = 10;             // 총알 생성 개수
        float intervalAngle = 360 / count;  // 총알 사이의 각도
        float weightangle = 0;      // 가중 각도 (항상 똑같은 곳이 아니게)
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject clone = Instantiate(bulletPrefeb, transform.position, Quaternion.identity);
                float angle = weightangle + intervalAngle * i;

                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

                clone.GetComponent<Movement2D>().MoveTo(new Vector2(x, y));
            }

            weightangle += 3;

            yield return new WaitForSeconds(attackRate);
        }

    }
}
