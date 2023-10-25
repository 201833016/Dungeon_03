using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDrawSystem : MonoBehaviour
{
    [SerializeField] private BlessCardInven saveCardData;   // 카드 인벤토리
    public CardSO tempTest; // 선택한 카드 정보
    public int selectNum;   // 선택한 카드의 리스트 순서 번호
    [SerializeField] private TempCardSelectPageSO cardSelectData; // 카드 선택 효과를 위한 정보
    [SerializeField] private Health health; // 카드 효과 대상이 될 플레이어
    [SerializeField] private UICardPage cardUI; // 선택할 카드 페이지 
    [SerializeField] private Player player;

    public void DarwCard()
    {
        if (tempTest != null)
        {
            Debug.Log($"{tempTest.cardName} 을 카드 인벤에 넣음");
            saveCardData.TestAddCard(tempTest, 1);  // 카드 인벤토리에 선택한 카드 넣기
            CardEffectAction(); // 카드 효과 발동
            RemoveCardInList(); // 선택한 카드를 덱 리스트에서 제거
            cardUI.CardPageHide();
            Time.timeScale = 1f;    // 일시정지 해제
        }
        else
        {
            Debug.Log("고르고 눌르길");
        }
    }

    public void RemoveCardInList()  // 선택한 카드를 덱 리스트에서 제거
    {
        TempCardController DeleteCard = GetComponent<TempCardController>();
        DeleteCard.TempSelectCardRemove(selectNum);    
    }

    public void CardEffectAction()  // 카드 효과 발동
    {
        AcceptCard(selectNum);
        player.PlayerStateUpdate();
    }

    public void AcceptCard(int index)   // 카드 선택후 버튼 클릭 => 효과 발동
    {
        CardSelect cardEEE = cardSelectData.GetCardAt(index);   // 카드 덱에서 가져오기
        if(cardEEE.IsEmpty)
        {
            return; // 카드가 없으면 불발
        }
        ICardAction cardAction = cardEEE.card as ICardAction;   // 카드 효과 이벤트
        if(cardAction != null)
        {
            cardAction.PerformAction(health);   // 플레이어 대상으로 카드 효과 발동
        }
    }
    

}


