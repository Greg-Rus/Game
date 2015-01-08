﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class F_PasiveSensor : MonoBehaviour {

	public float visionAngle;
	public float visionRange;
	
	Vector3 vectorToPoI; 
	float angleToPoI;
	private List<string> closeObjects;
	
	
	// Use this for initialization
	void Start () {
		//Check if object with this scrip has a trigger collider that can be used as a proximity sensor.
		if(!GetComponent<Collider>().isTrigger == true)
		{
			Debug.LogError("Error: " + gameObject.name + " needs a trigger collider!");
		}
		closeObjects = new List<string>();
	}
	
	public bool checkScanner(GameObject PoI)
	{
		return scannVisionRange(PoI) || scanProximityRange(PoI);
	}

	//Trigger events update the closeObjects list with names of objects in range.
	//	
	void OnTriggerEnter(Collider other){
		if (other.rigidbody)
		{
			if (!(closeObjects.Contains(other.name))) 
			{
				closeObjects.Add (other.name);
			}
		}
	}
	void OnTriggerExit(Collider other){
		if (closeObjects.Contains(other.name)) {
			closeObjects.Remove(other.name);			
		}
	}	
	
	bool scannVisionRange(GameObject PoI)
	{
		//First check if PoI in view angle.
		vectorToPoI = PoI.transform.position - transform.position;
		angleToPoI = Vector3.Angle (vectorToPoI, transform.forward);
		if (angleToPoI <= visionAngle) 
		{
			//Next, check if PoI is in vision range
			RaycastHit hit;
			if(	Physics.Raycast (transform.position, vectorToPoI, out hit, visionRange) &&
			   hit.rigidbody && 
			   hit.rigidbody.name == PoI.name){
				return true;
			}
		}
		return false;
	}
	
	bool scanProximityRange(GameObject PoI)
	{
		if(closeObjects.Contains(PoI.name))
		{
			return true;
		} else{return false;}
	}
}