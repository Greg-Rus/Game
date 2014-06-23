using UnityEngine;
using System.Collections;

public class rotationTogle : MonoBehaviour {

	// Use this for initialization
	public int rotationOn =1;

	// Update is called once per frame
	void Update () {
	
	}

	public void rotationSwitch(){
		rotationOn *= (-1);
	}
}
