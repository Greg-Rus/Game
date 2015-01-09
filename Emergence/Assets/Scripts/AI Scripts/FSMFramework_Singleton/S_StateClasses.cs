using UnityEngine;
using System.Collections;

public class S_StartPatrolState : S_FSMState
{
	private Transform[] patrolWaypoints;
	private int closestWaypointIndex;
	private GameObject NPCRef;
	private Vector3 NPCPosition;
	private NavMeshAgent NPCNav;
	private int currentWaypoint;
	private S_PersonalData NPCData;
	//	private Transform closestWaypoint;
	
	public S_StartPatrolState(Transform[] waypoints)
	{
		patrolWaypoints = waypoints;
		stateID = StateID.StartPatrol;
	}
	
//	public override void SetNPC(GameObject NPC)
	public  void SetNPC(GameObject NPC)
	{
		NPCPosition = NPCRef.transform.position;
		NPCNav = NPCRef.GetComponent<NavMeshAgent>();
		NPCData = NPCRef.GetComponent<S_PersonalData>();
	}
	
	public override void Reason(GameObject PoI, GameObject NPC)
	{
		//Transform closestWaypoint = patrolWaypoints [0];
		closestWaypointIndex = 0;
		
		float distanceToClosest = (patrolWaypoints [0].position - NPCPosition).magnitude;
		for (int i=1; i<patrolWaypoints.Length; i++) 
		{
			float distanceToWaypoint = (patrolWaypoints[i].position - NPCPosition).magnitude;	
			if (distanceToWaypoint <= distanceToClosest)
			{
				closestWaypointIndex = i;
				distanceToClosest = distanceToWaypoint;
			}
			
		}
		//should pass this to constructor.
		NPCNav.stoppingDistance = 0f;
		NPCData.currentWaypoint = closestWaypointIndex;
		NPCNav.SetDestination (patrolWaypoints[closestWaypointIndex].position);
		NPC.GetComponent<F_TankController>().SetTransition(Transition.foundClosestWaypoint);
	}
	
	public override void Act (GameObject PoI, GameObject NPC)
	{
		;
	}
}

//Patrolling State Class

public class S_PatrollingState : S_FSMState
{
	private Transform[] patrolWaypoints;
	private int currentWaypoint;
	private S_TankController controller;
	
	public S_PatrollingState(Transform[] waypoints, S_TankController parent)
	{
		patrolWaypoints = waypoints;
		stateID = StateID.Patroling;
		controller = parent;
	}
	
	public override void DoBeforeEntering()
	{
		currentWaypoint = controller.currentWaypoint;
		Debug.Log("Set current waypoint to: " + currentWaypoint);
		//		controller.GetComponent<NavMeshAgent>().stoppingDistance = 0f;
		//		controller.GetComponent<NavMeshAgent>().SetDestination(patrolWaypoints[currentWaypoint].position);
	}
	
	public override void Reason (GameObject PoI, GameObject NPC)
	{
		if (NPC.GetComponentInChildren<F_PasiveSensor>().checkScanner(PoI.transform))
		{
			NPC.GetComponent<NavMeshAgent>().stoppingDistance = 10f;
			NPC.GetComponent<F_TankController>().SetTransition(Transition.poiInSight);
		}
	}
	
	public override void Act (GameObject PoI, GameObject NPC)
	{
		
		if ((patrolWaypoints[currentWaypoint].position - NPC.transform.position).magnitude  <= 1.0) 
		{
			currentWaypoint++;
			if (currentWaypoint > patrolWaypoints.Length -1) currentWaypoint = 0;
			NPC.GetComponent<NavMeshAgent>().SetDestination(patrolWaypoints[currentWaypoint].position);
			//mobilitySystem.setStoppingDistance (0f);
		}
	}
}

public class S_ChasingState : S_FSMState
{
	public S_ChasingState()
	{
		stateID = StateID.Chasing;
	}
	
	public override void Reason (GameObject PoI, GameObject NPC)
	{
		
		if(NPC.GetComponentInChildren<F_PasiveSensor>().checkScanner(PoI.transform) &&
		   (PoI.transform.position - NPC.transform.position).magnitude <=10f)
		{
			//This should be an attack transition
			NPC.GetComponent<F_TankController>().SetTransition(Transition.poiInFireingRange);
		}
		
		if(!NPC.GetComponentInChildren<F_PasiveSensor>().checkScanner(PoI.transform))
		{
			NPC.GetComponent<F_TankController>().SetTransition(Transition.poiLost);
		}
	}
	
	public override void Act (GameObject PoI, GameObject NPC)
	{
		NPC.GetComponent<NavMeshAgent>().SetDestination(PoI.transform.position);
		NPC.GetComponent<F_Targetting>().aimTurret(PoI.transform);
	}
}

public class S_AttackingState : S_FSMState
{
	public S_AttackingState()
	{
		stateID = StateID.Attacking;
	}
	
	public override void Reason (GameObject PoI, GameObject NPC)
	{
		if(!NPC.GetComponentInChildren<F_PasiveSensor>().checkScanner(PoI.transform))
		{
			NPC.GetComponent<F_TankController>().SetTransition(Transition.poiLost);
		}
	}
	
	public override void Act (GameObject PoI, GameObject NPC)
	{
		NPC.GetComponent<NavMeshAgent>().SetDestination(PoI.transform.position);
		NPC.GetComponent<F_Targetting>().aimTurret(PoI.transform);
		if(NPC.GetComponent<F_Targetting>().lockOnTarget(2f))
		{
			NPC.GetComponent<TankMinionAttack>().fire();
		}
	}
}

