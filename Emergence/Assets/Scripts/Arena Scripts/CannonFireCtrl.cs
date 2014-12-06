using UnityEngine;
using System.Collections;

public class CannonFireCtrl : MonoBehaviour {
	public GameObject bulletPrefab;
	public float shotPower;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp("Fire1")) {
			GameObject bullet;
			bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
			bullet.rigidbody.AddForce( transform.forward * shotPower);


		}
	}
}
