using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//This is a singleton. Created when first minion tries to subscribe.

public class S_TankController : MonoBehaviour {

	private static S_TankController _instance;
	public static S_TankController instance
	{
		get
		{
			return _instance;
		}
	}

	public GameObject Player;
	public Transform[] patrolWaypoints;
	public int currentWaypoint;
	public StateID State;
	private S_FSMSystem fsm;
	private List<GameObject> NPCs;

	public void SetTransition(Transition t) { fsm.PerformTransition(t); } //setter to use private fsm?
	
	void Awake() 
	{
	//Unity version of singleton pattern. Ensures the static reference points onlyto the firs object created. All other objects will be destroyed on Awake. This good if someone makes more tean one in the editor.
		if(_instance == null)
		{
			_instance = this;
		}
		else if(this != _instance)
		{
			Destroy(this.gameObject);
		}
	//Create a list of GameObjects so the NPCs can subscribe to it.	
		NPCs = new List<GameObject>();
	}
	
	
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
		S_StartPatrolState startPatrol = new S_StartPatrolState (patrolWaypoints);
		startPatrol.AddTransition(Transition.foundClosestWaypoint , StateID.Patroling);
		
		S_PatrollingState patrolling = new S_PatrollingState (patrolWaypoints, this);
		patrolling.AddTransition(Transition.poiInSight, StateID.Chasing);
		
		S_ChasingState chasing = new S_ChasingState();
		chasing.AddTransition(Transition.poiLost, StateID.StartPatrol);
		chasing.AddTransition(Transition.poiInFireingRange, StateID.Attacking);
		
		S_AttackingState attacking = new S_AttackingState();
		attacking.AddTransition(Transition.poiLost, StateID.StartPatrol);
		
		fsm = new S_FSMSystem();
		fsm.AddState(startPatrol);
		fsm.AddState(patrolling);
		fsm.AddState(chasing);
		fsm.AddState(attacking);	
	}
	
	public void subscirbeForFSM (GameObject NPC)
	{
		if(!NPCs.Contains(NPC))
		{
			NPCs.Add (NPC);
		}
	}
	
}