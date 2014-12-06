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



	


	void Awake(){
				//WheelCollider[] wheelColliders  = {wheelFR, wheelMR, wheelBR, wheelFL, wheelML, wheelBL} ;
		foreach (WheelCollider wheel in wheelColliders) {
			wheel.mass = Mass;
			wheel.suspensionDistance = Suspension_Distance;
			wheel.radius = Radius;

			WheelFrictionCurve newFrictionCurve = wheel.sidewaysFriction;

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
}
