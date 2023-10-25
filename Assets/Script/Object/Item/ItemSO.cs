using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Inventory.Model
{
    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField] public bool isStackable { get; set; }      // 중첩 가능한 아이템인가
        public int ID => GetInstanceID();

        [field: SerializeField] public int maxStackSize { get; set; } = 1;  // 중첩 가능 아이템 개수
        [field: SerializeField] public string itemName { get; set; }    // 아이템 이름
        [field: SerializeField] public Sprite itemSprite { get; set; }  // 아이템 이미지
        [field: SerializeField][field: TextArea] public string itemDescription { get; set; }    // 아이템 설명 내용
        [field: SerializeField] public List<ItemParameter> defaultParameterList { get; set; }   // 아이템 내구도
        [field: SerializeField] public string item_type { get; set; }    // 아이템 종류
    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameterSO;
        public float value;
        public bool Equals(ItemParameter other)
        {
            return other.itemParameterSO == itemParameterSO;
        }
    }
}

