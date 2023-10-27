using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SPD_Increase", menuName = "Bless Card/Player Stat/SPD+")]
public class CardStatSpeedIncreaseSO : CardStatModifierSO
{
    public override void AffectCharater(Health player, int val)
    {
        if(player != null)
        {
            player.AddSpeedMove(val);
            Debug.Log($"이동속도 [{val}] 증가");
        }
    }
}
