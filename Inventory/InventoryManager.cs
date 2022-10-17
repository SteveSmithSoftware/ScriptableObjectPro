using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using VirtualPetSystem;


namespace InventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        public InventoryObject inventoryObject;
        public TextMeshProUGUI inventoryItems;


        private void Start()
        {
            ShowItems();
        }

        public void AddItem(ItemObject itemVal)
        {
            inventoryObject.AddItem(itemVal, 1);
            ShowItems();
        }

        public void ShowItems()
        {
            string result = "";

            foreach (InventorySlot item in inventoryObject.Inventory)
            {
                //PetConsumible consumible = item.item as PetConsumible;
                result += item.ToString() + ",";
            }
            if (inventoryItems != null) inventoryItems.text = result;
            else Debug.Log(result);
        }

    }
}