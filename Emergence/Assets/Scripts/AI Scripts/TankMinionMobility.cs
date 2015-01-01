using UnityEngine;
using System.Collections;

public class TankMinionMobility : MonoBehaviour {

	// Use this for initialization
	public Transform[] waypoints;

	public int currentWaypoint = 0;
	private NavMeshAgent nav;
	private float distanceToGround;
	private Vector3 fixedCenterOfMass;
	public bool isNavControlled;
	public bool isBeingAffectedByPhysiscs;
	public float distanceToTarget;
	public float magnitudeToTarget;
	public float rigidbodyVelocity;


	//public Transform rightTrackForcePiont;
	//public Transform leftTrackForcePiont;
	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		isNavControlled = true;
		distanceToGround = collider.bounds.extents.y;

		fixedCenterOfMass = rigidbody.centerOfMass;
		fixedCenterOfMass.y = distanceToGround;
		rigidbody.centerOfMass = fixedCenterOfMass;

		nav.updatePosition = true;
		nav.updateRotation = true;


	}
	void Update (){

		rigidbodyVelocity = rigidbody.velocity.magnitude;
		isAffectedByPhysics ();
		Debug.Log ("Grounded is " + IsGrounded());

	}
	void LateUpdate(){
		moveTracks ();
		patrol ();

		}
	//Toggles the navAgents kinematic controlls.
	void moveTracks(){
		if ((!IsGrounded ()  || isAffectedByPhysics()) && isNavControlled )
		{
			usePhysics();			 
		}

		if (IsGrounded () && !isAffectedByPhysics() &&  !isNavControlled)
		{
			useNavAgent();
		}
				

	}
	// Update is called once per frame
	bool isAffectedByPhysics()
	{
		//Debug.Log (rigidbody.velocity.magnitude);
		return isBeingAffectedByPhysiscs = rigidbody.velocity.magnitude > 0 ? true : false;
	}
	/*
	void OnCollisionEnter(Collision hit)
	{
		foreach (ContactPoint contact in hit.contacts){
			if (contact.otherCollider.name != "Terrain"){
			print("This collider collided with: " + contact.otherCollider.name);
				usePhysics();
			}
		} 

	}
	*/
	void OnTriggerEnter(Collider hit)
	{

		if (hit.name != "Terrain"){
			print("This collider collided with: " + hit.name);
			usePhysics();
		}
		 
		
	}

	bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -transform.up, distanceToGround + 0.1f);

	}

	private void usePhysics()
	{
		isNavControlled = false;
		rigidbody.isKinematic = false; 
		rigidbody.useGravity = true; 
		nav.enabled=false;
		Debug.Log ("Physics!");
	}
	private void useNavAgent()
	{
		isNavControlled = true;
		rigidbody.isKinematic = true; 
		rigidbody.useGravity = false; 
		nav.enabled=true;
		nav.ResetPath ();
		nav.destination = waypoints[currentWaypoint].position;

		//nav.Resume ();
		Debug.Log ("NavAgent!");
	}

	void patrol(){


		//The below if is just for debuging
		//if (nav.enabled == true){
		//	distanceToTarget = nav.remainingDistance;
		//	if ( distanceToTarget <= 0.5) Debug.Log ("distance = 0.5!!");
		//}
		magnitudeToTarget =  (waypoints [currentWaypoint].position - transform.position).magnitude;
		if (nav.enabled == true && (waypoints[currentWaypoint].position - transform.position).magnitude  <= 1.0) 
		{
			currentWaypoint++;
			if (currentWaypoint > waypoints.Length -1) currentWaypoint = 0;
			nav.destination = waypoints[currentWaypoint].position;
		}
		

		
	}
}
