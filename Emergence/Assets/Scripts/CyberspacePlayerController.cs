using UnityEngine;
using System.Collections;

public class CyberspacePlayerController : MonoBehaviour {
	public float speed = 10f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float forward = Input.GetAxis("Vertical") * speed *Time.deltaTime;
		float strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

		transform.Translate(strafe, 0, forward);

	}
}
