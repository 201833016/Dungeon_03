using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ATK_Increase", menuName = "Bless Card/Player Stat/ATK+")]
public class CardStatAtkIncreaseSO : CardStatModifierSO
{
    public override void AffectCharater(Health player, int val)
    {
        if (player != null)
        {
            player.AddAttack(val);
            Debug.Log($"공격력 [{val}] 증가");
        }
    }
}
