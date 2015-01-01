using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
	public GameObject explosion;
	public GameObject marker;
	// Use this for initialization

	// Update is called once per frame
	void OnCollisionEnter(Collision hit){
		//Debug.Log ("HIT and rigidbody is: " + hit.rigidbody.isKinematic);
		Debug.Log (hit.collider.name);
		GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
		//GameObject mark = Instantiate(marker, transform.position, Quaternion.identity) as GameObject;
		if (hit.rigidbody && hit.rigidbody.name != "Mako") {
			if (hit.rigidbody.isKinematic)
			{
				Debug.Log ("Projectile hit but object is kinematic");

					//hit.rigidbody.isKinematic = false; 
					//hit.rigidbody.useGravity = true; 


			}
			hit.rigidbody.AddExplosionForce (50000f, transform.position, 5f, 1f, ForceMode.Impulse);
				}
		Destroy(gameObject); // destroy the grenade
		Destroy(expl, 3); // delete the explosion after 3 seconds
	}
	//	void OnTriggerEnter(Collider hit){
//		Debug.Log ("HIT");

//	}


}
