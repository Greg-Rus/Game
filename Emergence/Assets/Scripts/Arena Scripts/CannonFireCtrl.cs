using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CannonFireCtrl : MonoBehaviour {
	public GameObject bulletPrefab;
	public float maxShotPower;
	public float minShotPower;
	public float chargeSpeed;
	public Slider powerSlider;

	float shotDelta;
	float shotCharge = 0f;
	// Use this for initialization
	void Start () {
		shotDelta = maxShotPower - minShotPower;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) 
		{
			shotCharge = Mathf.Clamp ( shotCharge + chargeSpeed * Time.deltaTime, 0f, 1f);
			Debug.Log (shotCharge);
			powerSlider.value = shotCharge;
		}

		if (Input.GetButtonUp("Fire1")) {
			GameObject bullet;
			bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
			bullet.rigidbody.AddForce( transform.forward * (minShotPower + shotDelta * shotCharge));
			shotCharge = 0f;
			powerSlider.value = shotCharge;
		}
	}
}
