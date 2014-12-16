using UnityEngine;
using System.Collections;

public class CarControll : MonoBehaviour {
	public WheelCollider wheelFR;
	public WheelCollider wheelMR;
	public WheelCollider wheelBR;
	public WheelCollider wheelFL;
	public WheelCollider wheelML;
	public WheelCollider wheelBL;
	public Transform wheelParent;
	public WheelCollider[] wheelColliders;

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
	public float centerOfMassOffset;
	public float currentSpeed;


	private float currentTorque;
	private float currentSteerAngle;
	private float lastSteerAngle;
	private float deltaOfSteerAngle;
	private Vector3 fixedCenterOfMass;
	private bool isBraking;

	private float deltaTime;
	private float wheelAngleFR;
	private float wheelAngleFL;
	private float wheelAngleML;
	private float wheelAngleMR;

	void Start () {
		isBraking = false;
		fixedCenterOfMass = rigidbody.centerOfMass;
		fixedCenterOfMass.y = centerOfMassOffset;
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
		//UpdateSlip ();



	}

	void Update() {

		RotateWheels ();
		TurnSteeringWheels ();

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
		wheelML.steerAngle = currentSteerAngle * 0.5f;
		wheelMR.steerAngle = currentSteerAngle * 0.5f;
	}

	void RotateWheels(){
		float deltaTime = Time.fixedDeltaTime;
		
		//wheelTransformMR.Rotate (wheelMR.rpm * 6f *deltaTime, 0, 0);
		wheelTransformBR.Rotate (wheelBR.rpm * 6f *deltaTime, 0, 0);
		
		//wheelTransformML.Rotate (wheelML.rpm * 6f *deltaTime, 0, 0);
		wheelTransformBL.Rotate (wheelBL.rpm * 6f *deltaTime, 0, 0);
	}

	void TurnSteeringWheels() {

		deltaTime = Time.fixedDeltaTime;
		wheelAngleFL=Mathf.Repeat (wheelAngleFL + deltaTime * wheelFL.rpm * 6f, 360.0f);
		wheelAngleFR=Mathf.Repeat (wheelAngleFR + deltaTime * wheelFR.rpm * 6f, 360.0f);

		wheelAngleML=Mathf.Repeat (wheelAngleML + deltaTime * wheelML.rpm * 6f, 360.0f);
		wheelAngleMR=Mathf.Repeat (wheelAngleMR + deltaTime * wheelMR.rpm * 6f, 360.0f);

		wheelTransformFL.localRotation = Quaternion.Euler (
			wheelAngleFL,
			wheelFL.steerAngle,
			0f);

		wheelTransformFR.localRotation = Quaternion.Euler (
			wheelAngleFR,
			wheelFR.steerAngle,
			0f);

		wheelTransformML.localRotation = Quaternion.Euler (
			wheelAngleML,
			wheelML.steerAngle,
			0f);

		wheelTransformMR.localRotation = Quaternion.Euler (
			wheelAngleMR,
			wheelMR.steerAngle,
			0f);

	}

	void UpdateSlip (){

//		double slipAmountD = (0.01 + 0.022/(rigidbody.velocity.magnitude+1)) * slideFactor;
//		float slipAmount = (float)slipAmountD;

		float slipAmount = 0.07f;
		WheelFrictionCurve newCurve = wheelFR.sidewaysFriction;
		newCurve.stiffness = slipAmount;
		wheelFR.sidewaysFriction = newCurve;

		newCurve = wheelMR.sidewaysFriction;
		newCurve.stiffness = slipAmount;
		wheelMR.sidewaysFriction = newCurve;

		newCurve = wheelBR.sidewaysFriction;
		newCurve.stiffness = slipAmount;
		wheelBR.sidewaysFriction = newCurve;

		newCurve = wheelFL.sidewaysFriction;
		newCurve.stiffness = slipAmount;
		wheelFL.sidewaysFriction = newCurve;

		newCurve = wheelML.sidewaysFriction;
		newCurve.stiffness = slipAmount;
		wheelML.sidewaysFriction = newCurve;

		newCurve = wheelBL.sidewaysFriction;
		newCurve.stiffness = slipAmount;
		wheelBL.sidewaysFriction = newCurve;

	}




}
