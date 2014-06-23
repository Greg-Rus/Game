using UnityEngine;
using System.Collections;

public class ElevateMemCell : MonoBehaviour {
	
	public float defElevation = 0.1f;

	private float dTime;
	private Vector3 dYposition;

	public float startElevation;

	float testTime = 0f;
	float testSin;
	public float waveOffset;

	void Start () {
		testTime = waveOffset;
		startElevation = transform.position.y;

	}
	

	void Update () {
		dYposition = transform.position;
		dYposition.y = startElevation + defElevation * waveOffset;

		transform.position = dYposition;


			
	}

	float sinWave(){
		if (testTime >= Mathf.PI*2)
			testTime -= Mathf.PI*2 ;
		testSin = Mathf.Sin (testTime*1)*1;
		testTime += Time.deltaTime;

		return testSin;
	}


}
