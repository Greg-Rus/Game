using UnityEngine;
using System.Collections;

public class CyberspaceAIController : MonoBehaviour {
	public Transform[] waypoints;


	private int currentWaypoint;
	private NavMeshAgent nav; 

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();

	}
	void Update (){
		patrol ();
	}
	
	// Update is called once per frame
	void patrol(){
		//Debug.Log (currentWaypoint);
		if (nav.remainingDistance <= 1.0) {
			currentWaypoint++;
		}	
		if (currentWaypoint > waypoints.Length -1) {
			currentWaypoint =0;
		}

		nav.destination = waypoints[currentWaypoint].position;
	
	}

}
