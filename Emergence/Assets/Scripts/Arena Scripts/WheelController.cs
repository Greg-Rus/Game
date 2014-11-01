using UnityEngine;
using System.Collections;

public class WheelController : MonoBehaviour {
	public Transform wheelGeometry;
	private RaycastHit hitInfo;
	public Vector3 targetWheelPosition;
	private WheelCollider wheelsCollider;
	private WheelHit hit;

	// Use this for initialization
	void Start () {
		wheelsCollider = gameObject.GetComponent<WheelCollider> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		WheelSuspensionUpdate ();
	}

	void WheelSuspensionUpdate()
	{
		targetWheelPosition = wheelGeometry.localPosition;
		if ( wheelsCollider.GetGroundHit(out hit))	{
			
			targetWheelPosition.y -=  Vector3.Dot (wheelGeometry.position - hit.point, transform.up) -  wheelsCollider.radius;
			
		}
		else{
			
			targetWheelPosition.y = transform.localPosition.y -   wheelsCollider.suspensionDistance;
			
		}
		
		wheelGeometry.transform.localPosition = targetWheelPosition;	
	}
}