using UnityEngine;
using System.Collections;

public class AICore : MonoBehaviour {
	public TankMinionMobility mobilitySystem;
	public TankMinionPerception perceptionSystem;
	public TankTargeting targetingSystem;
	public TankMinionAttack weaponSystem;
	public Health healthSystem;
	public Transform Player;
	public State currentState;
	public Transform[] patrolWaypoints; //Number of waypoints must be greater than one.
	public float attackRange;
	public float gunAccuracy;
	public float hitPoints;

	private bool _hasTarget;
	private bool _startPatrol;
	//private bool _isDead;
	public int currentWaypoint;

	public enum State {Idle, StartPatrol, Patroling, Chase, Attack, Dead}
	// Use this for initialization
	void Awake(){
		//The monion scans for the player by default.
		setupTargetingSystem ();
		currentState = State.StartPatrol;
		healthSystem.setHP (hitPoints);
		//_isDead = false;

	}
//	void Start () {	}
	
	// Update is called once per frame
	void LateUpdate () {


		UpdateState ();
	}

	void UpdateState(){
		switch (currentState)
		{
			case State.Idle : UpdateIdle(); break;
			case State.StartPatrol : StartPatrol();break;
			case State.Patroling : UpdatePatrol();break;
			case State.Chase : UpdateChase();break;
			case State.Attack : UpdateAttack();break;
			case State.Dead : UpdateDead();break;

		}
	}
	void StartPatrol(){
		//Called when patrol is started or restarted. Choses the closes waypoint on the patrol route and sets state to patroling.
		Transform closestWaypoint = patrolWaypoints [0];
		currentWaypoint = 0;

		float distanceToLastWaypoint = (patrolWaypoints [0].position - transform.position).magnitude;
		for (int i=1; i<patrolWaypoints.Length; i++) {
			float distanceToWaypoint = (patrolWaypoints[i].position - transform.position).magnitude;	
			if (distanceToWaypoint <= distanceToLastWaypoint){
				closestWaypoint = patrolWaypoints[i];
				currentWaypoint = i;
			}
		}
		mobilitySystem.setDestination (closestWaypoint);
		mobilitySystem.setStoppingDistance (0f);
		currentState = State.Patroling;
	}

	void UpdatePatrol(){
		if ( mobilitySystem.distanceToDestination(patrolWaypoints[currentWaypoint])  <= 1.0) 
		{
			//Debug.Log ("Reached Waypoint: " + currentWaypoint);
			currentWaypoint++;
			if (currentWaypoint > patrolWaypoints.Length -1) currentWaypoint = 0;
			mobilitySystem.setDestination(patrolWaypoints[currentWaypoint]);
			mobilitySystem.setStoppingDistance (0f);
		}
	}

	void UpdateIdle(){
		}
	void UpdateChase(){
		mobilitySystem.setStoppingDistance (attackRange);
		moveToAttackDistance ();
		if (mobilitySystem.distanceToDestination (Player) <= attackRange &&
		    targetingSystem.lockOnTarget(gunAccuracy)) {
			currentState = State.Attack;		
		}
	}
	void UpdateAttack(){
		if (mobilitySystem.distanceToDestination (Player) > attackRange && perceptionSystem.targetInSight()) {
			currentState = State.Chase;
		}
		weaponSystem.fire ();


		}
	void UpdateDead(){
		mobilitySystem.usePhysics ();
		mobilitySystem.enabled = false;
		perceptionSystem.enabled = false;
		targetingSystem.enabled = false;
		this.enabled = false;
		}

	public void targetAquired(){

		targetingSystem.target = Player;
		//_hasTarget = true;
		currentState = State.Chase;
	}
	public void targetLost(){
		//if target no longer in sensor range turn off scipt and set target to null. Next time it might be a different PoI

		targetingSystem.target = null;
		targetingSystem.resetTurret ();
		//_hasTarget = false;
		currentState = State.StartPatrol;
	}
	void setupTargetingSystem(){
		//initial setup of targeting system
		perceptionSystem.PoI = Player;
		targetingSystem.target = null;
		//_hasTarget = false;
	}
	void moveToAttackDistance(){
		mobilitySystem.setDestination (Player);
	}

	public void isDestroyed(){
		currentState = State.Dead;
	}
}
