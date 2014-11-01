using UnityEngine;
using System.Collections;

public class MemArrayController : MonoBehaviour {
	public GameObject memArray;
	private GameObject [] memIndex;
	private int i;
	public int xSize;
	private float iVector;
	public float cellSideSize;
	private Vector3 spawnSpot;
	private float counter = 0f;
	private Vector3 dYposition;
	private float defElevation = 0.2f;
	private float lastCounter;
	public float startYposition;
	public float startZposition;
	public int startXposition;

	// Use this for initialization
	void Start () {
		cellSideSize = 1.0f;

		memIndex = new GameObject[xSize];
		for (i = 0; i<xSize; i++) {
			iVector = (float)i+0.5f+startXposition * cellSideSize;
		
			spawnSpot = new Vector3 (iVector, startYposition, startZposition);

			memIndex[i] = Instantiate (memArray, spawnSpot, Quaternion.identity) as GameObject;
				}
	}
	
	// Update is called once per frame
	void Update () {
		//counter = 0f;
		foreach (GameObject item in memIndex){

			dYposition = item.transform.position;
			dYposition.y = startYposition + defElevation * sinWave();
			
			item.transform.position = dYposition;
			counter = counter + 1.0f;
		}
		lastCounter = lastCounter+0.01f;
		counter = lastCounter;
	}

	float sinWave(){
		float val = counter;
		if (val >= Mathf.PI*2)	val -= Mathf.PI*2 ;
		val = Mathf.Sin (val*1)*1;
		val += Time.deltaTime;
		
		return val;
	}

}
