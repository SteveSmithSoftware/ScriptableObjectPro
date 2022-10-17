using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[CreateAssetMenu(fileName = "New Item Container", menuName = "Inventory System/Item Container")]
	public class ItemContainer : ScriptableObject
	{
		// Add all SO's here in Inspector
		[SerializeField]
		List<ItemObject> itemObjects;

		static ItemContainer instance;
		static Dictionary<int, int> IOdict = new Dictionary<int, int>();

		private void OnEnable()
		{
			instance = this;

			if (itemObjects != null)
			{
				for (int i = 0; i < itemObjects.Count; i++)
				{
					IOdict.Add(itemObjects[i].GetInstanceID(), i);
				}
			}
		}

		public static int Count
		{
			get { return instance.itemObjects.Count; }
		}

		public static ItemObject Get(int ix)
		{
			if (ix >= 0 && ix < Count) return instance.itemObjects[ix];
			throw new ArgumentOutOfRangeException("index", "Invalid index " + ix + " in Get");
		}

		public static int Get(ItemObject so)
		{
			int id = so.GetInstanceID();
			if (IOdict.ContainsKey(id)) return IOdict[id];
			Debug.LogWarning("Item Object Object " + so.name + " not found");
			return -1;
		}

		public static List<ItemObject> Get(List<int> ixs)
		{
			List<ItemObject> objects = new List<ItemObject>();
			foreach (int ix in ixs)
			{
				objects.Add(Get(ix));
			}
			return objects;
		}

		public static List<int> Get(List<ItemObject> objects)
		{
			List<int> ixs = new List<int>();
			foreach (ItemObject so in objects)
			{
				ixs.Add(Get(so));
			}
			return ixs;
		}


		public static int Add(ItemObject so)
		{
			instance.itemObjects.Add(so);
			int ix = Count - 1;
			IOdict.Add(so.GetInstanceID(), ix);
			return ix;
		}
	}
}
