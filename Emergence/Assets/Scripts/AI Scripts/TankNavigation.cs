using UnityEngine;
using System.Collections;

public class TankNavigation : MonoBehaviour {
	
	public Transform[] waypoints;
	public int currentWaypoint = 0;
	private NavMeshAgent nav;
	private float distanceToGround;
	private Vector3 fixedCenterOfMass;
	public bool isNavControlled;
	public bool isBeingAffectedByPhysiscs;
	public bool isGrounded;
	public float physicsSpeed;
	public float distanceToTarget;
	public float magnitudeToTarget;
	public float rigidbodyVelocity;
	public Vector3 desiredVectror;
	

	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		distanceToGround = collider.bounds.extents.y;
		
		fixedCenterOfMass = rigidbody.centerOfMass;
		fixedCenterOfMass.y = distanceToGround;
		rigidbody.centerOfMass = fixedCenterOfMass;
		
		nav.updatePosition = false;
		nav.updateRotation = false;
		nav.destination = waypoints[currentWaypoint].position;
		//usePhysics ();
		
		
	}
	void Update (){
		rigidbodyVelocity = rigidbody.velocity.magnitude;
		//isAffectedByPhysics ();

		patrol ();
		move ();

		
		
	}

	void move()
	{
		desiredVectror = nav.desiredVelocity;
		rigidbody.velocity = desiredVectror;
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
				//usePhysics();
			}	
		}
		
		
		
	}
	
	bool IsGrounded()
	{
		return isGrounded = Physics.Raycast(transform.position, -transform.up, distanceToGround + 0.1f);
		
	}
	

	
	void patrol(){
	
		magnitudeToTarget =  (waypoints [currentWaypoint].position - transform.position).magnitude;
		if (magnitudeToTarget  <= 1.0) 
		{
			currentWaypoint++;
			if (currentWaypoint > waypoints.Length -1) currentWaypoint = 0;
			nav.destination = waypoints[currentWaypoint].position;
		}
		
		
		
	}
}
