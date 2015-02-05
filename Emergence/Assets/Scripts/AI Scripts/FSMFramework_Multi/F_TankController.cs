using UnityEngine;
using System.Collections;

public class F_TankController : MonoBehaviour {
	public GameObject Player;
	public Transform[] patrolWaypoints;
	public int currentWaypoint;
	public StateID State;
	public Transform currentPoI;
	private FSMSystem fsm;
	private int closestWaypointIndex;
	private NavMeshAgent nav;
	private Transform myTransform;
	
	// Use this for initialization
	public void SetTransition(Transition t) { fsm.PerformTransition(t); }
	void Start () {
		MakeFSM ();
		currentPoI = Player.transform;
		nav = GetComponent<NavMeshAgent>();
		myTransform = transform;
		SetClosestWaypoint();
	}
	
	// Update is called once per frame
	void Update () {
		State = fsm.CurrentStateID;
		fsm.CurrentState.Reason();
		fsm.CurrentState.Act();
	}

	private void MakeFSM()
	{
		PatrollingState patrolling = new PatrollingState (this);
		patrolling.AddTransition(Transition.poiInSight, StateID.Chasing);
		patrolling.AddTransition(Transition.isDestroyed, StateID.Destroyed);
		
		ChasingState chasing = new ChasingState(this);
		chasing.AddTransition(Transition.poiLost, StateID.Patroling);
		chasing.AddTransition(Transition.poiInFireingRange, StateID.Attacking);
		chasing.AddTransition(Transition.isDestroyed, StateID.Destroyed);
		
		AttackingState attacking = new AttackingState(this);
		attacking.AddTransition(Transition.poiLost, StateID.Patroling);
		attacking.AddTransition(Transition.isDestroyed, StateID.Destroyed);
		
		DestroyedState destroyed = new DestroyedState(this);
		
		fsm = new FSMSystem();
		fsm.AddState(patrolling);
		fsm.AddState(chasing);
		fsm.AddState(attacking);
		fsm.AddState(destroyed);	
	}
	
	public void SetClosestWaypoint()
	{
		closestWaypointIndex = 0;
		
		float distanceToClosest = (patrolWaypoints [0].position - myTransform.position).magnitude;
		for (int i=1; i<patrolWaypoints.Length; i++) 
		{
			float distanceToWaypoint = (patrolWaypoints[i].position - myTransform.position).magnitude;	
			if (distanceToWaypoint <= distanceToClosest)
			{
				closestWaypointIndex = i;
				distanceToClosest = distanceToWaypoint;
			}
			
		}
		//Before we change state we can set the stoppingDistance and destination.
		nav.stoppingDistance = 0f;
		nav.SetDestination (patrolWaypoints[closestWaypointIndex].position);
		currentWaypoint = closestWaypointIndex;
	}
	
}


