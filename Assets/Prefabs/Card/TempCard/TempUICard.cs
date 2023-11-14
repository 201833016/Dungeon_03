using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TempUICard : MonoBehaviour, IPointerClickHandler
{
    public CardSO cardSO;
    [SerializeField] private Image cardImage;   // 카드 이미지
    [SerializeField] private TMP_Text cardName; // 카드 이름
    [SerializeField] private TMP_Text TempCardDesc; // 카드 설명
    [SerializeField] private Image borderImage; // 선택 표시 이미지

    public event Action<TempUICard> OnItemClicked, OnRightMouseBtnClick;
    // 내부 대리자 선언
    // 아이템 클릭시, 아이템 드래그 놓기, 드래그 시작, 드래그 종료, 마우스 우측 클릭

    //private bool empty = true;  // 아이템이 itemUI에 있는가를 확인

    private void Awake()
    {
        TempResetCard();    // 카드 이미지 비활성화
        TempDeselect();     // 카드 선택 효과 비화성화
    }

    public void TempDeselect()  // 선택 효과 비활성화
    {
        borderImage.enabled = false;
    }

    public void TempResetCard()
    {
        this.cardImage.gameObject.SetActive(false); // 인벤토리의 카드 데이터가 없어지면 비활성화
        //empty = true;
    }

    public void TempSetCard(CardSO cardSO, Sprite sprite, string nametxt, string desctxt)    // 선택한 카드에 카드 데이터 가져오기
    {
        this.cardSO = cardSO;
        this.cardImage.gameObject.SetActive(true);
        this.cardImage.sprite = sprite;
        this.cardName.text = nametxt + "";
        this.TempCardDesc.text = desctxt + "";
        //empty = false;
    }

    public void TempSelect()    // 카드 선택 효과 활성화
    {
        borderImage.enabled = true; // 카드를 선택 클릭시, 선택 표시 가시화
    }

    public void OnPointerClick(PointerEventData eventData)  // 오브젝트 클릭시 이벤트
    {
        if (eventData.button == PointerEventData.InputButton.Right) // 누른 포인터와 카드가 같으면
        {
            OnRightMouseBtnClick?.Invoke(this);     // null이 아니면 마우스 우측버튼 이벤트 호출
        }
        else
        {
            OnItemClicked?.Invoke(this);        // null이 아니면 카드 선택시 이벤트 호출
        }
    }


}
