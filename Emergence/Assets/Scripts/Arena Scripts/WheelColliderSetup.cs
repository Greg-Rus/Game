using UnityEngine;
using System.Collections;

public class WheelColliderSetup : MonoBehaviour {
	public WheelCollider[] wheelColliders;

	public float Mass;
	public float Radius;
	public float Suspension_Distance;

	public float SS_spring;
	public float SS_damper;
	public float SS_targetPosition;
	
	public float FF_extremum_slip;
	public float FF_extremum_value;
	public float FF_asymptote_slip;
	public float FF_asymptote_value;
	public float FF_stiffness;

	public float SF_extremum_slip;
	public float SF_extremum_value;
	public float SF_asymptote_slip;
	public float SF_asymptote_value;
	public float SF_stiffness;

	public float handbrake_stiffness;

	WheelFrictionCurve newFrictionCurve;
	WheelFrictionCurve preHandBrakeFF;
	WheelFrictionCurve preHandBrakeSF;

	


	void Awake(){
				//WheelCollider[] wheelColliders  = {wheelFR, wheelMR, wheelBR, wheelFL, wheelML, wheelBL} ;
		foreach (WheelCollider wheel in wheelColliders) {
			wheel.mass = Mass;
			wheel.suspensionDistance = Suspension_Distance;
			wheel.radius = Radius;

			newFrictionCurve = wheel.sidewaysFriction;

			newFrictionCurve.asymptoteSlip = SF_asymptote_slip;
			newFrictionCurve.asymptoteValue = SF_asymptote_value;
			newFrictionCurve.extremumSlip = SF_extremum_slip;
			newFrictionCurve.extremumValue = SF_extremum_value;
			newFrictionCurve.stiffness = SF_stiffness;
			wheel.sidewaysFriction = newFrictionCurve;

			newFrictionCurve = wheel.forwardFriction;

			newFrictionCurve.asymptoteSlip = FF_asymptote_slip;
			newFrictionCurve.asymptoteValue = FF_asymptote_value;
			newFrictionCurve.extremumSlip = FF_extremum_slip;
			newFrictionCurve.extremumValue = FF_extremum_value;
			newFrictionCurve.stiffness = FF_stiffness;
			wheel.forwardFriction = newFrictionCurve;

			JointSpring newSuspensionSpring = wheel.suspensionSpring;
			newSuspensionSpring.spring = SS_spring;
			newSuspensionSpring.damper = SS_damper;
			newSuspensionSpring.targetPosition = SS_targetPosition;
			wheel.suspensionSpring = newSuspensionSpring;
		}
	}
/*	void Update(){
		if (Input.GetButtonDown ("Jump") == true) {
		
			for ( int i = 2; i < 6; i++){
				preHandBrakeFF = wheelColliders[i].forwardFriction;
				newFrictionCurve = wheelColliders[i].forwardFriction;
				newFrictionCurve.stiffness = handbrake_stiffness;
				wheelColliders[i].forwardFriction = newFrictionCurve;

				preHandBrakeSF = wheelColliders[i].sidewaysFriction;
				newFrictionCurve = wheelColliders[i].sidewaysFriction;
				newFrictionCurve.stiffness = handbrake_stiffness;
				wheelColliders[i].sidewaysFriction = newFrictionCurve;

				wheelColliders[i].brakeTorque = 50000;
			}
		
		}
		if (Input.GetButtonUp ("Jump") == true) {
		
			for ( int i = 2; i < 6; i++){

				wheelColliders[i].sidewaysFriction = preHandBrakeSF;
				wheelColliders[i].brakeTorque = 0;
				wheelColliders[i].forwardFriction = preHandBrakeFF;

			}
		}


	}*/
}
