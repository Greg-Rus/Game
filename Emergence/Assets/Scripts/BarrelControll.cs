using UnityEngine;
using System.Collections;

public class BarrelControll : MonoBehaviour {
	public float barrelSpeed = 10f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Input.GetAxis ("Mouse ScrollWheel")*barrelSpeed * Time.deltaTime, 0, 0);
	}
}
