using UnityEngine;
using System.Collections;


public class CameraControll : MonoBehaviour {
	public GameObject target;
	public Transform barrel;
	public float rotateSpeed = 5;
	Vector3 offset;
	Vector3 xRot;
	Transform test;
	float eulerX;
	float eulerY;
	float eulerZ;
	Vector3 newRotation;
	float barrelEulerX;
	float barrelOffset =0f;
	
	void Start() {
		//offset = target.transform.position - transform.position;
		eulerY = 0f;
		eulerX = 0f;
	}
	
	void LateUpdate() {


		eulerY -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
		eulerY = Mathf.Clamp(eulerY, -80f, 80f);
		eulerX += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
		eulerX = eulerX % 360;
		transform.localEulerAngles = new Vector3(eulerY, eulerX, 0);

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			barrelOffset +=5f;		
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			barrelOffset -=5f;		
		}



	
//		newRotation = transform.localEulerAngles;
/*		eulerZ = transform.rotation.z;
		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
		float vertical = Input.GetAxis("Mouse Y") * rotateSpeed*-1 * Time.deltaTime;
		newRotation = new Vector3 (vertical, horizontal, 0f);

*/





//		newRotation.x += vertical;
//		newRotation.y += horizontal;

//		Quaternion newRotationQuaternion = Quaternion.Euler (newRotation);
		//transform.rotation = newRotationQuaternion;

//		target.transform.Rotate(0, horizontal, 0);
		barrelEulerX = (Mathf.Clamp (eulerY + barrelOffset, -90f, 0f)) ;
		barrel.localEulerAngles = new Vector3 (barrelEulerX, 0f, 0f);


		target.transform.localEulerAngles = new Vector3 (0f, eulerX , 0f);


	}
}