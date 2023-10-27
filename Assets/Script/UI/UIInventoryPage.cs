using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem itemPrefab;    // 아이템UI 프리팹 
        [SerializeField] private RectTransform contentPanel;    // 아이템 UI가 놓일 공간
        [SerializeField] private UIInventoryDescription itemDescription;    // 아이템 설명창
        [SerializeField] private MouseFollwer mouseFollwer; // 마우스 포인터
        List<UIInventoryItem> listofUIItems = new List<UIInventoryItem>();  //  아이템 리스트
        public int selectItemSlotNum;   // 인벤토리 슬롯중 클릭한 아이템의 위치

        private int currentlyDraggedItemIndex = -1; // 드래그 하는 아이템을 위한 index


        public event Action<int> OnDesCriptionRequested, OnItemActionRequested, OnStartDragging;
        // 내부 대리자 선언
        // 아이템 설명, 아이템 이벤트, 드래그 시작
        public event Action<int, int> OnSwapItems;  // 아이템 드래그 교체시 리스트 번호 

        private void Awake()
        {
            Hide(); // 인벤토리 숨기기
            mouseFollwer.Toggle(false); // 드래그 교체 이미지 비활성화
            itemDescription.ResetDescription(); // 아이템 설명창 비활성화
        }

        public void InitializeInventoryUI(int invenSize)    // 인벤토리의 아이템 칸 생성
        {
            for (int i = 0; i < invenSize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listofUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnitemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;   // 우클릭시 아이템 액션효과
            }
        }

        public void UpDateData(int itemIndex, Sprite itemImage, int itemQuantity)   // 인벤토리 아이템 데이터 상시 업데이트
        {
            if (listofUIItems.Count > itemIndex)
            {
                listofUIItems[itemIndex].SetData(itemImage, itemQuantity);  // itemUI에 아이템 데이터 가져오
            }
        }

        private void HandleShowItemActions(UIInventoryItem item)    // ㅏ아이템 우클릭시
        {
            int index = listofUIItems.IndexOf(item);    // 드래그 하려는 아이템의 순서를 아이템 리스트에서 가져옴 
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);   // null이 아니면 아이템 이벤트 호출
        }

        private void HandleEndDrag(UIInventoryItem item)    // 아이템 토글 종료시
        {
            ResetDraggedItem(); // 마우스 포인터 초기화
        }

        private void HandleSwap(UIInventoryItem itemUI)
        {
            int index = listofUIItems.IndexOf(itemUI);
            if (index == -1)
            {

                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);  // null이 아니면 아이템 교체 이벤트 호출
            HandleItemSelection(itemUI);    // 해당 아이템 클릭
        }

        private void HandleBeginDrag(UIInventoryItem item)  // 아이템 토글 시, 반투명하게 
        {
            int index = listofUIItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            currentlyDraggedItemIndex = index;
            HandleItemSelection(item);
            OnStartDragging?.Invoke(index);

        }

        private void HandleItemSelection(UIInventoryItem item)  // 인벤토리 슬롯(UIitem) 클릭
        {
            int index = listofUIItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            OnDesCriptionRequested?.Invoke(index);  // null이 아니면 아이템 설명 이벤트 호출

        }

        public void Show()  // 인벤토리 보이기
        {
            gameObject.SetActive(true);
            //ResetSelection();   // 아이템UI 선택 초기화

        }
        public void Hide()  // 인벤토리 숨기기
        {
            gameObject.SetActive(false);
            ResetDraggedItem(); // 마우스 포인터 초기화
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)  // 마우스 포인터에 아이템 이미지, 개수 가져오기
        {
            mouseFollwer.Toggle(true);
            mouseFollwer.SetData(sprite, quantity);
        }

        private void ResetDraggedItem() // 마우스 포인터 초기화
        {
            mouseFollwer.Toggle(false);
            currentlyDraggedItemIndex = -1; // 드래그 변수 초기화
        }

        public void ResetSelection()    // 아이템UI 선택 초기화
        {
            itemDescription.ResetDescription(); // 아이템 설명창 비활성화
            BuffUseYN.instance.AllEnabledFalse();   // 버프 아이템 시간 비활성화
            DeselectAllItems(); // 아이템 미선택시 비활성화 
        }

        private void DeselectAllItems() // 아이템 선택 표시 비활성화 
        {
            foreach (UIInventoryItem item in listofUIItems)
            {
                item.Deselect();    // 아이템 선택 표시 초기화
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite sprite, string name, string description)
        {
            itemDescription.SetDescription(sprite, name, description);  // 아이템 설명창에 가져온 정보 넣기
            DeselectAllItems(); // 아이템 미선택시 비활성화
            listofUIItems[itemIndex].Select();  // 해당 아이템 선택 클릭시, 선택 표시 가시화
            selectItemSlotNum = itemIndex;  // 아이템리스트 순서 번호 가져오기
            //Debug.Log($"{selectItemSlotNum}이 번호");
        }

        internal void ResetAllItems()   // 선택 상태 초기화
        {
            foreach (var item in listofUIItems)
            {
                item.ResetData();   // 인벤토리 정보 초기화
                item.Deselect();    // 아이템 선택 표시 초기화
            }
        }
    }

}