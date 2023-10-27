using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player") && !other.CompareTag("Maps") 
            && !other.CompareTag("Boss") && !other.CompareTag("Wall") 
            && !other.CompareTag("MapFollwed") && !other.CompareTag("Monster")
            && !other.CompareTag("MonsterBullet") && !other.CompareTag("Box"))
        {
            // 해당 tag를 제외한 나머지만 지우기
            Destroy(other.gameObject);  
        }
    }
}
