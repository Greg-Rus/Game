using UnityEngine;
using System.Collections;

public class TankMinionPerception : MonoBehaviour {

	public Transform PoI;
	public float visionAngle;
	public float visionRange;
	public AICore FSM;

	//Location of Point of Interest. The AI can scann for PoIs based on contect decisions made by FSM.
	//Interresting objects may be tagged with a PoI script that will tell the scanner what it is. 
	//The Scanner can then report the findings to the AI core. 
	Vector3 vectorToPoI; 
	float angleToPoI;
	public bool detectedByProximitySense;
	bool detectedByLongScan;
	bool _hasTarget;
	// Use this for initialization
	void Awake(){
		FSM = GetComponentInParent(typeof(AICore)) as AICore;

	}

	void Start () {
		detectedByProximitySense = false;
		detectedByLongScan = false;
		_hasTarget = false;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		scannVisionRange ();
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

	public void scannVisionRange(){
		//drawDebugCone ();
		vectorToPoI = PoI.position - transform.position;
		angleToPoI = Vector3.Angle (vectorToPoI, transform.forward);
		if (angleToPoI <= visionAngle) {
			RaycastHit hit;
			if(	Physics.Raycast (transform.position, vectorToPoI, out hit, visionRange) &&
				hit.rigidbody && 
			   	hit.rigidbody.name == PoI.name){
				detectedByLongScan = true;
			} else {
				detectedByLongScan = false;

			}
		}
		if (detectedByLongScan || detectedByProximitySense) {
			Debug.Log ("Player in sight");
			if (!_hasTarget) 
			{
				Debug.Log ("Target Acquired");
				FSM.targetAquired ();
				_hasTarget = true;
			}
		} else if (_hasTarget) 	{
			Debug.Log ("Target Lost");
			FSM.targetLost ();
			_hasTarget = false;
		}
						
	}

	public bool targetInSight(){
		return detectedByLongScan || detectedByProximitySense;
	}

}
