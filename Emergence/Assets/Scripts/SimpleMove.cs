using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {
public		GameObject TestObject;
public 		float magnitude;

	
	// Update is called once per frame
	void Update () {
		transform.Translate (0,0 , 2 * Time.deltaTime);
	}
}
