using UnityEngine;
using System.Collections;

public class SimpleRotation : MonoBehaviour {
	private int rotTog;
	public rotationTogle test;

	// Update is called once per frame
//	void Awake () {
//		rotTog = GetComponent<rotationTogle> ().rotationOn;

//	}

	void Update () {
		if (test.rotationOn == 1) {
						transform.Rotate (0, 50 * Time.deltaTime, 0);
				}
	

	}
}
