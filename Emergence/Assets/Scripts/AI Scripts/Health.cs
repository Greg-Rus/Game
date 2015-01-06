using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float HitPoints;
	public Vector3 directionOfLastHit; 
	public GameObject deathExplosion;

	private bool _isDead;



	AICore FSM;
	// Use this for initialization
	void Start(){
		if ((FSM = this.GetComponent (typeof(AICore)) as AICore) == null) {
						Debug.Log ("Failed to get AICore script");
		}
		_isDead = false;
	}

	public void setHP(float HP){
		HitPoints = HP;
	}

	public float getCurrentHP(){
		return HitPoints;
	}

	public void repair(float repairAmount){
		HitPoints += repairAmount;
	}

	public void takeDamage(float damage){
		HitPoints -= damage;
		if (HitPoints <= 0f && !_isDead) {
			_isDead = true;
			FSM.isDestroyed();
			explode();
		}
	}
	public void explode(){
		GameObject expl = Instantiate(deathExplosion, transform.position, Quaternion.identity) as GameObject;
		Destroy (expl, 4);
	}
}
