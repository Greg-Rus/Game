using UnityEngine;
using System.Collections;

public class CyberCameraControll : MonoBehaviour {

	public GameObject target;
	public float rotateSpeed = 5;
	Vector3 offset;
	Vector3 xRot;
	Transform test;
	float eulerX;
	float eulerY;
	float eulerZ;
	Vector3 newRotation;
	Rigidbody targetRigidbody;
	
	void Start() {
		eulerY = 0f;
		eulerX = 0f;
		targetRigidbody = target.GetComponent<Rigidbody>();
	}
	
	void LateUpdate() {
		
		
		eulerY -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
		eulerY = Mathf.Clamp(eulerY, -80, 80);
		eulerX += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
		eulerX = eulerX % 360;
		transform.localEulerAngles = new Vector3(eulerY, 0, 0);
		
		targetRigidbody.MoveRotation( Quaternion.Euler (new Vector3 (0f, eulerX, 0f) ));
		
		
	}
}