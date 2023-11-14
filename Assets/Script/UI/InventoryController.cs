using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;
using System.Text;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        public UIInventoryPage invenUI;   // 인벤토리 메뉴
        [SerializeField] private InventorySO inventorySO;   // 인벤토리 정보
        public List<InventoryItem> initialItems = new List<InventoryItem>();    // 인벤토리내 아이템 정보 리스트

        [SerializeField] private UIManager uiManager;   // uiManager 정보
        [SerializeField] private BlessCardController blessCardController;   // 축복 카드 인벤토리 스크립트
        StatController statController;
        private TestLevel testLevel;    // 스테이지 레벨
        private void Awake() {
            statController = GetComponent<StatController>();
            testLevel = GameObject.Find("TestLevel").GetComponent<TestLevel>();
            if (testLevel.level == 1)   // 게임 시작시 축복 상태 초기화
            {
                inventorySO.Initialize();
            }
        }

        private void Start()
        {
            PrePareUI();    // UI 세팅
            PrepareInventoryData();
        }

        private void PrepareInventoryData() // 시작시 아무것도 없이 초기화 시키는 기능, 남김 없이 사라짐
        {
            //inventorySO.Initialize();   // 인벤토리 슬롯 생성, 초기화 시키는 기능
            inventorySO.OnInventoryUpdated += UpdateInventoryUI;    // 인벤토리 현재 상태 초기화
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                {
                    continue;
                }
                inventorySO.AddItemList(item);  // 인벤토리 리스트에 아이템 정보 추가
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> invenState)   // 인벤토리 업데이트
        {
            invenUI.ResetAllItems();    // 선택 상태 초기화
            foreach (var item in invenState)
            {
                invenUI.UpDateData(item.Key, item.Value.item.itemSprite, item.Value.quantity);     // 인벤토리 아이템 데이터 상시 업데이트
            }
        }

        private void PrePareUI()    // 시작시 인벤토리 칸, 이벤트 생성
        {
            invenUI.InitializeInventoryUI(inventorySO.Size);    // 인벤토리 칸 개수생성
            this.invenUI.OnDesCriptionRequested += HandleDescriptionRequest;
            this.invenUI.OnSwapItems += HandleSwapItems;
            this.invenUI.OnStartDragging += HandleDragging;
            this.invenUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleDescriptionRequest(int itemIndex)    // 아이템 설명 요청
        {
            InventoryItem inventoryItem = inventorySO.GetItemAt(itemIndex); // 해당 번호의 아이템 정보 가져오기
            if (inventoryItem.IsEmpty)
            {
                invenUI.ResetSelection();   // 빈 아이템UI 클릭시 설명창 초기화
                return;
            }
            ItemSO itemSO = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            invenUI.UpdateDescription(itemIndex, itemSO.itemSprite, itemSO.itemName, description);  // 아이템UI 정보 초기화
            //Debug.Log($"4.종류 : {itemSO.item_type}");
            
            switch (itemSO.item_type)
            {
                case "ATK":
                    if (BuffManager.instance.onATK == true)    // 사용하여 시간이 돌아가면
                    {
                        BuffUseYN.instance.DEFEnabled(false);
                        BuffUseYN.instance.ATKEnabled(true);
                        
                        uiManager.description_Button_Panel.SetActive(false);
                    }
                    else
                    {
                        BuffUseYN.instance.AllEnabledFalse();
                        uiManager.description_Button_Panel.SetActive(true);
                    }
                    break;
                case "DEF":
                    if (BuffManager.instance.onDEF == true)    // 사용하여 시간이 돌아가면
                    {
                        BuffUseYN.instance.ATKEnabled(false);
                        BuffUseYN.instance.DEFEnabled(true);
                        uiManager.description_Button_Panel.SetActive(false);
                    }
                    else
                    {
                        BuffUseYN.instance.AllEnabledFalse();
                        uiManager.description_Button_Panel.SetActive(true);
                        
                    }
                    break;
                default:
                    BuffUseYN.instance.AllEnabledFalse();
                    uiManager.description_Button_Panel.SetActive(true);
                    break;
            }

        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();     // 문자열 변경 가능
            sb.Append(inventoryItem.item.itemDescription);  // 변경되는 문자열 객체 참조 
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameterSO.ParameterName}" +   // 아이템 이름
                $" : {inventoryItem.itemState[i].value} /" +        // 아이템 설명
                $"{inventoryItem.item.defaultParameterList[i].value}"); // 내구도 수치
                sb.AppendLine();    // 문자열 줄 종결
            }
            return sb.ToString();
        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)    // 아이템 드래그 교체 종료시
        {
            inventorySO.SwapItems(itemIndex1, itemIndex2);  // 아이템 교체
        }

        private void HandleDragging(int itemIndex)  // 아아템 드래그 교체 시작시
        {
            InventoryItem inventoryItem = inventorySO.GetItemAt(itemIndex); // 해당 번호의 아이템 정보 가져오기
            if (inventoryItem.IsEmpty)
            {
                return;
            }
            invenUI.CreateDraggedItem(inventoryItem.item.itemSprite, inventoryItem.quantity);   // 마우스 포인터에 아이템 이미지, 개수 가져오기
        }

        private void HandleItemActionRequest(int itemIndex) // 아이템 우클릭시 효과 요청
        {
            uiManager.UseItem(itemIndex);   // 인벤토리에서 아이템 사용 요청
        }


        public void OnOffInvenPage()    // 인벤토리 페이지 열기
        {
            if (invenUI.isActiveAndEnabled == false)
            {
                invenUI.Show();
                if (invenUI.isActiveAndEnabled == false)
                {
                    invenUI.Show();
                }
                invenUI.ResetSelection();   // 아이템UI 선택 초기화
                blessCardController.bCardUI.Hide();
                statController.statUI.Hide();
                foreach (var item in inventorySO.GetCurrentInventoryState())
                {
                    invenUI.UpDateData(item.Key, item.Value.item.itemSprite, item.Value.quantity);   // 인벤토리 아이템 데이터 상시 업데이트
                }
            }
            else
            {
                invenUI.Hide();
            }
        }

    }
}
