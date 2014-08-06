using UnityEngine;
using System.Collections;

public class PoolTest : MonoBehaviour {
	public ObjectPool pool;
	public GameObject memCell;
	GameObject temp1;
	GameObject temp2;
	GameObject temp3;
	Collider c1;
	// Use this for initialization
	void Start () {
		pool = new ObjectPool (memCell, 10);
		Debug.Log ("Pool made");

		temp1 = pool.retrieveObject();
		temp2 = pool.retrieveObject();
		temp3 = pool.retrieveObject();

		c1 = temp1.collider;
		temp2 = c1.gameObject;

		pool.storeObject (temp2);
		Debug.Log ("temp stored");	
		temp1 = pool.retrieveObject();
		Debug.Log ("temp re-drawn");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
