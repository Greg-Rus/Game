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
	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Spere Sensor. Place this on an sensor object equipped with a trigger sphere collider
	void onTriggerEnter(){


	}

	public bool scannVisionRange(){
		//drawDebugCone ();
		vectorToPoI = PoI.position - transform.position;
		angleToPoI = Vector3.Angle (vectorToPoI, transform.forward);
		if (angleToPoI <= visionAngle) {
			RaycastHit hit;
			if (Physics.Raycast (transform.position, vectorToPoI, out hit, visionRange)) {
				Debug.Log ("Player in sight");
				return true;
			}

		}
		return false;
						
	}

	void drawDebugCone (){


	}
}
