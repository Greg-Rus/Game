using UnityEngine;
using System.Collections;

public class TankNavigation : MonoBehaviour {

	public Transform[] waypoints;
	public int currentWaypoint;
	private NavMeshAgent nav;
	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		patrol ();
	}

	void patrol()
	{
		if ( nav.remainingDistance <= 0.5) 
		{
			currentWaypoint++;
			if (currentWaypoint > waypoints.Length -1) currentWaypoint = 0;
			nav.destination = waypoints[currentWaypoint].position;
		}
	}
}
