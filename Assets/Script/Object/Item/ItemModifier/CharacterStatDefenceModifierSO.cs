using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Defence Plus", menuName = "item Effect/Defence Plus")]
public class CharacterStatDefenceModifierSO : ChracterStatModifierSO
{
    public string type;
    public float per;
    public float duration;
    public Sprite icon;

    public override void AffectCharacter(Health health, float val)   // 캐릭터 상태 추상 클래스 오버라이드
    {
        //Health health = character.GetComponent<Health>();
        if(health != null)
        {
            //health.AddAttack(val); // 공격력 증가 효과
            BuffManager.instance.CreateBuff(type, per, duration, icon);
            //TestTimeCheck.instance.TextTime(type, per, duration);
        }
    }
}
