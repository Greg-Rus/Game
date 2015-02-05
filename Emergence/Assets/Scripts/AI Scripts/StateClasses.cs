using UnityEngine;
using System.Collections;

public class StartPatrolState : FSMState
{
	private Transform[] patrolWaypoints;
	private int closestWaypointIndex;
	private F_TankController myController;
	private NavMeshAgent myNav;
	private F_ControlMode myControlMode;
	
//	private Transform closestWaypoint;
	
	public StartPatrolState(F_TankController NPC)
	{
		stateID = StateID.StartPatrol;
		patrolWaypoints = NPC.patrolWaypoints;
		myController = NPC;
		myNav = NPC.GetComponent<NavMeshAgent>();
		myControlMode = NPC.GetComponent<F_ControlMode>();
		
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
	private int closestWaypointIndex;
	private Transform[] patrolWaypoints;
	private int currentWaypoint;
	private F_TankController myController;
	private F_PasiveSensor mySensor;
	private NavMeshAgent myNav;
	private F_ControlMode myControlMode;
	
	public PatrollingState(F_TankController NPC)
	{
		myController = NPC;
		patrolWaypoints = myController.patrolWaypoints;
		stateID = StateID.Patroling;
		mySensor = myController.GetComponentInChildren<F_PasiveSensor>();
		myNav = myController.GetComponent<NavMeshAgent>();
		myControlMode = myController.GetComponent<F_ControlMode>();
		
	}
	public override void DoBeforeEntering() 
	{
		myController.SetClosestWaypoint();
	}
	
	public override void Reason ()
	{
		if (mySensor.checkScanner(myController.currentPoI.transform))
		{	
			myController.SetTransition(Transition.poiInSight);
		}
	}
	
	public override void Act ()
	{
		//aybe instead of this do a method that updates this form startPatrol?
		if ((patrolWaypoints[myController.currentWaypoint].position - myController.transform.position).magnitude  <= 1.0) 
		{
			myController.currentWaypoint++;
			if (myController.currentWaypoint > patrolWaypoints.Length -1) 
			{
				myController.currentWaypoint = 0;
			}
			Debug.Log (myController.currentWaypoint);
			myNav.SetDestination(patrolWaypoints[myController.currentWaypoint].position);
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
		   mySensor.isInAttackRange(myController.currentPoI))
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
	
	public override void DoBeforeEntering()
	{
		myNav.stoppingDistance = mySensor.attackRange;
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
	
	public override void DoBeforeExiting()
	{
		myTargetting.resetTurret();
	}
}

public class DestroyedState : FSMState
{
	private F_TankController myController;
	private F_PasiveSensor mySensor;
	private NavMeshAgent myNav;
	private F_Targetting myTargetting;
	private TankMinionAttack myAttack;
	private F_Stats myStats;
	private F_ControlMode myControlMode;
	
	public DestroyedState(F_TankController NPC)
	{
		stateID = StateID.Destroyed;
		myController = NPC;
		mySensor = myController.GetComponentInChildren<F_PasiveSensor>();
		myNav = myController.GetComponent<NavMeshAgent>();
		myTargetting = myController.GetComponent<F_Targetting>();
		myAttack = myController.GetComponent<TankMinionAttack>();
		myStats = myController.GetComponent<F_Stats>();
		myControlMode = myController.GetComponent<F_ControlMode>();
	}
	public override void Reason ()
	{
	//No more reasoning. This is the final state.
	}
	public override void Act ()
	{
		myStats.explode();
		myAttack.enabled = false;
		myNav.enabled = false;
		mySensor.enabled = false;
		myTargetting.enabled = false;
		myControlMode.usePhysics();
		myControlMode.enabled=false;
		myController.enabled = false; //Disable the FSM because final state is reached.
	}
}

