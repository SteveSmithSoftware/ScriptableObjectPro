using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UOContainer", menuName = "SOPro/UO Container")]
public class UnityObjectContainer : ScriptableObject
{ 
	// Add all UnityEngine Objects's here in Inspector
	[SerializeField]
	List<UnityEngine.Object> objects;

	static UnityObjectContainer instance;

	static Dictionary<int, int> UOdict = new Dictionary<int, int>();

	private void OnEnable()
	{
		instance = this;

		if (objects != null)
		{
			for (int i = 0; i < objects.Count; i++)
			{
				UOdict.Add(objects[i].GetInstanceID(), i);
			}
		}
	}

	public static int Count
	{
		get { return instance.objects.Count; }
	}

	public static UnityEngine.Object Get(int ix)
	{
		if (ix >= 0 && ix < Count) return instance.objects[ix];
		throw new ArgumentOutOfRangeException("index", "Invalid index " + ix + " in Get");
	}

	public static int Get(UnityEngine.Object uo)
	{
		int id = uo.GetInstanceID();
		if (UOdict.ContainsKey(id)) return UOdict[id];
		Debug.LogWarning("Unity Object " + uo.GetType()+ " " + uo.name + " not found");
		return -1;
	}

	public static List<UnityEngine.Object> Get(List<int> ixs)
	{
		List<UnityEngine.Object> objects = new List<UnityEngine.Object>();
		foreach (int ix in ixs)
		{
			objects.Add(Get(ix));
		}
		return objects;
	}

	public static List<int> Get(List<UnityEngine.Object> objects)
	{
		List<int> ixs = new List<int>();
		foreach (UnityEngine.Object uo in objects)
		{
			ixs.Add(Get(uo));
		}
		return ixs;
	}

	public static int Add(UnityEngine.Object uo)
	{
		instance.objects.Add(uo);
		int ix = Count - 1;
		UOdict.Add(uo.GetInstanceID(), ix);
		return ix;
	}
}
