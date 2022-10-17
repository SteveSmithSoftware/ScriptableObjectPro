using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOContainer", menuName = "SOPro/SO Container")]
public class ScriptableObjectContainer : ScriptableObject
{
	// Add all SO's here in Inspector
	[SerializeField]
	List<ScriptableObject> scriptableObjects;

	static ScriptableObjectContainer instance;

	static Dictionary<int, int> SOdict = new Dictionary<int, int>();

	private void OnEnable()
	{
		instance = this;

		if (scriptableObjects != null)
		{
			for (int i = 0; i < scriptableObjects.Count; i++)
			{
				SOdict.Add(scriptableObjects[i].GetInstanceID(), i);
			}
		}
	}

	public static int Count
	{
		get { return instance.scriptableObjects.Count; }
	}

	public static ScriptableObject Get(int ix)
	{
		if (ix >=0 && ix < Count) return instance.scriptableObjects[ix];
		throw new ArgumentOutOfRangeException("index", "Invalid index "+ix+" in Get");
	}

	public static int Get(ScriptableObject so)
	{
		int id = so.GetInstanceID();
		if (SOdict.ContainsKey(id)) return SOdict[id];
		Debug.LogWarning("Scriptable Object " + so.name + " not found");
		return -1;
	}

	public static List<ScriptableObject> Get(List<int> ixs)
	{
		List<ScriptableObject> objects = new List<ScriptableObject>();
		foreach (int ix in ixs)
		{
			objects.Add(Get(ix));
		}
		return objects;
	}

	public static List<int> Get(List<ScriptableObject> objects)
	{
		List<int> ixs = new List<int>();
		foreach (ScriptableObject so in objects)
		{
			ixs.Add(Get(so));
		}
		return ixs;
	}


	public static int Add(ScriptableObject so)
	{
		instance.scriptableObjects.Add(so);
		int ix = Count - 1;
		SOdict.Add(so.GetInstanceID(), ix);
		return ix;
	}
}
