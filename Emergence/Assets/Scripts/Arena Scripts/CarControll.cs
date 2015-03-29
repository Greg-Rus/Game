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
	
	public float extremum;
	public float asymptote;
	
	public float antiRoll;
	public Transform centerOfMass;


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
	
	private WheelFrictionCurve wfc;
	public Vector3 relativeVelocity;
	public float slipFactor;
	
	private float handbrakeXDragFactor = 0.5f;
	private float initialDragMultiplierX = 10.0f;
	private float handbrakeTime = 0.0f;
	private float handbrakeTimer = 1.0f;
    Vector3 dragMultiplier = new Vector3(2f, 5f, 1f);
    bool handbrake = false;

	void Start () {
		isBraking = false;
		fixedCenterOfMass = rigidbody.centerOfMass;
		fixedCenterOfMass.y = centerOfMassOffset;
		rigidbody.centerOfMass = fixedCenterOfMass;
		lastSteerAngle = currentSteerAngle;
		wheelAngleFR = wheelTransformFR.localEulerAngles.x;
		wheelAngleFL = wheelTransformFL.localEulerAngles.x;
		wheelColliders  = new WheelCollider[]{wheelFL, wheelFR, wheelML, wheelMR, wheelBL, wheelBR} ;
		//wheelColliders = GetComponentInChildren<WheelColliderSetup>().wheelColliders;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentSpeed = (Mathf.PI * 2 * wheelFR.radius) * wheelFR.rpm *0.06f;
		currentTorque = maxTorque * Input.GetAxis ("Vertical");
		currentSteerAngle = maxSteerAngle * Input.GetAxis ("Horizontal");
		ApplyTorqueToWheels ();
		ApplySteeringAngleToWheels ();
		UpdateFriction(relativeVelocity);
		updateAnitRollBArs();
	}

	void Update() {

		RotateWheels ();
		TurnSteeringWheels ();
		relativeVelocity = transform.InverseTransformDirection(rigidbody.velocity);

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

	
	void UpdateDrag(Vector3 relativeVelocity)
	{
		Vector3 relativeDrag = new Vector3(	-relativeVelocity.x * Mathf.Abs(relativeVelocity.x), 
		                                         -relativeVelocity.y * Mathf.Abs(relativeVelocity.y), 
		                                         -relativeVelocity.z * Mathf.Abs(relativeVelocity.z) );
		
		Vector3 drag = Vector3.Scale(dragMultiplier, relativeDrag);
		
		if(initialDragMultiplierX > dragMultiplier.x) // Handbrake code
		{			
			drag.x /= (relativeVelocity.magnitude / (maxSpeed / ( 1 + 2 * handbrakeXDragFactor ) ) );
			drag.z *= (1 + Mathf.Abs(Vector3.Dot(rigidbody.velocity.normalized, transform.forward)));
			drag += rigidbody.velocity * Mathf.Clamp01(rigidbody.velocity.magnitude / maxSpeed);
		}
		else // No handbrake
		{
			drag.x *= maxSpeed / relativeVelocity.magnitude;
		}
		
		if(Mathf.Abs(relativeVelocity.x) < 5 && !handbrake)
			drag.x = -relativeVelocity.x * dragMultiplier.x;
		
		
		rigidbody.AddForce(transform.TransformDirection(drag) * rigidbody.mass * Time.deltaTime);
	}
	
	void UpdateFriction(Vector3 relativeVelocity)
	{
		float sqrVel = relativeVelocity.x * relativeVelocity.x * slipFactor;

		// Add extra sideways friction based on the car's turning velocity to avoid slipping
		extremum = Mathf.Clamp(300 - sqrVel, 0, 300);
		asymptote =Mathf.Clamp(150 - (sqrVel / 2), 0, 150);
		
		foreach (WheelCollider w in wheelColliders)
		{
			wfc = w.sidewaysFriction;
			wfc.extremumValue = extremum;
			wfc.asymptoteValue = asymptote;
			w.sidewaysFriction = wfc;
			
			wfc = w.forwardFriction;
			wfc.extremumValue = extremum;
			wfc.asymptoteValue = asymptote;
			w.forwardFriction = wfc;
			
		}
	}
	
	void updateAnitRollBArs()
	{
		float travelL = 1f;
		float travelR = 1f;
		WheelHit hit;
		
		for (int i= 0; i<6 ;i++)
		{
			WheelCollider wheelL = wheelColliders[i];
			WheelCollider wheelR = wheelColliders[i+1];
		
			bool groundedL = wheelL.GetGroundHit(out hit);
			if (groundedL)
			{
				travelL = (-wheelL.transform.InverseTransformPoint(hit.point).y - wheelL.radius) / wheelL.suspensionDistance;
			}
			bool groundedR = wheelR.GetGroundHit(out hit);
			if (groundedR)
			{
				travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y - wheelR.radius) / wheelR.suspensionDistance;
			}
			float anitRollForce = (travelL - travelR) * antiRoll;
			
			if(groundedL)
			{
				rigidbody.AddForceAtPosition(wheelL.transform.up * - anitRollForce, wheelL.transform.position);
			}
			if(groundedR)
			{
				rigidbody.AddForceAtPosition(wheelR.transform.up * anitRollForce, wheelR.transform.position);
			}
			i++;
		}
	
	}





























}
