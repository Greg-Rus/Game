using UnityEngine;
using System.Collections;

public class CarControll : MonoBehaviour {
	public WheelCollider wheelFR;
	public WheelCollider wheelMR;
	public WheelCollider wheelBR;
	public WheelCollider wheelFL;
	public WheelCollider wheelML;
	public WheelCollider wheelBL;

	public Transform wheelTransformFR;
	public Transform wheelTransformMR;
	public Transform wheelTransformBR;
	public Transform wheelTransformFL;
	public Transform wheelTransformML;
	public Transform wheelTransformBL;

	public float maxTorque = 20;
	public float maxSteerAngle = 10;
	private float currentTorque;
	private float currentSteerAngle;
	private Vector3 fixedCenterOfMass;
	// Use this for initialization
	void Start () {
		fixedCenterOfMass = rigidbody.centerOfMass;
		fixedCenterOfMass.y = -0.7f;
		rigidbody.centerOfMass = fixedCenterOfMass;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentTorque = maxTorque * Input.GetAxis ("Vertical");
		currentSteerAngle = maxSteerAngle * Input.GetAxis ("Horizontal");

		wheelBR.motorTorque = currentTorque;
		wheelBL.motorTorque = currentTorque;

		wheelFR.steerAngle = currentSteerAngle;
		wheelFL.steerAngle = currentSteerAngle;
	}

	void Update() {
		wheelTransformFR.localEulerAngles = new Vector3 (0f, wheelFR.steerAngle, 0f );
		wheelTransformFL.localEulerAngles = new Vector3 (0f, wheelFL.steerAngle, 0f );


		wheelTransformFR.Rotate (wheelFR.rpm * -6f *Time.deltaTime, 0, 0);
		wheelTransformMR.Rotate (wheelMR.rpm * -6f *Time.deltaTime, 0, 0);
		wheelTransformBR.Rotate (wheelBR.rpm * -6f *Time.deltaTime, 0, 0);
		wheelTransformFL.Rotate (wheelFL.rpm * -6f *Time.deltaTime, 0, 0);
		wheelTransformML.Rotate (wheelML.rpm * -6f *Time.deltaTime, 0, 0);
		wheelTransformBL.Rotate (wheelBL.rpm * -6f *Time.deltaTime, 0, 0);


	}
}
