using UnityEngine;
using System.Collections;

public class DieOnCollisionExit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit() {

		Destroy (this.gameObject);

	} 
}
