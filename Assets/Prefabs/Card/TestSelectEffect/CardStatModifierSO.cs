using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardStatModifierSO : ScriptableObject
{
    public abstract void AffectCharater(Health player, int val);  // 카드 효과 추상 클래스 

}
