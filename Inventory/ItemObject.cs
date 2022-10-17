using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[CreateAssetMenu(fileName = "New Inventory Object", menuName = "Inventory System/New Inventory Object")]
	public class ItemObject : ScriptableObject
	{
		public string ItemName;
		public bool active = true;
	}
}
