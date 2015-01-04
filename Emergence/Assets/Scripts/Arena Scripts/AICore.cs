using UnityEngine;
using System.Collections;

public class AICore : MonoBehaviour {
	public TankMinionMobility mobilitySystem;
	public TankMinionPerception perceptionSystem;
	public TankTargeting targetingSystem;
	public Transform Player;

	private bool _hasTarget;
	// Use this for initialization
	void Awake(){
		setupTargetingSystem ();

	}
//	void Start () {	}
	
	// Update is called once per frame
	void LateUpdate () {
		//if player detected start targetting.
		if (perceptionSystem.scannVisionRange ()) 
		{
			if (!_hasTarget) 
			{
				targetAquired ();
			} 

		}
		else if (_hasTarget) 
		{
			targetLost ();
		}
	}

	void targetAquired(){

		targetingSystem.target = Player;
		_hasTarget = true;
	}
	void targetLost(){
		//if target no longer in sensor range turn off scipt and set target to null. Netx time it might me a different PoI

		targetingSystem.target = null;
		targetingSystem.resetTurret ();
		_hasTarget = false;
	}
	void setupTargetingSystem(){
		//initial setup of targeting system
		perceptionSystem.PoI = Player;
		targetingSystem.target = null;
		_hasTarget = false;
	}
}
