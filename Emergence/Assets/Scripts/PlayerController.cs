using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed = 10f;
	public float rotationSpeed = 10f;
	private Vector3 rot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float translation = Input.GetAxis("Vertical") * speed;
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		transform.Translate(0, 0, translation);
		transform.Rotate(0, rotation, 0);

		//rigidbody.AddRelativeForce (Vector3.forward * Input.GetAxis ("Vertical") * speed);
		//rigidbody.AddRelativeTorque (Vector3.up * Input.GetAxis ("Horizontal") * rotationSpeed);

	}
}
