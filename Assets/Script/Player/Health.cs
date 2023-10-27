using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Player Heatlth", menuName = "Player/Health")]
public class Health : ScriptableObject     // Player 스테이터스
{
    public float maxHP;    //최대 체력
    public float currentHP;   //  현재 체력
    public float attack;  // 플레이어 공격력
    public float defence; // 플레이어 방어력
    public float speedMove;   // 이동 속도
    public float speedAttack; // 공격 속도
    public int shootMode;   // 총알 갯수

    public float attackCnt;
    public float defenceCnt;

    private void Awake()
    {
        currentHP = maxHP;
    }


    public void AddHP(float heal) // 회복 아이템 사용시
    {
        currentHP += heal;
        if (currentHP >= maxHP)
        {
            currentHP = maxHP;  // 회복 오버 되면, 최대치 까지
        }
        Player.Instance.playerHPBar.UpdateHPBar(currentHP, maxHP);
        Player.Instance.PlayerStateUpdate();
    }

    public void AddMaxHealth(float item_HP)
    {
        maxHP += item_HP;
        currentHP += item_HP;
        Player.Instance.PlayerStateUpdate();
    }

    public void AddAttack(float item_Attack) // 공격력 증가 축복 습득시
    {
        attack += item_Attack;
        attackCnt += item_Attack;
        Player.Instance.PlayerStateUpdate();
    }

    public void AddDefence(float item_Defence) // 방어력 증가 축복 습득시
    {
        defence += item_Defence;
        defenceCnt += item_Defence;
        Player.Instance.PlayerStateUpdate();
    }

    public void AddSpeedMove(float item_Speed)  // 이동속도 증가 축복 습득시
    {
        speedMove += item_Speed;
        Player.Instance.PlayerStateUpdate();
    }

    public void ShootModeChange(int bless_shoot)
    {
        shootMode = bless_shoot;
        Player.Instance.PlayerStateUpdate();
    }

    private void Die()
    {
        Debug.Log("Died");
        // Destroy(gameobject);
        currentHP = maxHP;
    }
}