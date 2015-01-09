using UnityEngine;
using System.Collections;

public class StartPatrolState : FSMState
{
	private Transform[] patrolWaypoints;
	private int closestWaypointIndex;
	private F_TankController myController;
	private NavMeshAgent myNav;
	
//	private Transform closestWaypoint;
	
	public StartPatrolState(F_TankController NPC)
	{
		stateID = StateID.StartPatrol;
		patrolWaypoints = NPC.patrolWaypoints;
		myController = NPC;
		myNav = NPC.GetComponent<NavMeshAgent>();
	}
	
	public override void Reason()
	{
		closestWaypointIndex = 0;
		
		float distanceToClosest = (patrolWaypoints [0].position - myController.transform.position).magnitude;
		for (int i=1; i<patrolWaypoints.Length; i++) 
		{
			float distanceToWaypoint = (patrolWaypoints[i].position - myController.transform.position).magnitude;	
			if (distanceToWaypoint <= distanceToClosest)
			{
				closestWaypointIndex = i;
				distanceToClosest = distanceToWaypoint;
			}
			
		}
		//Before we change state we can set the stoppingDistance and destination.
		myNav.stoppingDistance = 0f;
		myNav.SetDestination (patrolWaypoints[closestWaypointIndex].position);
		//Update controller on closest waypoint so that patroll state knows where to pick up from.
		myController.currentWaypoint = closestWaypointIndex;
		myController.SetTransition(Transition.foundClosestWaypoint);
	}
	
	public override void Act ()
	{
		;
	}
}

//Patrolling State Class

public class PatrollingState : FSMState
{
	private Transform[] patrolWaypoints;
	private int currentWaypoint;
	private F_TankController myController;
	private F_PasiveSensor mySensor;
	private NavMeshAgent myNav;
	
	public PatrollingState(F_TankController NPC)
	{
		myController = NPC;
		patrolWaypoints = myController.patrolWaypoints;
		stateID = StateID.Patroling;
		mySensor = myController.GetComponentInChildren<F_PasiveSensor>();
		myNav = myController.GetComponent<NavMeshAgent>();
		
	}
	
	public override void Reason ()
	{
		if (mySensor.checkScanner(myController.currentPoI.transform))
		{
			myNav.stoppingDistance = 10f;
			myController.SetTransition(Transition.poiInSight);
		}
	}
	
	public override void Act ()
	{
		//aybe instead of this do a method that updates this form startPatrol?
		currentWaypoint = myController.currentWaypoint;
		if ((patrolWaypoints[currentWaypoint].position - myController.transform.position).magnitude  <= 1.0) 
		{
			currentWaypoint++;
			if (currentWaypoint > patrolWaypoints.Length -1) currentWaypoint = 0;
			myNav.SetDestination(patrolWaypoints[currentWaypoint].position);
			//mobilitySystem.setStoppingDistance (0f);
		}
	}
}

public class ChasingState : FSMState
{
	private F_TankController myController;
	private F_PasiveSensor mySensor;
	private NavMeshAgent myNav;
	private F_Targetting myTargetting;
	
	public ChasingState(F_TankController NPC)
	{
		stateID = StateID.Chasing;
		myController = NPC;
		mySensor = myController.GetComponentInChildren<F_PasiveSensor>();
		myNav = myController.GetComponent<NavMeshAgent>();
		myTargetting = myController.GetComponent<F_Targetting>();
	}
	
	public override void Reason ()
	{
	
		if(mySensor.checkScanner(myController.currentPoI) &&
		   (myController.currentPoI.transform.position - myController.transform.position).magnitude <=10f)
		   {
		   //This should be an attack transition
			myController.SetTransition(Transition.poiInFireingRange);
		   }
		
		if(!mySensor.checkScanner(myController.currentPoI))
		{
			myController.SetTransition(Transition.poiLost);
		}
	}
	
	public override void Act ()
	{
		myNav.SetDestination(myController.currentPoI.position);
		myTargetting.aimTurret(myController.currentPoI);
	}
}

public class AttackingState : FSMState
{
	private F_TankController myController;
	private F_PasiveSensor mySensor;
	private NavMeshAgent myNav;
	private F_Targetting myTargetting;
	private TankMinionAttack myAttack;
	
	public AttackingState(F_TankController NPC)
	{
		stateID = StateID.Attacking;
		myController = NPC;
		mySensor = myController.GetComponentInChildren<F_PasiveSensor>();
		myNav = myController.GetComponent<NavMeshAgent>();
		myTargetting = myController.GetComponent<F_Targetting>();
		myAttack = myController.GetComponent<TankMinionAttack>();
	}
	
	public override void Reason ()
	{
		if(!mySensor.checkScanner(myController.currentPoI))
		{
			myController.SetTransition(Transition.poiLost);
		}
	}
	
	public override void Act ()
	{
		myNav.SetDestination(myController.currentPoI.position);
		myTargetting.aimTurret(myController.currentPoI);
		if(myTargetting.lockOnTarget(2f))
		{
			myAttack.fire();
		}
	}
}

