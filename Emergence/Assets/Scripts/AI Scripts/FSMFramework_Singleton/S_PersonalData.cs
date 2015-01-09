using UnityEngine;
using System.Collections;

public class S_PersonalData : MonoBehaviour {

	public int currentWaypoint;
	
	// Use this for initialization
	void Start () {
		S_TankController.instance.subscirbeForFSM(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
