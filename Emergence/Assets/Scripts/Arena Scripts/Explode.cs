using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
	public GameObject explosion;
	public float explosionPower;
	public float explosionRadius;

	private TankMinionMobility script;


	void OnCollisionEnter(Collision hit){
		Debug.Log ("Projectile has directly hit: " + hit.collider.name);
		GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
		Collider[] collidersInRange = Physics.OverlapSphere (transform.position, explosionRadius);

		foreach (Collider col in collidersInRange)
		{
			Debug.Log (col.name + " caught in explosion");
			if (col.rigidbody)

			{
				if(col.rigidbody.isKinematic)
				{
					Debug.Log (col.rigidbody.name + " has a Kinematic Rigidbody");
					if(script = col.rigidbody.GetComponent("TankMinionMobility") as TankMinionMobility) {
						Debug.Log (col.rigidbody.name + " has the TankMinionMobility script.");
						script.usePhysics();
						Debug.Log ("Forced Physics from explosion.");
					}
					script = null;
				}
				col.rigidbody.AddExplosionForce (explosionPower,
				                                 transform.position,
				                                 explosionRadius,
				                                 1f,
				                                 ForceMode.Impulse);
				Debug.Log ("Explosion force applied.");
			}
		}
		Destroy(gameObject); // destroy the grenade
		Destroy(expl, 3); // delete the explosion after 3 seconds
	}
}
