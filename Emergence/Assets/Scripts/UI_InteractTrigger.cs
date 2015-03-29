using UnityEngine;
using System.Collections;

public class UI_InteractTrigger : MonoBehaviour {
	
	public GameController myGameController;
	RaycastHit hit;
	Transform myTransform;
	public LayerMask UI_InterractLayer;
	public string hitInteractableObject;
	public float rayLenth;
	// Use this for initialization
	void Awake () {
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 forward = myTransform.forward * rayLenth;
		Debug.DrawRay(transform.position, forward, Color.green);
		if (Physics.Raycast(myTransform.position, forward, out hit, rayLenth, UI_InterractLayer))
		{
			if(hitInteractableObject != hit.collider.name)
			{
				hitInteractableObject= hit.collider.name;
				myGameController.playerTargetingInteractable(hitInteractableObject);
			}
		}
		else if(hitInteractableObject != null) 
		{
			myGameController.playerStoppedTargetingInteractable();
			hitInteractableObject = null;
		}
	}
	
}
