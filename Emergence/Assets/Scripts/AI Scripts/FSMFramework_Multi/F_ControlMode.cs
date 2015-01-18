using UnityEngine;
using System.Collections;

public class F_ControlMode : MonoBehaviour {

	public bool isNavControlled;
	public bool isBeingAffectedByPhysiscs;
	public bool isGrounded;

	private NavMeshAgent nav;
	private float distanceToGround;
	private Vector3 fixedCenterOfMass;
	private bool mustYealdToPhysics;
	private Vector3 destinationBeforeHit;
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
		usePhysics ();
	}
	
	// Update is called once per frame
	void LateUpdate(){
		isAffectedByPhysics ();
		StartCoroutine(updateControllMode ());
	}
	IEnumerator updateControllMode(){
		if (mustYealdToPhysics) {
			mustYealdToPhysics = false;
			yield return new WaitForFixedUpdate();
		}
	
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
			if (hit.otherCollider.rigidbody && hit.otherCollider.rigidbody.name == "Mako"){
				print("This collider collided with: " + hit.otherCollider.rigidbody.name);
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
			//destinationBeforeHit = nav.destination;
			isNavControlled = false;
			rigidbody.isKinematic = false; 
			rigidbody.useGravity = true; 
			//nav.enabled=false;
			nav.updatePosition = false;
			nav.updateRotation = false;
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
			//nav.enabled=true;
			nav.updatePosition = true;
			nav.updateRotation = true;
			//nav.ResetPath ();
			//nav.destination = destinationBeforeHit;
			//nav.Resume ();
			Debug.Log ("Switched to NavAgent!");	
		}
		
	}
}
