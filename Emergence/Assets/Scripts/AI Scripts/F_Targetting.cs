using UnityEngine;
using System.Collections;

public class F_Targetting : MonoBehaviour {

	public Transform turretPivot;
	public Transform gunPivot;
	public float smoothTimeDamping = 0.5f;
	
	private float direction;
	private float xAngle;
	private float yAngle;
	private Vector3 toTarget;
	private float yVelocity = 0.0F;
	private float xVelocity = 0.0F;
	private Vector3 relativeGunToTarget;
	//private float angleDelta;
	private float xAngleDelta;
	private float yAngleDelta;
	private Vector3 relativeTargetLocation;
	private Vector3 globalPlaneLocation;
	private Vector3 relativePlaneLocation;
	public bool _resetting = false;
	
	
	void Start () {
		
		
	}
	void Update () {

		if (_resetting) 
		{
			resetTurret();
		}
		
	}
	public void aimTurret(Transform target)
	{
		if(_resetting){
			_resetting = false;
		}
		//Convert target location to local sapce 
		relativeTargetLocation = turretPivot.InverseTransformPoint (target.position);
		relativeGunToTarget = gunPivot.InverseTransformPoint (target.position);
		turnTurret (relativeTargetLocation);
		turnGun (relativeGunToTarget);
	}
	
	float clampAngle(float angle, float lowerMax, float upperMax){
		
		if (angle >= 180f && angle < upperMax) 
			angle = upperMax;
		
		if (angle < 180f && angle > lowerMax) 
			angle = lowerMax;
		
		//Debug.Log (angle);
		return angle;
		
		
	}
	
	void turnGun(Vector3 targetInLocalSpace){
		//Verify if target is on the above or below of the turret in local space. This is used to add or subtract angular motion.
		if (targetInLocalSpace.y <= 0)
			direction = 1f;
		else
			direction = -1;
		
		//Convert target position to Z,Y plane position
		targetInLocalSpace.x = 0f;
		relativePlaneLocation = targetInLocalSpace;
		
		//Convert local plane positin to global coordinates
		globalPlaneLocation = gunPivot.TransformPoint (relativePlaneLocation);
		
		//Calculate vector to turret plane position (in global space)
		toTarget = globalPlaneLocation - gunPivot.position;
		
		//Debug.DrawRay (gunPivot.position, gunPivot.forward *5f, Color.red);
		//Debug.DrawLine (gunPivot.position, globalPlaneLocation , Color.blue);
		
		//Calculate the angle between current forward vector and turret plane target position
		xAngleDelta = Vector3.Angle (gunPivot.forward, toTarget);
		
		//Calculate angle delta to change Y rotation so that over time turret faces target.
		
		xAngle = Mathf.SmoothDampAngle(gunPivot.localEulerAngles.x, 
		                               clampAngle(gunPivot.localEulerAngles.x + (xAngleDelta * direction),20f,300f) ,
		                               ref xVelocity, smoothTimeDamping);
		
		//Apply new rotation to turret using local Euler angles
		gunPivot.localEulerAngles = new Vector3 (xAngle, 0f, 0f);
		//Debug.Log ("Angle Delta: " + angleDelta + " xAngle: " + xAngle);
		
	}
	
	void turnTurret(Vector3 targetInLocalSpace){
		
		//Verify if target is on the right or lefot of the turret in local space. This is used to add or subtract angular motion.
		if (targetInLocalSpace.x >= 0)
			direction = 1f;
		else
			direction = -1;
		
		//Convert target position to X,Z plane position
		targetInLocalSpace.y = 0f;
		relativePlaneLocation = targetInLocalSpace;
		
		//Convert local plane positin to global coordinates
		globalPlaneLocation = turretPivot.TransformPoint (relativePlaneLocation);
		
		//Calculate vector to turret plane position (in global space)
		toTarget = globalPlaneLocation - turretPivot.position;
		
		//Debug.DrawRay (turretPivot.position, turretPivot.forward, Color.red);
		//Debug.DrawLine (turretPivot.position, globalPlaneLocation , Color.blue);
		
		//Calculate the angle between current forward vector and turret plane target position
		yAngleDelta = Vector3.Angle (turretPivot.forward, toTarget);
		
		//Calculate angle delta to change Y rotation so that over time turret faces target.
		yAngle = Mathf.SmoothDampAngle(turretPivot.localEulerAngles.y, 
		                               turretPivot.localEulerAngles.y + (yAngleDelta)*direction ,
		                               ref yVelocity, smoothTimeDamping);
		
		//Debug.Log ("Angle Delta: " + angleDelta + " xAngle: " + yAngle);
		//Apply new rotation to turret using local Euler angles
		turretPivot.localEulerAngles = new Vector3 (0f, yAngle, 0f);
		
	}
	public bool lockOnTarget(float accuracyThreshold){
	//This determines if we can start fireing. Prevetns fireing when player is in range but the turret is not yet alligned.
		if (Mathf.Abs (xAngleDelta) <= accuracyThreshold && Mathf.Abs (yAngleDelta) <= accuracyThreshold) {
			return true;		
		} else {
			return false;		
		}
	}
	
	public void resetTurret(){
		//If this method was called by AI Core then the turret and gun should start rotation towards neutral posiontos facing forward.
		//this is (0,0,0) in local space.
		_resetting = true;
		
		yAngle = Mathf.SmoothDampAngle(turretPivot.localEulerAngles.y, 
		                               0 ,
		                               ref yVelocity, smoothTimeDamping);
		
		//Apply new rotation to turret using local Euler angles
		turretPivot.localEulerAngles = new Vector3 (0f, yAngle, 0f);
		
		xAngle = Mathf.SmoothDampAngle(gunPivot.localEulerAngles.x, 
		                               0,
		                               ref xVelocity, smoothTimeDamping);
		
		//Apply new rotation to turret using local Euler angles
		gunPivot.localEulerAngles = new Vector3 (xAngle, 0f, 0f);
		
		//if both angles are close enough to zero, reset the local rotation to foward facing (0,0,0)
		if (yAngle <= 0.001f && xAngle <= 0.001f) 
		{
			turretPivot.localEulerAngles = new Vector3 (0f, 0f, 0f);
			gunPivot.localEulerAngles = new Vector3 (0f, 0f, 0f);
			_resetting = false;
		}
	}
	
	
}

