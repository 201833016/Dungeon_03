using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackObj : MonoBehaviour
{
    private int randomIndex;
    [SerializeField] private TextMeshProUGUI countIndex;

    private void Awake()
    {
        RandomDraw();
    }
    private void Update() {
        countIndex.text = $"{randomIndex}";
    }

    public void RandomDraw()
    {
        randomIndex = Random.Range(3, 10);
        countIndex.text = $"{randomIndex}";
    }

    public int GetHit(int count)   // 장애물 피격시
    {
        randomIndex -= count;
        if (randomIndex <= 0)
        {
            Destroy(gameObject);
        }
        return randomIndex;
    }

}
