using UnityEngine;
using System.Collections;

public class TankController : MonoBehaviour {
	public GameObject Player;
	public Transform[] patrolWaypoints;
	public int currentWaypoint;
	private FSMSystem fsm;
	// Use this for initialization
	public void SetTransition(Transition t) { fsm.PerformTransition(t); } //setter to use private fsm?
	void Start () {
		MakeFSM ();
	}
	
	// Update is called once per frame
	void Update () {
		fsm.CurrentState.Reason(Player, gameObject);
		fsm.CurrentState.Act(Player, gameObject);
	}

	private void MakeFSM()
	{
		StartPatrolState startPatrol = new StartPatrolState (patrolWaypoints);
		startPatrol.AddTransition(Transition.foundClosestWaypoint , StateID.Patroling);
	}
}


