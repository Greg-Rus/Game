using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
	public GameObject explosion;
	// Use this for initialization

	// Update is called once per frame
	void OnCollisionEnter(Collision hit){
		Debug.Log ("HIT");
		GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
		if (hit.rigidbody) {
			hit.rigidbody.AddExplosionForce (5000000f, transform.position, 5f, 1f);
				}
		Destroy(gameObject); // destroy the grenade
		Destroy(expl, 3); // delete the explosion after 3 seconds
	}
	//	void OnTriggerEnter(Collider hit){
//		Debug.Log ("HIT");

//	}
}
