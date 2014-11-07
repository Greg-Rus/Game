using UnityEngine;
using System.Collections;

public class MemCellPoolCreator : MonoBehaviour {
	public ObjectPool thePool;
	public GameObject memCell;
	public int objectPoolSize;
	// Use this for initialization
	void Awake () {
		thePool = new ObjectPool (memCell, objectPoolSize);
		Debug.Log ("Creator "+thePool);
	}

}
