using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool {

	private GameObject type;
	private int quantity;
//	private GameObject[] activePool; 
	private GameObject[] inactivePool;
//	private int activeCounter;
	private int inactiveCounter;

	public ObjectPool(GameObject objectType, int number){
		type = objectType;
		quantity = number;
		inactivePool = new GameObject[quantity];

		for (int i = 0; i < quantity; i++) {
		
			GameObject newObject = GameObject.Instantiate(type, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			newObject.SetActive(false);
			inactivePool[i] = newObject;

		}
		inactiveCounter = quantity-1;

	}

	public GameObject retrieveObject()
	{

		inactivePool [inactiveCounter].SetActive(true);
		GameObject topInactiveObj = inactivePool [inactiveCounter];
		inactiveCounter--;

		return topInactiveObj;

	}

	public void storeObject(GameObject removedObject)
	{

		inactiveCounter++;
		inactivePool [inactiveCounter] = removedObject;
		inactivePool [inactiveCounter].SetActive(false);

	}

}
