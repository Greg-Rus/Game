using UnityEngine;
using System.Collections;

public class ElevateMemCell : MonoBehaviour {
	
	private float activeSensors = 0;
	private float lastActiveSensors;
	public float maxElevation;
	public float baseLevel;
	private float y;
	private Vector3 position;
	void Start () {


	}
	

	void Update () {
//		Debug.Log (activeSensors);
/*		if (activeSensors != lastActiveSensors) {
			position = transform.position;
			//position.y += maxElevation * activeSensors;
			position.y += 1f;
			transform.position = position;
		
		}

		lastActiveSensors = activeSensors;
*/

	}

	public void increaseFill()
	
	{
		activeSensors+=0.2f;
		position = transform.position;
		position.y = baseLevel + maxElevation * activeSensors;
		transform.position = position;
	}

	public void decreaseFill(){
		activeSensors-=0.2f;
		position = transform.position;
		position.y = baseLevel + maxElevation * activeSensors;
		transform.position = position;

	}

	void onTriggerExit(){
		Debug.Log ("aaa");

	}
}
