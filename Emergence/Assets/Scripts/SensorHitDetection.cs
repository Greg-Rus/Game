using UnityEngine;
using System.Collections;

public class SensorHitDetection : MonoBehaviour {
	public ElevateMemCell memCell;
	// Use this for initialization
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(){
		memCell.increaseFill ();
//		Debug.Log ("Collision Trigger!!");
	}
	void OnTriggerExit(){
		memCell.decreaseFill ();
//		Debug.Log ("Collision Trigger Exit!!");

	}


}
