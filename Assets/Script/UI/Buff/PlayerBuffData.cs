using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffData : MonoBehaviour
{
    public static PlayerBuffData instance;
    
    private void Awake()
    {
        instance = this;
    }
    public Health player_Health;
    public List<BaseBuff> onBuff = new List<BaseBuff>();
    public OriginalHealth original_Health;

    public float BuffChange(string type, float origin)
    {
        if (onBuff.Count > 0)
        {
            float temp = 0;
            for (int i = 0; i < onBuff.Count; i++)
            {
                if (onBuff[i].type.Equals(type))
                {
                    temp += origin * onBuff[i].percentage;
                }
            }
            return origin + temp;
        }
        else
        {
            return origin;
        }
    }

    public void ChooseBuff(string type)
    {
        switch (type)
        {
            case "ATK":
                player_Health.attack = BuffChange(type, player_Health.attackCnt);    // 공격력 아이템 사용시
                break;
            case "DEF":
                player_Health.defence = BuffChange(type, player_Health.defenceCnt);
                break;
            default:
                break;
        }
    }
}
