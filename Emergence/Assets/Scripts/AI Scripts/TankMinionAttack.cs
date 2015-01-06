using UnityEngine;
using System.Collections;

public class TankMinionAttack : MonoBehaviour {
	public GameObject projectile;
	public Transform muzzle;
	public float shotSpeed;
	public float reloadTime;
	bool reloading;

	void Awake(){
		reloading = false;
	}
	// Use this for initialization

	public void fire(){
		if (!reloading) {
			GameObject bullet = Instantiate (projectile, muzzle.position, Quaternion.identity) as GameObject;
			bullet.rigidbody.velocity = muzzle.transform.forward * shotSpeed;
			StartCoroutine (reload ());
		}
	}

	IEnumerator reload(){
		reloading = true;
		yield return new WaitForSeconds (reloadTime);
		reloading = false;
	}



}
