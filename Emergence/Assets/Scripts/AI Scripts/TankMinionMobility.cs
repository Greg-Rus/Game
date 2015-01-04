using UnityEngine;
using System.Collections;

public class TankMinionMobility : MonoBehaviour {

	// Use this for initialization
	public Transform[] waypoints;

	public int currentWaypoint = 0;
	private NavMeshAgent nav;
	private float distanceToGround;
	private Vector3 fixedCenterOfMass;
	private bool mustYealdToPhysics;

	public bool isNavControlled;
	public bool isBeingAffectedByPhysiscs;
	public bool isGrounded;
	public float distanceToTarget;
	public float magnitudeToTarget;
	public float rigidbodyVelocity;


	//public Transform rightTrackForcePiont;
	//public Transform leftTrackForcePiont;
	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		isNavControlled = true;
		//FindDistanceToGround ();
		distanceToGround = collider.bounds.extents.y;

		fixedCenterOfMass = rigidbody.centerOfMass;
		fixedCenterOfMass.y = distanceToGround;
		rigidbody.centerOfMass = fixedCenterOfMass;

		nav.updatePosition = true;
		nav.updateRotation = true;
		usePhysics ();


	}
	void FixedUpdate(){


		//IsGrounded ();

	}
	void Update (){

		rigidbodyVelocity = rigidbody.velocity.magnitude;
		patrol ();


	}
	void LateUpdate(){
		isAffectedByPhysics ();
		StartCoroutine(moveTracks ());

	}


	//Toggles the navAgents kinematic controlls.
	IEnumerator moveTracks(){
		if (mustYealdToPhysics) {
			mustYealdToPhysics = false;
			yield return new WaitForFixedUpdate();
				}
		//if ((!IsGrounded ()  || isAffectedByPhysics()) && isNavControlled )
		//{
		//	usePhysics();			 
		//}

		//if (IsGrounded () && !isAffectedByPhysics() &&  !isNavControlled)
		//{
		//	useNavAgent();
		//}
		if (IsGrounded() && !isAffectedByPhysics () && !isNavControlled) 
		{
			useNavAgent();
		}

	}

	bool isAffectedByPhysics()
	{
		return isBeingAffectedByPhysiscs = rigidbody.velocity.magnitude > 0 ? true : false;
	}

	void OnCollisionEnter(Collision col)
	{
		foreach (ContactPoint hit in col.contacts) {
			if (hit.otherCollider.name == "Mako"){
				print("This collider collided with: " + hit.otherCollider.name);
				usePhysics();
			}	
		}

		 
		
	}

	bool IsGrounded()
	{
		return isGrounded = Physics.Raycast(transform.position, -transform.up, distanceToGround);
		
	}

	public void usePhysics()
	{	
		if (!isAffectedByPhysics ()) 
		{
			isNavControlled = false;
			rigidbody.isKinematic = false; 
			rigidbody.useGravity = true; 
			nav.enabled=false;
			mustYealdToPhysics = true;
			Debug.Log ("Switched to Physics!");
		}
	
	}
	private void useNavAgent()
	{
		if (!isNavControlled) 
		{
			isNavControlled = true;
			rigidbody.isKinematic = true; 
			rigidbody.useGravity = false; 
			nav.enabled=true;
			nav.ResetPath ();
			nav.destination = waypoints[currentWaypoint].position;
			//nav.Resume ();
			Debug.Log ("Switched to NavAgent!");	
		}

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
