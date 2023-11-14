using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOpen : MonoBehaviour
{
    Player player;
    private Animator animator;
    public bool isPlayEnter;  // 플레이어 접근 확인

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        isPlayEnter = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("testOpen", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayEnter = true;
            Debug.Log($"{isPlayEnter}");
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayEnter = false;
        }
    }

    private void DestroyBox()
    {
        Destroy(gameObject);  // itemBox를 true하는데서 오류남
    }
}
