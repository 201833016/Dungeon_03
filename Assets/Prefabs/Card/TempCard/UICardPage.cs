using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardPage : MonoBehaviour
{
    [SerializeField] private TempUICard cardPrefab;    // 선택할 카드 프리팹 
    [SerializeField] private RectTransform contentPanel;    // 선택할 카드가 놓일 공간
    [SerializeField] private TempUICardTxt tempCardDesc;    // 선택한 카드 정보
    [SerializeField] private List<TempUICard> listCard = new List<TempUICard>();  //  선택할 카드를 가져올 덱 리스트
    public int selectIndex; // 선택한 카드의 순서 번호

    public event Action<int> OnCardDataRequested;   // 선택한 카드의 정보 이벤트

    private void Awake()
    {
        CardPageHide(); // 카드 선택 페이지 숨기기
        tempCardDesc.TempCardResetDescription();    // 선택한 카드 정보 숨기기
    }

    public void InitializeTempCardUI(int invenCardSize)     // 선택 카드 초기화
    {
        for (int i = 0; i < invenCardSize; i++)
        {
            TempUICard uiTemp = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity); // 선택한 카드 생성
            uiTemp.transform.localScale = new Vector3(1f, 1f, 1f);    // 선택할 카드 크기 조절 [xyz비율 : 70%]
            uiTemp.transform.SetParent(contentPanel);   // 선택할 카드가 놓일 오브젝트
            listCard.Add(uiTemp);   // 선택할 카드의 리스트

            uiTemp.OnItemClicked += HandleCardSelection;    // 카드 클릭시 효과
            uiTemp.OnRightMouseBtnClick += HandleShowItemActions;   // 우클릭시 카드 액션효과
        }
    }

    private void HandleCardSelection(TempUICard card)   // 카드 선택시 정보 주기
    {
        int index = listCard.IndexOf(card);
        if (index == -1)
        {
            return;
        }
        OnCardDataRequested?.Invoke(index);
    }

    private void HandleShowItemActions(TempUICard card)
    {
        throw new NotImplementedException();
    }

    public void TempUpdateCardData(int cardIndex, CardSO cardSO, Sprite cardImage, string cardName, string cardDesc)
    {
        if (listCard.Count > cardIndex)
        {
            listCard[cardIndex].TempSetCard(cardSO, cardImage, cardName, cardDesc);
        }
    }

    public void CardPageShow()  // 카드 페이지 보이기
    {
        gameObject.SetActive(true);
        TempResetSelection();
    }

    private void TempResetSelection()   // 카드 선택 상태 숨기기
    {
        tempCardDesc.TempCardResetDescription();
        TempDeselectAllCards();
    }

    private void TempDeselectAllCards() // 카드 선택 효과 숨기기
    {
        foreach (TempUICard card in listCard)
        {
            card.TempDeselect();
        }
    }

    public void CardPageHide()  // 카드 페이지 숨기기
    {
        gameObject.SetActive(false);
    }

    internal void UpDateSelectDescription(int cardIndex, CardSO cardSO, Sprite cardImage, string cardName, string cardDescription)
    {
        tempCardDesc.TempCardSetDescription(cardSO, cardImage, cardName, cardDescription);
        TempDeselectAllCards();
        listCard[cardIndex].TempSelect();

        selectIndex = cardIndex;    // 카드 순서 번호 가져오기
    }
}
