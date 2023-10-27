using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Inventory ", menuName = "Inventory/Card Inventory", order = 2)]
public class BlessCardInven : ScriptableObject
{
    [SerializeField] public List<BlessCard> invenCards;

 
    public event Action<Dictionary<int, BlessCard>> OnTestCardUpdated; // 내부 대리자 선언, 인벤토리 현재 상태

    public void InitializeCard()
    {
        invenCards = new List<BlessCard>();

        for (int i = 0; i < 20; i++)
        {
            invenCards.Add(BlessCard.GetEmptyCard());   // 빈 카드 항목 가져오기
            
        }
    }


    /*
    public void AddCard(CardSO card)
    {
        for (int i = 0; i < invenCards.Count; i++)
        {
            if (invenCards[i].IsEmpty)
            {
                invenCards[i] = new BlessCard
                {
                    card = card
                };
            }
        }
    }*/

    public int TestAddCard(CardSO card, int quantity)   // 인벤토리에 카드 추가
    {
        for (int i = 0; i < invenCards.Count; i++)
        {
            while (quantity > 0 && TestIsCardFull() == false)
            {
                quantity -= AddCardToFirstFreeSlot(card, 1); // 개수 1개 감소

            }
        }
        InformAboutChange();
        return quantity;
    }

    private void InformAboutChange()    // 상태 업데이트
    {
        OnTestCardUpdated?.Invoke(GetCurrentTestCardState()); // 상태 업데이트
        //Debug.Log("05: 상태 업데이트");
    }

    private int AddCardToFirstFreeSlot(CardSO card, int quantity)
    {
        BlessCard newCard = new BlessCard
        {
            card = card,
            quantity = quantity,
        };

        for (int i = 0; i < invenCards.Count; i++)
        {
            if (invenCards[i].IsEmpty)  // 아이템 리스트가 비었다면
            {
                invenCards[i] = newCard;    // 그자리에 아이템 정보 추가
                return quantity;
            }
        }
        return 0;
    }

    public Dictionary<int, BlessCard> GetCurrentTestCardState()    // 상태 업데이트
    {
        Dictionary<int, BlessCard> returnValue = new Dictionary<int, BlessCard>();
        for (int i = 0; i < invenCards.Count; i++)
        {
            if (invenCards[i].IsEmpty)
            {
                continue;
            }
            returnValue[i] = invenCards[i];
        }
        return returnValue;
    }

    private bool TestIsCardFull() => invenCards.Where(card => card.IsEmpty).Any() == false;

    public Dictionary<int, BlessCard> GetCurrentBlessCardState()    // 상태 업데이트
    {
        Dictionary<int, BlessCard> returnCard = new Dictionary<int, BlessCard>();
        for (int i = 0; i < invenCards.Count; i++)
        {
            if (invenCards[i].IsEmpty)
            {
                continue;
            }
            returnCard[i] = invenCards[i];
        }
        return returnCard;
    }
}

[Serializable]
public struct BlessCard
{
    public CardSO card;
    public int quantity;
    public bool IsEmpty => card == null;

    public static BlessCard GetEmptyCard() => new BlessCard     // 빈 카드 항목 가져오기
    {
        card = null
    };
}
