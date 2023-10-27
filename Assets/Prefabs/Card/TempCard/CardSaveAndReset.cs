using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSaveAndReset : MonoBehaviour
{
    public static CardSaveAndReset instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] private TempCardSelectPageSO useCardList; // 사용될 카드 덱
    [SerializeField] private TempCardSelectPageSO saveCardList; // 사용되지 않을 덱

    public void CardInData()  // 저장 카드 덱에서 사용 카드덱 에 카드 집어 넣기 [삭제요망]
    {
        useCardList.cardSelects = new List<CardSelect>();
        for (int i = 0; i < saveCardList.cardSelects.Count; i++)
        {
            useCardList.cardSelects.Add(saveCardList.cardSelects[i]);   // 저장용 덱에서 값 집어 넣기
        }
    }
    /*
    public void InitializeCardList()    // 카드 덱 초기화   [삭제요망]
    {
        useCardList.cardSelects = new List<CardSelect>();   // 사용하던카드 리스트 초기화
    }

    public void CardResetDeck() // 카드 덱 초기화
    {
        useCardList.cardSelects = new List<CardSelect>();   // 사용하던카드 리스트 초기화
        for (int i = 0; i < saveCardList.cardSelects.Count; i++)
        {
            useCardList.cardSelects.Add(saveCardList.cardSelects[i]);   // 저장용 덱에서 값 집어 넣기
        }
    }
    */
}
