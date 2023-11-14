using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu]
public abstract class CardSO : ScriptableObject
{
    public int ID => GetInstanceID();

    [field: SerializeField] public string cardName { get; set; }    // 카드 이름
    [field: SerializeField] public Sprite cardImage { get; set; }  // 카드 이미지
    [field: SerializeField][field: TextArea] public string cardDescription { get; set; }    // 카드 설명 내용
}

