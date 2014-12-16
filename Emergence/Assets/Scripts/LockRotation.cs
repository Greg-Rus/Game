using UnityEngine;
using System.Collections;

public class LockRotation : MonoBehaviour {
	public bool lockXAxis;
	public bool lockYAxis;
	public bool lockZAxis;
	private Vector3 rotation;
	// Update is called once per frame
	void LateUpdate () {
		rotation = transform.localEulerAngles;
		if (lockXAxis) {
			rotation.x = 0f;
		}
		if (lockYAxis) {
			rotation.y = 0f;
		}
		if (lockZAxis) {
			rotation.z = 0f;
		}
		transform.localEulerAngles = rotation;
	}
}
