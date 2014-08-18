using UnityEngine;
using System.Collections;


public class CameraControll : MonoBehaviour {
	public GameObject target;
	public float rotateSpeed = 5;
	Vector3 offset;
	Vector3 xRot;
	Transform test;
	float eulerX;
	float eulerY;
	
	void Start() {
		//offset = target.transform.position - transform.position;
	}
	
	void LateUpdate() {
/*		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
		target.transform.Rotate(0, horizontal, 0);
	
		
		float desiredAngle = target.transform.eulerAngles.y;

		Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);



		transform.position = target.transform.position - ( rotation * offset);
		transform.RotateAround (target.transform.position, Vector3.right ,vertical);
		
		transform.LookAt(target.transform);
*/
	
		eulerX = transform.eulerAngles.x;
		eulerY = transform.eulerAngles.y;

		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
		float vertical = Input.GetAxis("Mouse Y") * rotateSpeed*-1 * Time.deltaTime;

		eulerX += vertical;
		eulerY += horizontal;

		Quaternion newRotation = Quaternion.Euler (eulerX, eulerY, 0);
		transform.rotation = newRotation;

		target.transform.Rotate(0, horizontal, 0);
		//transform.Rotate (vertical*-1f, 0, 0);
		//transform.Rotate (0, horizontal, 0);


	

		//transform.RotateAround (target.transform.position, Vector3.right ,vertical);
		//transform.RotateAround (target.transform.position, Vector3.up ,horizontal);

		//transform.LookAt(target.transform);

		//transform.position = target.transform.position - offset;

	}
}