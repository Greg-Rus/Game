using UnityEngine;
using System.Collections;

public class F_Stats : MonoBehaviour {

	public float HitPoints;
	public GameObject deathExplosion;
	private bool isDead;
	
	void Start()
	{
		isDead = false;
	}
	
	public void repair(float repairAmount){
		HitPoints += repairAmount;
	}
	
	public void takeDamage(float damage){
		HitPoints -= damage;
		if (HitPoints <= 0f && !isDead) {
			isDead = true;
			GetComponent<F_TankController>().SetTransition(Transition.isDestroyed);
		}
	}
	public void explode(){
		GameObject expl = Instantiate(deathExplosion, transform.position, Quaternion.identity) as GameObject;
		Destroy (expl, 4);
	}
}
