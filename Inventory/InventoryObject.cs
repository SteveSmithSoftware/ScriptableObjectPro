using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSSoftware.Attributes;

namespace InventorySystem
{
    [PersistAcrossBuilds, ResetOnError]
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/New Inventory")]
    public class InventoryObject : SSSoftware.SOPro.ScriptableObject
    {
        [SerializeField,Immutable]
        ItemContainer container;
        [SerializeField, Mutable]
        List<InventorySlot> Slots = new List<InventorySlot>();

        public List<InventorySlot> Inventory
		{
            get { return Slots; }
		}
        public int Count
		{
            get { return Slots.Count; }
		}

        public void AddItem(ItemObject _item, int _amount)
        {
            if (_item == null)
                return;

            int id = ItemContainer.Get(_item);
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].ID == id)
                {
                    Slots[i].AddAmount(_amount);
                    if (Slots[i].amount <= 0)
                    {
                        Slots.RemoveAt(i);
                    }
                    Save();
                    return;
                } else
				{
                    if (Slots[i].ID < 0) // Reuse empty Slot
					{
                        Slots[i].item = _item;
                        Slots[i].amount = _amount;
                        Save();
                        return;
					}
				}
            }
            Slots.Add(new InventorySlot(_item, _amount));
            Save();
        }

        public void RemoveItem(ItemObject _item)
        {
            if (_item == null)
                return;

            int id = ItemContainer.Get(_item);
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].ID == id)
                {
                    Slots.RemoveAt(i);
                    Save();
                    return;
                }
            }
        }

        public InventorySlot ContainsItem(ItemObject _item)
        {
            int id = ItemContainer.Get(_item);
            foreach (InventorySlot itemSlot in Slots)
            {
                if (id == itemSlot.ID)
                    return itemSlot;
            }
            return null;
        }

        public void Clear()
        {
            Slots.Clear();
        }
    }
}