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
	public void SetTransition(Transition t) { fsm.PerformTransition(t); }
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
		startPatrol.AddTransition(Transition.isDestroyed, StateID.Destroyed);
		
		PatrollingState patrolling = new PatrollingState (this);
		patrolling.AddTransition(Transition.poiInSight, StateID.Chasing);
		patrolling.AddTransition(Transition.isDestroyed, StateID.Destroyed);
		
		ChasingState chasing = new ChasingState(this);
		chasing.AddTransition(Transition.poiLost, StateID.StartPatrol);
		chasing.AddTransition(Transition.poiInFireingRange, StateID.Attacking);
		chasing.AddTransition(Transition.isDestroyed, StateID.Destroyed);
		
		AttackingState attacking = new AttackingState(this);
		attacking.AddTransition(Transition.poiLost, StateID.StartPatrol);
		attacking.AddTransition(Transition.isDestroyed, StateID.Destroyed);
		
		DestroyedState destroyed = new DestroyedState(this);
		
		fsm = new FSMSystem();
		fsm.AddState(startPatrol);
		fsm.AddState(patrolling);
		fsm.AddState(chasing);
		fsm.AddState(attacking);
		fsm.AddState(destroyed);
		
		
	}
	
}


