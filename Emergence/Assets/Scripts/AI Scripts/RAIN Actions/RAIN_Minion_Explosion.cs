using UnityEngine;
using System.Collections;

public class RAIN_Minion_Explosion : MonoBehaviour {
	
	public GameObject deathExplosion;
	// Use this for initialization

	public void setOffExplosion()
	{
		GameObject expl = Instantiate(deathExplosion, transform.position, Quaternion.identity) as GameObject;
		Destroy (expl, 6);
	}
}
