using UnityEngine;
using System.Collections;

public class F_TankController : MonoBehaviour {
	public GameObject Player;
	public Transform[] patrolWaypoints;
	public int currentWaypoint;
	public StateID State;
	private FSMSystem fsm;
	// Use this for initialization
	public void SetTransition(Transition t) { fsm.PerformTransition(t); } //setter to use private fsm?
	void Start () {
		MakeFSM ();
	}
	
	// Update is called once per frame
	void Update () {
		State = fsm.CurrentStateID;
		fsm.CurrentState.Reason(Player, gameObject);
		fsm.CurrentState.Act(Player, gameObject);
	}

	private void MakeFSM()
	{
		StartPatrolState startPatrol = new StartPatrolState (patrolWaypoints);
		startPatrol.AddTransition(Transition.foundClosestWaypoint , StateID.Patroling);
		
		PatrollingState patrolling = new PatrollingState (patrolWaypoints, this);
		patrolling.AddTransition(Transition.poiInSight, StateID.Chasing);
		
		ChasingState chasing = new ChasingState();
		chasing.AddTransition(Transition.poiLost, StateID.StartPatrol);
		chasing.AddTransition(Transition.poiInFireingRange, StateID.Attacking);
		
		AttackingState attacking = new AttackingState();
		attacking.AddTransition(Transition.poiLost, StateID.StartPatrol);
		
		fsm = new FSMSystem();
		fsm.AddState(startPatrol);
		fsm.AddState(patrolling);
		fsm.AddState(chasing);
		fsm.AddState(attacking);
		
		
	}
	
}


