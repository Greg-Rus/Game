using UnityEngine;
using System.Collections;

public class TankTargeting : MonoBehaviour {
	public Transform target;
	public Transform turretPivot;
	public Transform gunPivot;
	public float speed = 0.5f;

	private Transform targetLookAt;
	private Vector3 fromTurretRotation = Vector3.zero;
	private Vector3 toTurretRotation = Vector3.zero;
	private float turretTurnAngle;
	//private Vector3 newRotation;
	private Quaternion newRotation;

	private Vector3 flatVectorToTarget;
	private Vector3 vectorToTarget;
	private Vector3 currentRotation;
	private float direction;
	private Quaternion oldRotation;
	private float yVelocity = 0.0F;
	float angleDelta;
	// Use this for initialization
	void Start () {


	}
	void Update () {



		//flatVectorToTarget = target.position - turretPivot.position;
		//flatVectorToTarget.y = 0f;

		Vector3 relativeTargetLocation = turretPivot.InverseTransformPoint (target.position);

		if (relativeTargetLocation.x >= 0)
						direction = 1f;
				else
						direction = -1;


		relativeTargetLocation.y = 0f;
		Vector3 relativePlaneLocation = relativeTargetLocation;
		Vector3 globalPlaneLocation = turretPivot.TransformPoint (relativePlaneLocation);

		Vector3 toTarget = globalPlaneLocation - turretPivot.position;

		Debug.DrawRay (turretPivot.position, turretPivot.forward, Color.red);
		//Debug.DrawRay (turretPivot.position, target.position - turretPivot.position, Color.green);
		Debug.DrawLine (turretPivot.position, globalPlaneLocation , Color.blue);
		//Debug.Log(target.position);


		//newRotation.x = 0;
		//newRotation.z = 0;
		//Debug.Log (Quaternion.Euler(newRotation));
		//Debug.Log (oldRotation);
		//newRotation = Quaternion.LookRotation(globalPlaneLocation).eulerAngles;
		//turretPivot.rotation =  Quaternion.Euler(newRotation);
		//newRotation = Quaternion.LookRotation (globalPlaneLocation);

		//tu jeszcze jakoś działa
		//newRotation = turretPivot.rotation * Quaternion.FromToRotation (turretPivot.forward, toTarget);

		//turretPivot.rotation = newRotation;
		float angleDif = Vector3.Angle (turretPivot.forward, toTarget);
		Debug.Log (angleDif);
		float yAngle = Mathf.SmoothDampAngle(turretPivot.localEulerAngles.y, 
		                                     turretPivot.localEulerAngles.y + (angleDif)*direction ,
		                                     ref yVelocity, 0.5f);
		turretPivot.localEulerAngles = new Vector3 (0f, yAngle, 0f);
		//Quaternion rotation = turretPivot.rotation;
		//turretPivot.rotation = turretPivot.rotation * Quaternion.Euler(0, yAngle, 0);

		//turretPivot.RotateAround (turretPivot.position, turretPivot.up, Vector3.Angle (turretPivot.forward, toTarget));

	}
	

}
