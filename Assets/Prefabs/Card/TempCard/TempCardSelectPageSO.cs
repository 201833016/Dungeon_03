using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Card Select Page", menuName = "Inventory/Card Select", order = 3)]
public class TempCardSelectPageSO : ScriptableObject
{
    public List<CardSelect> cardSelects;    // 고른 카드 정보 리스트
    [field: SerializeField] public int Size { get; private set; } = 3;  // 카드 선택 페이지에서 3장만 고를수 있도록
    public event Action<Dictionary<int, CardSelect>> OnCardDeckUpdated; // 내부 대리자 선언, 카드 덱 현재 상태


    public void InitializeTempCard()    // 카드 덱 초기화
    {
        cardSelects = new List<CardSelect>();
        for (int i = 0; i < Size; i++)
        {
            cardSelects.Add(CardSelect.GetEmptyCard()); // 빈 아이템 항목 가져오기
        }
    }

    public Dictionary<int, CardSelect> GetCurrentTempCardState()    // 카드 덱 상태 업데이트
    {
        Dictionary<int, CardSelect> returnCard = new Dictionary<int, CardSelect>();
        for (int i = 0; i < cardSelects.Count; i++)
        {
            if (cardSelects[i].IsEmpty)
            {
                continue;
            }
            returnCard[i] = cardSelects[i];
        }
        return returnCard;
    }

    internal CardSelect GetCardAt(int obj)  // 카드 덱에서 가져오기
    {
        return cardSelects[obj];    // 카드 선택
    }

    private void CardDeckStateUpdate()    // 상태 업데이트
    {
        OnCardDeckUpdated?.Invoke(GetCurrentTempCardState()); // 상태 업데이트
        Debug.Log("카드 덱: 상태 업데이트");
    }

}

[Serializable]
public struct CardSelect
{
    public CardSO card;
    public bool IsEmpty => card == null;
    public static CardSelect GetEmptyCard() => new CardSelect     // 빈 카드 항목 가져오기
    {
        card = null
    };
}
