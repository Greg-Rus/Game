using UnityEngine;
using System.Collections;

public class MemCellPoolCreator : MonoBehaviour {
	public ObjectPool thePool;
	public GameObject memCell;
	public int objectPoolSize;
	public int expandSize;
	// Use this for initialization
	void Awake () {
		thePool = new ObjectPool (memCell, objectPoolSize, expandSize);
		Debug.Log ("Created: "+thePool);
	}

}
