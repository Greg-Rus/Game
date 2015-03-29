using UnityEngine;
using System.Collections;

public class CyberspacePlayerController : MonoBehaviour {
	public float speed = 10f;
	// Use this for initialization
	private Rigidbody myRigidbody;
	private Transform myTransform;
	void Start () {
		myRigidbody = GetComponent<Rigidbody>();
		myTransform = GetComponent<Transform>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float forward = Input.GetAxis("Vertical") * speed *Time.deltaTime;
		float strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

		myRigidbody.MovePosition(myTransform.position + myTransform.forward * forward + myTransform.right * strafe);

	}
}
