using UnityEngine;
using System.Collections;

public class StartPatrolState : FSMState
{
	private Transform[] patrolWaypoints;
	private int closestWaypointIndex;
	private Transform closestWaypoint;
	
	public StartPatrolState(Transform[] waypoints)
	{
		patrolWaypoints = waypoints;
		stateID = StateID.StartPatrol;
	}
	
	public override void Reason(GameObject player, GameObject npc)
	{
		Transform closestWaypoint = patrolWaypoints [0];
		closestWaypointIndex = 0;
		
		float distanceToClosest = (patrolWaypoints [0].position - npc.transform.position).magnitude;
		for (int i=1; i<patrolWaypoints.Length; i++) 
		{
			float distanceToWaypoint = (patrolWaypoints[i].position - npc.transform.position).magnitude;	
			if (distanceToWaypoint <= distanceToClosest)
			{
				closestWaypointIndex = i;
				distanceToClosest = distanceToWaypoint;
			}
			
		}
		//should pass this to constructor.
		npc.GetComponent<F_TankController>().currentWaypoint = closestWaypointIndex;
		npc.GetComponent<NavMeshAgent>().SetDestination (patrolWaypoints[closestWaypointIndex].position);
		npc.GetComponent<F_TankController>().SetTransition(Transition.foundClosestWaypoint);
	}
	
	public override void Act (GameObject player, GameObject npc)
	{
		;
	}
}

//Patrolling State Class

public class PatrollingState : FSMState
{
	private Transform[] patrolWaypoints;
	private int currentWaypoint;
	private F_TankController controller;
	
	public PatrollingState(Transform[] waypoints, F_TankController parent)
	{
		patrolWaypoints = waypoints;
		stateID = StateID.Patroling;
		controller = parent;
	}
	
	public void DoBeforeEntering()
	{
//		currentWaypoint = controller.currentWaypoint;
//		controller.GetComponent<NavMeshAgent>().stoppingDistance = 0f;
//		controller.GetComponent<NavMeshAgent>().SetDestination(patrolWaypoints[currentWaypoint].position);
	}
	
	public override void Reason (GameObject PoI, GameObject NPC)
	{
		if (NPC.GetComponentInChildren<F_PasiveSensor>().checkScanner(PoI))
		{
			NPC.GetComponent<F_TankController>().SetTransition(Transition.poiInSight);
		}
	}
	
	public override void Act (GameObject PoI, GameObject NPC)
	{
		if ((patrolWaypoints[currentWaypoint].position - NPC.transform.position).magnitude  <= 1.0) 
		{
			//Debug.Log ("Reached Waypoint: " + currentWaypoint);
			currentWaypoint++;
			if (currentWaypoint > patrolWaypoints.Length -1) currentWaypoint = 0;
			NPC.GetComponent<NavMeshAgent>().SetDestination(patrolWaypoints[currentWaypoint].position);
			//mobilitySystem.setStoppingDistance (0f);
		}
	}
}

