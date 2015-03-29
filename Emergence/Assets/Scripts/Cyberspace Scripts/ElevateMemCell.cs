using UnityEngine;
using System.Collections;

public class ElevateMemCell : MonoBehaviour {
	
	public float activeSensors = 0;
//	private float lastActiveSensors;
	public float maxElevation;
	public float baseLevel;
	public float riseSpeed;
	public float dropSpeed;
	private float y;
	private Vector3 position;
	private float targetElevation ;
	private Transform myTransform;
	void Start () {
	
		targetElevation = baseLevel;
		myTransform = GetComponent<Transform>();

	}
	

	void Update () {
		position = myTransform.position;

		if (position.y < targetElevation - 0.2f) {

			position.y +=  riseSpeed * Time.deltaTime;
			myTransform.position = position;
		
		}
		else if (position.y > targetElevation + 0.2f) {
				
			position.y -=  dropSpeed * Time.deltaTime;
			myTransform.position = position;

		}
		else 
		{
			position.y = targetElevation;
			myTransform.position = position;
		}

//		lastActiveSensors = activeSensors;


	}

	public void increaseFill()
	
	{
//		activeSensors+=0.2f;
//		position = transform.position;
//		position.y = baseLevel + maxElevation * activeSensors;
//		transform.position = position;

		activeSensors+=0.2f;
		targetElevation = baseLevel + maxElevation * activeSensors;
	}

	public void decreaseFill(){
//		activeSensors-=0.2f;
//		position = transform.position;
//		position.y = baseLevel + maxElevation * activeSensors;
//		transform.position = position;

		activeSensors-=0.2f;
		targetElevation = baseLevel + maxElevation * activeSensors;

	}

//	void onTriggerExit(){
//		Debug.Log ("aaa");

//	}
	public void reset(){
		transform.position = Vector3.zero;
		activeSensors = 0f;

	}
}
