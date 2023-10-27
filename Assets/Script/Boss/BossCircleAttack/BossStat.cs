using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BossStat : ScriptableObject
{
    public float maxHP;    //최대 체력
    public float currentHP;   //  현재 체력
    public float attack;  // 보스 공격력
    public float defence; // 보스 방어력
    public float speedMove;   // 이동 속도
    public float speedAttack; // 공격 속도

    private void Awake()
    {
        currentHP = maxHP;
    }




}
