using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "rainCheck", menuName = "Bless Card/Player Stat/Rain_Check")]
public class CardStaNot : CardStatModifierSO
{
    public override void AffectCharater(Health player, int val)
    {
        Debug.Log($"꽝! 다음기회에");
    }
}
