using UnityEngine;
using System.Collections;

public class RAIN_Minion_Fire : MonoBehaviour {
	public GameObject projectile;
	public Transform muzzle;
	public float shotSpeed;
	
	void Awake(){

	}
	// Use this for initialization
	
	public void fire(){
			GameObject bullet = Instantiate (projectile, muzzle.position, Quaternion.identity) as GameObject;
			bullet.rigidbody.velocity = muzzle.transform.forward * shotSpeed;		
	}
}
