using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class Item : MonoBehaviour
{
    [field: SerializeField] public ItemSO invenItem { get; private set; }   // 아이템
    [field: SerializeField] public int Quantity { get; set; } = 1;  // 아이템 기본 갯수
    [SerializeField] private float duration = 0.3f; // 아이템 지속 시간
    
    private void Start() 
    {
        GetComponent<SpriteRenderer>().sprite = invenItem.itemSprite;      // SO내의 저장된 이미지 가져오기
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }

    private IEnumerator AnimateItemPickup()
    {
        
        Vector3 startScale = transform.localScale;  // 아이템 오브젝트 현재 크기
        Vector3 endScale = Vector3.zero;        // 아이템 오브젝트 끝날때 크기
        float currentTime = 0;
        while(currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        //transform.localScale = endScale;
        Destroy(gameObject);
    }
}
