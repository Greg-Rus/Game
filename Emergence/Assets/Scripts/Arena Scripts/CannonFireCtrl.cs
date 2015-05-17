using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CannonFireCtrl : MonoBehaviour {
	public GameObject bulletPrefab;
	public float maxShotPower;
	public float minShotPower;
	public float chargeSpeed;
	public Slider powerSlider;
	public int numberOfSamples;
	public float samplingRate;
	public float samplingIncrease;
	public LineRenderer lineRenderer;
	public GameController myGameController;

	float shotDelta;
	float shotCharge = 0f;

	List<Vector3> positions;
	float shotSpeed;
	bool hasHitSomething;

	// Use this for initialization
	void Start () {
		myGameController = GameController.instance;
		shotDelta = maxShotPower - minShotPower;


	}
	
	// Update is called once per frame
	void Update () {
		if(!myGameController.inMainMenu && !myGameController.inMissionMenu)
		{
			if (Input.GetMouseButton (0)) 
			{
				shotCharge = Mathf.Clamp ( shotCharge + chargeSpeed * Time.deltaTime, 0f, 1f);
				powerSlider.value = shotCharge;
				shotSpeed =  minShotPower + shotDelta * shotCharge;
				//UpdateTrajectory(transform.position, transform.forward,shotSpeed, timePerSegmentInSeconds, maxTravelDistance);
				UpdateTrajectory2(transform.position, transform.forward *shotSpeed, Physics.gravity);
			}
	
			if (Input.GetButtonUp("Fire1")) {
				GameObject bullet;
				bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
				//bullet.rigidbody.AddForce( transform.forward * (minShotPower + shotDelta * shotCharge));
				shotSpeed =  minShotPower + shotDelta * shotCharge;
				bullet.rigidbody.velocity = transform.forward * shotSpeed ;
				shotCharge = 0f;
				powerSlider.value = shotCharge;
				//lineRenderer.SetVertexCount(0);
			}
		}


	}

	void UpdateTrajectory2(Vector3 initialPosition, Vector3 initialVelocity, Vector3 gravity)
	{

		float timeDelta = samplingRate / initialVelocity.magnitude;

		lineRenderer.SetVertexCount(numberOfSamples);
		
		Vector3 position = initialPosition;
		Vector3 velocity = initialVelocity;
		for (int i = 0; i < numberOfSamples; ++i)
		{
			lineRenderer.SetPosition(i, position);
			
			position += velocity * timeDelta + 0.5f * gravity * timeDelta * timeDelta;
			velocity += gravity * timeDelta;
			timeDelta+= samplingIncrease;
		}
	}
}
