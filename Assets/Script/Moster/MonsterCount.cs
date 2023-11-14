using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCount : MonoBehaviour
{
    [Header("아이템 상자 ")] public GameObject itemBox;

    [Header("포탈 ")] public GameObject mapPotal;
    public int monsterCount;
    private void Start()
    {
        if (itemBox != null)
        {
            itemBox.SetActive(false);
        }
        if (mapPotal != null)
        {
            mapPotal.SetActive(false);
        }
        monsterCount = this.transform.childCount;

    }

    private void Update()
    {
        monsterCount = this.transform.childCount;
        if (monsterCount == 0)
        {
            //Destroy(gameObject);
            if (itemBox != null)
            {
                itemBox.SetActive(true);    // 아이템 상자 활성화
            }
            if (mapPotal != null)
            {
                mapPotal.SetActive(true);  // 이동 포탈 활성화
            } 
        }
    }
}
