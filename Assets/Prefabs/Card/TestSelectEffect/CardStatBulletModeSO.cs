using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Bullet Mode", menuName = "Bless Card/Player Stat/Bullet+")]
public class CardStatBulletModeSO : CardStatModifierSO
{
    public override void AffectCharater(Health player, int val)   // 효과 추상 클래스 오버라이드
    {
        if(player != null)
        {
            player.ShootModeChange(val);
            Debug.Log($"총알 발사 [{val}]개 ");
        }
    }
}
