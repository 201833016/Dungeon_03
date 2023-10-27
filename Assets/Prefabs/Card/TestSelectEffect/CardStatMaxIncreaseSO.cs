using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaxHP Increase", menuName = "Bless Card/Player Stat/Max HP+")]
public class CardStatMaxIncreaseSO : CardStatModifierSO
{
    public override void AffectCharater(Health player, int val)   // 효과 추상 클래스 오버라이드
    {
        if(player != null)
        {
            player.AddMaxHealth(val);
            Debug.Log($"최대 체력 [{val}] 증가");
        }
    }
}
