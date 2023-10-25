using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "Not need/monster Stat")]
public class SOMonster : ScriptableObject
{
    public float STR; // 공격력 계수
    public float END; // 방어력 계수
    public float AGI; // 속도 계수
    public float CON; // 생명력 계수

    public SOItemDropTable itemDropTable;   // 아이템 드랍 테이블 연결
}
