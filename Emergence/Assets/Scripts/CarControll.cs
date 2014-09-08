﻿using UnityEngine;
using System.Collections;

public class CarControll : MonoBehaviour {
	public WheelCollider wheelFR;
	public WheelCollider wheelMR;
	public WheelCollider wheelBR;
	public WheelCollider wheelFL;
	public WheelCollider wheelML;
	public WheelCollider wheelBL;
	public Transform wheelParent;

	public Transform wheelTransformFR;
	public Transform wheelTransformMR;
	public Transform wheelTransformBR;
	public Transform wheelTransformFL;
	public Transform wheelTransformML;
	public Transform wheelTransformBL;

	public float maxTorque = 20;
	public float maxSteerAngle = 10;
	public float maxBrakeTorque = 50;
	public float maxSpeed = 50;
	private float currentTorque;
	private float currentSteerAngle;
	private float lastSteerAngle;
	private float deltaOfSteerAngle;
	private Vector3 fixedCenterOfMass;
	private bool isBraking;
	public float currentSpeed;
	private float deltaTime;
	private float wheelAngleFR;
	private float wheelAngleFL;


	// Use this for initialization
	void Start () {
		isBraking = false;
		fixedCenterOfMass = rigidbody.centerOfMass;
		fixedCenterOfMass.y = -0.7f;
		rigidbody.centerOfMass = fixedCenterOfMass;
		lastSteerAngle = currentSteerAngle;
		wheelAngleFR = wheelTransformFR.localEulerAngles.x;
		wheelAngleFL = wheelTransformFL.localEulerAngles.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentSpeed = (Mathf.PI * 2 * wheelFR.radius) * wheelFR.rpm *0.06f;
		currentTorque = maxTorque * Input.GetAxis ("Vertical");
		currentSteerAngle = maxSteerAngle * Input.GetAxis ("Horizontal");
		ApplyTorqueToWheels ();
		ApplySteeringAngleToWheels ();



	}

	void Update() {

		RotateWheels ();
		TurnSteeringWheels ();


//		lastSteerAngle = lastSteerAngle - currentSteerAngle;
//		float FL = wheelFL.steerAngle - wheelTransformFL.localEulerAngles.z;
//		float FR = wheelFR.steerAngle - wheelTransformFR.localEulerAngles.z;
//		wheelTransformFL.localEulerAngles = new Vector3 (wheelTransformFL.localEulerAngles.x - wheelTransformFL.localEulerAngles.z, wheelFL.steerAngle, wheelTransformFL.localEulerAngles.z);
//		wheelTransformFR.localEulerAngles = new Vector3 (wheelTransformFR.localEulerAngles.x - wheelTransformFR.localEulerAngles.z, wheelFR.steerAngle, wheelTransformFR.localEulerAngles.z);
//		wheelTransformFL.Rotate (0, lastSteerAngle - currentSteerAngle, 0);
//		wheelTransformFR.Rotate (0, lastSteerAngle - currentSteerAngle, 0);
//		WheelSuspensionUpdate ();



	}

	void ApplyTorqueToWheels(){
	
		if (((currentSpeed> 0) && (Input.GetAxis("Vertical") <0 )) ||
		    ((currentSpeed< 0) && (Input.GetAxis("Vertical") > 0 )) ||
		    ((Mathf.Abs ( currentSpeed ) < 5) && Input.GetAxis ("Vertical") == 0)){
			isBraking = true;
		}
		else {
			isBraking = false;
			wheelFL.brakeTorque =0;
			wheelFR.brakeTorque =0;
		}
		
		if (isBraking ==false) {
			if (currentSpeed < maxSpeed){
				wheelBR.motorTorque = currentTorque;
				wheelBL.motorTorque = currentTorque;
				wheelMR.motorTorque = currentTorque;
				wheelML.motorTorque = currentTorque;
			}
			else {
				wheelBR.motorTorque = 0;
				wheelBL.motorTorque = 0;
				wheelMR.motorTorque = 0;
				wheelML.motorTorque = 0;
			}
		}
		else {
			wheelFL.brakeTorque = maxBrakeTorque;
			wheelFR.brakeTorque = maxBrakeTorque;
			wheelBR.motorTorque = 0;
			wheelBL.motorTorque = 0;
			wheelMR.motorTorque = 0;
			wheelML.motorTorque = 0;
		}
		


	}

	void ApplySteeringAngleToWheels (){
		wheelFR.steerAngle = currentSteerAngle;
		wheelFL.steerAngle = currentSteerAngle;

	}

	void RotateWheels(){
		float deltaTime = Time.fixedDeltaTime;

//		wheelTransformFR.Rotate (wheelFR.rpm * 6f *deltaTime, 0, 0);
//		wheelTransformFL.Rotate (wheelFL.rpm * 6f *deltaTime, 0, 0);
		
		wheelTransformMR.Rotate (wheelMR.rpm * 6f *deltaTime, 0, 0);
		wheelTransformBR.Rotate (wheelBR.rpm * 6f *deltaTime, 0, 0);
		
		wheelTransformML.Rotate (wheelML.rpm * 6f *deltaTime, 0, 0);
		wheelTransformBL.Rotate (wheelBL.rpm * 6f *deltaTime, 0, 0);
	}

	void TurnSteeringWheels() {
//		deltaOfSteerAngle = currentSteerAngle - lastSteerAngle;
		
//		wheelTransformFL.RotateAround (wheelFL.transform.position, Vector3.up, deltaOfSteerAngle );
//		wheelTransformFR.RotateAround (wheelFR.transform.position, Vector3.up, deltaOfSteerAngle );
		deltaTime = Time.fixedDeltaTime;
		wheelAngleFL=Mathf.Repeat (wheelAngleFL + deltaTime * wheelFL.rpm * 6f, 360.0f);

		wheelAngleFR=Mathf.Repeat (wheelAngleFR + deltaTime * wheelFR.rpm * 6f, 360.0f);

		wheelTransformFL.localRotation = Quaternion.Euler (
			wheelAngleFL,
			wheelFL.steerAngle,
			0f);
//		Debug.Log (Mathf.Repeat (wheelTransformFL.localRotation.eulerAngles.x + deltaTime * wheelFL.rpm * 6f, 360.0f));
		wheelTransformFR.localRotation = Quaternion.Euler (
			wheelAngleFR,
			wheelFR.steerAngle,
			0f);
//		lastSteerAngle = currentSteerAngle;

	}




}
