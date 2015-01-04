using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
	public GameObject explosion;
	public float explosionPower;
	public float explosionRadius;

	private TankMinionMobility script;


	void OnCollisionEnter(Collision hit){
		GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
		Collider[] collidersInRange = Physics.OverlapSphere (transform.position, explosionRadius);

		foreach (Collider col in collidersInRange)
		{
			if (col.rigidbody)

			{
				if(col.rigidbody.isKinematic)
				{
					if(script = col.rigidbody.GetComponent("TankMinionMobility") as TankMinionMobility) {
						script.usePhysics();
					}
					script = null;
				}
				col.rigidbody.AddExplosionForce (explosionPower,
				                                 transform.position,
				                                 explosionRadius,
				                                 1f,
				                                 ForceMode.Impulse);
			}
		}
		Destroy(gameObject); // destroy the grenade
		Destroy(expl, 3); // delete the explosion after 3 seconds
	}
}
