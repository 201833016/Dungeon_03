using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBox : MonoBehaviour
{
    public static TestBox instance;

    private void Awake()
    {
        instance = this;
    }
    private Player player;
    private Animator animator;
    public bool playerCheck;
    private TempCardController tempCard;
    [SerializeField] private int RandomNum;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        tempCard = GameObject.Find("CardManger").GetComponent<TempCardController>();   
        RandomNum = Random.Range(0, 2); // 0 ~1
        if (RandomNum == 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (playerCheck && Input.GetKeyDown(KeyCode.B))
        {
            AnimateBoxOpen();
            Invoke("OpenPage", 0.2f);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCheck = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCheck = false;
        }
    }

    public void AnimateBoxOpen()
    {
        animator.SetBool("testOpen", true);
    }

    private void OpenPage()
    {
        tempCard.OpenCardPage();
        Destroy(gameObject);
    }


}
