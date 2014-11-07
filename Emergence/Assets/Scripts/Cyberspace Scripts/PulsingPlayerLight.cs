using UnityEngine;
using System.Collections;

public class PulsingPlayerLight : MonoBehaviour {
	public Light actorLight;
	public float maxUp;
	public float maxDown;
	public float pulseFrequency;
	private Vector3 lightPosition;
	private Vector3 newLightPosition;
	private Vector3 moveDirection;
	private float lightIntensity;
	private float dimmer = 1;
	// Use this for initialization
	void Start () {
//		lightPosition = transform.position;
//		moveDirection = Vector3.up;
		lightIntensity = actorLight.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		if (lightIntensity >= maxUp || lightIntensity <= maxDown) {
			dimmer = dimmer * -1f;
		}
		lightIntensity = lightIntensity + (pulseFrequency * Time.deltaTime) * dimmer;
		actorLight.intensity = lightIntensity;

/*		if (transform.position.y >= maxUp) {
			moveDirection = Vector3.down;
				}

		if (transform.position.y <= maxDown) {
			moveDirection = Vector3.up;
				}
		newLightPosition = transform.position;
		newLightPosition += moveDirection * pulseFrequency * Time.deltaTime;
		transform.position = newLightPosition;
*/
	}
}
