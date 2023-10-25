using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Plus", menuName = "item Effect/Attack Plus")]
public class CharacterStatAttackModifierSO : ChracterStatModifierSO
{
    public string type; // 버프 종류
    public float per;   // 받아올 실 변수
    public float duration;  // 버프 총 시간
    public Sprite icon; // 버프 아이콘

    public override void AffectCharacter(Health health, float val)   // 캐릭터 상태 추상 클래스 오버라이드
    {
        // 공격력 증가 효과
        if(health != null)
        {
            BuffManager.instance.CreateBuff(type, per, duration, icon); // 버프 생성
        }
    }
}
