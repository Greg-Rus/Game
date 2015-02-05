using UnityEngine;
using System.Collections;
using RAIN.Core;

public class Explode : MonoBehaviour {
	public GameObject explosion;
	public float explosionPower;
	public float explosionRadius;

	private TankMinionMobility script;
	private Health healthManager;
	private F_Stats NPCStats;
	private F_ControlMode F_script;
	private AIRig tRig;


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
					
					if(F_script = col.rigidbody.GetComponent<F_ControlMode>()) {
						F_script.usePhysics();
					}
					F_script = null;
					
				}
				col.rigidbody.AddExplosionForce (explosionPower,
				                                 transform.position,
				                                 explosionRadius,
				                                 1f,
				                                 ForceMode.Impulse);
				if (healthManager = col.GetComponent(typeof(Health)) as Health){
					healthManager.takeDamage(20f);
				}
				tRig = col.rigidbody.GetComponentInChildren<AIRig>();
				Debug.Log (tRig);
				if (tRig != null)
				{
					Debug.Log (tRig);
					float currentHelath = tRig.AI.WorkingMemory.GetItem<float>("health");
					tRig.AI.WorkingMemory.SetItem<float>("health", currentHelath - 20f);
				}
				
				if (NPCStats = col.GetComponent(typeof(F_Stats)) as F_Stats){
					NPCStats.takeDamage(20f);
				}
			}
		}
		Destroy(gameObject); // destroy the grenade
		Destroy(expl, 3); // delete the explosion after 3 seconds
	}
}
