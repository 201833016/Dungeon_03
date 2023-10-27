using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DEF_Increase", menuName = "Bless Card/Player Stat/DEF+")]
public class CardStatDefIncreaseSO : CardStatModifierSO
{
    public override void AffectCharater(Health player, int val)
    {
        if(player != null)
        {
            player.AddDefence(val);
            Debug.Log($"방어력 [{val}] 증가");
        }
    }
}
