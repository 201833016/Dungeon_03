using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    //[CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string actionName => "Equip";

        public AudioClip actionSFX  { get; private set;}

        public bool PerformAction(Health health, List<ItemParameter> itemState = null)
        {
            /*
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if(weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ? defaultParameterList : itemState);
                return true;
            }*/
            return false;
        }
    }
}


