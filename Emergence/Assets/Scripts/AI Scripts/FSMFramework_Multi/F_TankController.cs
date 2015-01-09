using UnityEngine;
using System.Collections;

public class F_TankController : MonoBehaviour {
	public GameObject Player;
	public Transform[] patrolWaypoints;
	public int currentWaypoint;
	public StateID State;
	public Transform currentPoI;
	private FSMSystem fsm;
	
	// Use this for initialization
	public void SetTransition(Transition t) { fsm.PerformTransition(t); } //setter to use private fsm?
	void Start () {
		MakeFSM ();
		currentPoI = Player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		State = fsm.CurrentStateID;
		fsm.CurrentState.Reason();
		fsm.CurrentState.Act();
	}

	private void MakeFSM()
	{
		StartPatrolState startPatrol = new StartPatrolState (this);
		startPatrol.AddTransition(Transition.foundClosestWaypoint , StateID.Patroling);
		
		PatrollingState patrolling = new PatrollingState (this);
		patrolling.AddTransition(Transition.poiInSight, StateID.Chasing);
		
		ChasingState chasing = new ChasingState(this);
		chasing.AddTransition(Transition.poiLost, StateID.StartPatrol);
		chasing.AddTransition(Transition.poiInFireingRange, StateID.Attacking);
		
		AttackingState attacking = new AttackingState(this);
		attacking.AddTransition(Transition.poiLost, StateID.StartPatrol);
		
		fsm = new FSMSystem();
		fsm.AddState(startPatrol);
		fsm.AddState(patrolling);
		fsm.AddState(chasing);
		fsm.AddState(attacking);
		
		
	}
	
}


