using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Inventory.Model
{
    [CreateAssetMenu(fileName = "item", menuName = "item")]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();
        
        public string actionName => "Consume";
        public AudioClip actionSFX { get; private set; }
        public bool PerformAction(Health health, List<ItemParameter> itemState = null)
        {
            foreach(ModifierData data in modifierData)
            {
                data.statModifier.AffectCharacter(health, data.value);
            }
            return true;
        }

    }

    public interface IDestroyableItem
    {

    }

    public interface IItemAction
    {
        public string actionName {  get; }
        public AudioClip actionSFX { get; }
        bool PerformAction(Health health, List<ItemParameter> itemState);
    }

    [Serializable] 
    public class ModifierData
    {
        public ChracterStatModifierSO statModifier;
        public float value;
    }
}

