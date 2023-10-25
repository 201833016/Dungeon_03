using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField] private Image itemImage;   // 아이템 이미지
        [SerializeField] private TMP_Text quantityTxt;  // 개수 텍스트
        [SerializeField] private Image borderImage; // 선택 표시 이미지
        public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnitemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;
        // 내부 대리자 선언
        // 아이템 클릭시, 아이템 드래그 놓기, 드래그 시작, 드래그 종료, 마우스 우측 클릭

        private bool empty = true;  // 아이템이 itemUI에 있는가를 확인

        private void Awake()
        {
            this.itemImage.gameObject.SetActive(false); // 인벤토리의 item데이터가 없어지면 비활성화
            //ResetData();    // 인벤토리 정보 초기화
            //Deselect();     // 아이템 비 선택 시
        }

        public void ResetData() // 인벤토리 정보 초기화
        {
            if (itemImage)
            {
                this.itemImage.gameObject.SetActive(false); // 인벤토리의 item데이터가 없어지면 비활성화
                empty = true;
            }

        }

        public void Deselect()  // 아이템 비 선택 시
        {
            if (itemImage)
            {
                borderImage.enabled = false;
            }
            
        }

        public void SetData(Sprite sprite, int quantity)    // itemUI에 아이템 데이터 가져오기
        {
            if (itemImage)
            {
                this.itemImage.gameObject.SetActive(true);
                this.itemImage.sprite = sprite;
                this.quantityTxt.text = quantity + "";
                empty = false;
            }            
        }

        public void Select()
        {
            if (itemImage)
            {
                borderImage.enabled = true; // 아이템을 선택 클릭시, 선택 표시 가시화
            }
            
        }


        public void OnPointerClick(PointerEventData eventData)  // 오브젝트에 마우스를 누르고 뗄떄 호출
        {
            if (eventData.button == PointerEventData.InputButton.Right) // 누른 포인터와 아이템 이 같으면
            {
                OnRightMouseBtnClick?.Invoke(this);     // null이 아니면 마우스 우측버튼 이벤트 호출
            }
            else
            {
                OnItemClicked?.Invoke(this);        // null이 아니면 아이템 선택시 이벤트 호출
            }
        }

        public void OnBeginDrag(PointerEventData eventData) // 아이템 드래그 시작시, 외부 인터페이스
        {
            if (empty)
            {
                return;
            }
            OnitemBeginDrag?.Invoke(this);  // null이 아니면 아이템 드래그 시작 이벤트 호출
        }

        public void OnEndDrag(PointerEventData eventData)   // 아이템 드래그 종료시, 외부 인터페이스
        {
            OnItemEndDrag?.Invoke(this);        // null이 아니면 아이템 드래그 종료시 이벤트 호출
        }

        public void OnDrop(PointerEventData eventData)  // 아이템 드래그 놓기, 외부 인터페이스
        {
            OnItemDroppedOn?.Invoke(this);      // null이 아니면 아이템 드래그 놓기 이벤트 호출
        }

        public void OnDrag(PointerEventData eventData)
        {

        }
    }

}
