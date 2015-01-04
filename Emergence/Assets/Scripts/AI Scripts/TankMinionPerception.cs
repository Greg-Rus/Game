using UnityEngine;
using System.Collections;

public class TankMinionPerception : MonoBehaviour {

	public Transform PoI;
	public float visionAngle;
	public float visionRange;

	//Location of Point of Interest. The AI can scann for PoIs based on contect decisions made by FSM.
	//Interresting objects may be tagged with a PoI script that will tell the scanner what it is. 
	//The Scanner can then report the findings to the AI core. 
	Vector3 vectorToPoI; 
	float angleToPoI;
	public bool detectedByProximitySense;
	bool detectedByLongScan;
	// Use this for initialization

	void Start () {
		detectedByProximitySense = false;
		detectedByLongScan = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Spere Sensor. Place this on an sensor object equipped with a trigger sphere collider
	void OnTriggerEnter(Collider other){
		Debug.Log (other.name);
		if (other.name == PoI.name) {
			detectedByProximitySense = true;		
		}
	}
	void OnTriggerExit(Collider other){
		if (other.name == PoI.name) {
			detectedByProximitySense = false;		
		}
	}

	public bool scannVisionRange(){
		//drawDebugCone ();
		vectorToPoI = PoI.position - transform.position;
		angleToPoI = Vector3.Angle (vectorToPoI, transform.forward);
		if (angleToPoI <= visionAngle) {
			RaycastHit hit;
			detectedByLongScan = Physics.Raycast (transform.position, vectorToPoI, out hit, visionRange);
		}
		if (detectedByLongScan || detectedByProximitySense) {
			Debug.Log ("Player in sight");
			return true;
		}
		return false;
						
	}

	void drawDebugCone (){


	}
}
