using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Inventory.UI;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "Item Inventory", menuName = "Inventory/Item", order = 1)]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;    // 아이템 정보 리스트
        [field: SerializeField] public int Size { get; private set; } = 10; // 인벤토리 아이템 칸 개수
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated; // 내부 대리자 선언, 인벤토리 현재 상태


        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());   // 빈 아이템 항목 가져오기
            }
        }

        public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)     // 인벤토리에 아이템 추가
        {
            if (item.isStackable == false)  // 중첩 불가 아이템일때
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInvenFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1, itemState); // 개수 1개 감소
                        
                    }

                }
                InformAboutChange();    // 상태 업데이트
                return quantity;
            }
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();    // 상태 업데이트
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity, List<ItemParameter> itemState = null) // 인벤토리 빈 슬롯중 가장 앞
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                itemState = new List<ItemParameter>(itemState == null ? item.defaultParameterList : itemState)
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)  // 아이템 리스트가 비었다면
                {
                    inventoryItems[i] = newItem;    // 그자리에 아이템 정보 추가
                    return quantity;
                }
            }
            return 0;
        }

        private bool IsInvenFull() => inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddStackableItem(ItemSO item, int quantity) // 중첩 가능 아이템
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }
                if (inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake = inventoryItems[i].item.maxStackSize - inventoryItems[i].quantity;    // 최대 개수까지의 남은 갯수

                    if (quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.maxStackSize);
                        quantity -= amountPossibleToTake;
                        Debug.Log($"2.종류 : {item.item_type}");
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        Debug.Log($"3.종류 : {item.item_type}");
                        InformAboutChange();
                        return 0;
                    }
                }

            }
            while (quantity > 0 && IsInvenFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.maxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()    // 상태 업데이트
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {
                    continue;
                }
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)  // 해당 번호의 아이템 정보 가져오기
        {
            return inventoryItems[itemIndex];
        }

        public void AddItemList(InventoryItem item)    // 인벤토리 리스트에 아이템 정보 추가
        {
            AddItem(item.item, item.quantity);
        }

        public void SwapItems(int itemIndex1, int itemIndex2)   // 아이템 교체
        {
            InventoryItem item1 = inventoryItems[itemIndex1];   // 교체 할려는 아이템
            inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
            inventoryItems[itemIndex2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()    // 상태 업데이트
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState()); // 상태 업데이트
            Debug.Log("05: 상태 업데이트");
        }

        internal void itemDescriptionHide() // 아이템 전부 사용시 설명창 숨기기
        {
            GameObject.Find("InventoryDescription").GetComponent<UIInventoryDescription>().ResetDescription();  // 외부 스크립트의 함수 가져오기
        }
        internal void RemoveItem(int itemIndex, int amount) // 아이템 사용
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)  // 없으면 불발
                {
                    Debug.Log("02: 승");
                    return;
                }
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)  // 1개 남은 아이템 사용
                {
                    Debug.Log("03: 전");
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                    itemDescriptionHide();
                }
                else    // 2개이상 있는 아이템 사용시
                {
                    Debug.Log("04: 결");
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder);
                }
                InformAboutChange();
            }
        }

        internal void DumpItem(int itemIndex, int amount)   // 아이템 버리기
        {
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)  // 없으면 불발
                {
                    Debug.Log("2. 버리기");
                    return;
                }
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)  // 1개 남은 아이템 사용
                {
                    Debug.Log("03: 버리기");
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                    itemDescriptionHide();
                    Debug.Log("번회: 설명창 비활성화 가능한가");
                }
                else    // 2개이상 있는 아이템 사용시
                {
                    Debug.Log("04: 버리는건가");
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(reminder);
                }
                InformAboutChange();
            }
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;    // 아이템 개수
        public ItemSO item;     // 아이템 데이터
        public List<ItemParameter> itemState;
        public bool IsEmpty => item == null;    // 아이템이 없을시, true

        public InventoryItem ChangeQuantity(int newQuantity)    // 개수 변경시
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public static InventoryItem GetEmptyItem() => new InventoryItem     // 빈 아이템 항목 가져오기
        {
            item = null,
            quantity = 0,
            itemState = new List<ItemParameter>()
        };
    }

}
