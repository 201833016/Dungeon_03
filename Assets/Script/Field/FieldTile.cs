using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTile : MonoBehaviour
{
    public static FieldTile instance;
    private void Awake() {
        FieldTile.instance = this;
    }
    private SpriteRenderer spriteRenderer; // 현재 바닥
    [SerializeField] private Sprite[] sprite; // 바꿀 바닥
    [SerializeField] private Material[] material; // 바꿀 바닥 재질
    private int num;

    public void Start_Floor(SpriteRenderer spR)
    {
        num = Random.Range(0, sprite.Length);   // 0 ~ 3
        spR.sprite = sprite[num];
        spR.material = material[num];
        //Debug.Log("바닥 타일 바꾸는 중");
    }
}
