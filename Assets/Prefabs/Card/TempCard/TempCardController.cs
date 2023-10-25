using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCardController : MonoBehaviour
{
    [SerializeField] private UICardPage cardUI; // 선택할 카드 페이지 
    [SerializeField] private TempCardSelectPageSO selectData;   // 카드 덱 SO
    public TempDrawSystem drawCard; // 선택한 카드 정보
    public List<CardSelect> initialCards = new List<CardSelect>();    // 카드 덱 정보 저장 리스트
    private TestLevel testLevel;    // 스테이지 레벨
    private void Awake() {
        testLevel = GameObject.Find("TestLevel").GetComponent<TestLevel>();
    }
    private void Start()
    {
        if (testLevel.level == 1)   // 게임 시작시 덱 상태 초기화
        {
            CardSaveAndReset.instance.CardInData();
        }
        CardShuffle();
        PrepareSelectCardUI();
    }


    private void CardShuffle()  // 시작하면 덱 리스트에서 카드 섞기
    {
        TempCardSelectPageSO cardShffle = ScriptableObject.CreateInstance<TempCardSelectPageSO>();  // 스크립트 오브젝트 인스턴스화

        selectData.cardSelects = ShuffleList(selectData.cardSelects);   // 카드 정보 리스트에서 셔플
        cardShffle.cardSelects = selectData.cardSelects;

    }

    public void TempSelectCardRemove(int index)     // 인벤토리에 넣은 카드는 덱 라스트에서 제거
    {
        TempCardSelectPageSO SelectCard = ScriptableObject.CreateInstance<TempCardSelectPageSO>();  // 스크립트 오브젝트 인스턴스화

        selectData.cardSelects.RemoveAt(index); // 카드 정보 리스트에서 제거
        SelectCard.cardSelects = selectData.cardSelects;
    }

    private void PrepareSelectCardUI()  // 시작시 카드 선택할 갯수, 이벤트 생성
    {
        cardUI.InitializeTempCardUI(selectData.Size);
        this.cardUI.OnCardDataRequested += HandleCardDataRequest;   // 카드 선택한거 가져오기
    }

    private void HandleCardDataRequest(int index)   // 선택한 카드의 정보 가져오기
    {
        CardSelect selectCard = selectData.GetCardAt(index);    // 해당 번호의 카드 정보 가져오기
        if (selectCard.IsEmpty)
        {
            return;
        }
        CardSO card = selectCard.card;
        cardUI.UpDateSelectDescription(index, card, card.cardImage, card.cardName, card.cardDescription);
        drawCard.tempTest = selectCard.card;    // 선택한 카드 SO정보 넘기기
        drawCard.selectNum = index; // 선택한 카드 순서 번호 넘기기
    }



    public void OpenCardPage()
    {
        if (cardUI.isActiveAndEnabled == false)
        {
            CardShuffle();
            //cardUI.CardPageShow();
            foreach (var card in selectData.GetCurrentTempCardState())
            {
                cardUI.CardPageShow();
                cardUI.TempUpdateCardData(card.Key, card.Value.card, card.Value.card.cardImage, card.Value.card.cardName, card.Value.card.cardDescription);
            }
            Time.timeScale = 0; // 일시정지
        }
        else
        {
            cardUI.CardPageHide();            
        }

    }


    private List<T> ShuffleList<T>(List<T> _list)   // SO내의 카드 섞기
    {
        int random1, random2;
        T temp;
        for (int i = 0; i < _list.Count; i++)
        {
            random1 = UnityEngine.Random.Range(0, _list.Count);
            random2 = UnityEngine.Random.Range(0, _list.Count);

            temp = _list[random1];
            _list[random1] = _list[random2];
            _list[random2] = temp;
        }
        return _list;
    }
}
