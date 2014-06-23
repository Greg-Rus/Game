using UnityEngine;
using System.Collections;

public class MouseClick : MonoBehaviour {
	public string GOname;
	private int rotOn;
	public rotationTogle test;

//	void Awake (){
//		rotOn =GetComponent<rotationTogle>().rotationOn;
//	}
	
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown(){
		Debug.Log ("Clicked " + GOname);
		test.rotationSwitch();
	}
}
