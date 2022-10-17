using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SSSoftware.Attributes;

namespace InventorySystem
{
    [System.Serializable, Mutable]
    public class InventorySlot
    {
        public int ID=-1;
        public ItemObject item
        {
            get { if (ID >= 0) return ItemContainer.Get(ID); else return null; }
            set { ID = ItemContainer.Get(value);  }
        }
        public int amount;

        public InventorySlot()
        {
        }

        public InventorySlot(ItemObject _item, int _amount)
        {
            item = _item;
            amount = _amount;
        }
        public void AddAmount(int value)
        {
            amount += value;
        }

        public override string ToString()
		{
            if (ID >= 0) return item.ItemName+" ["+amount+"]";
            else return "Empty";
		}
    }
}