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
		if(closestWaypointIndex != -1)
		{
			
			closestWaypointIndex = -1;
		}
	}
	
	public override void Act (GameObject player, GameObject npc)
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
		npc.GetComponent<TankController>().currentWaypoint = closestWaypointIndex;
		npc.GetComponent<TankController>().SetTransition(Transition.foundClosestWaypoint);
	}
}

public class PatrollingState : FSMState
{
	private Transform[] patrollWaypoints;
	
	public PatrollingState(Transform[] waypoints)
	{
		patrollWaypoints = waypoints;
	}
	
	public override void Reason (GameObject PoI, GameObject NPC)
	{
	
	}
	
	public override void Act (GameObject PoI, GameObject NPC)
	{
		
	}
}
