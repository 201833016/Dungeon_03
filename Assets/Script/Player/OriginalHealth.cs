using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Original Heatlth", menuName = "Player/Original Health")]
public class OriginalHealth : ScriptableObject
{
    public float maxHP;    //최대 체력
    public float currentHP;   //  현재 체력
    public float attack;  // 플레이어 공격력
    public float defence; // 플레이어 방어력
    public float speedMove;   // 이동 속도
    public float speedAttack; // 공격 속도

    public void ChangeOriginMaxHP(float val)
    {
        maxHP += val;
    }
}
