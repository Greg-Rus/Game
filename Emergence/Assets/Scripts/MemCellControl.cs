/*

using UnityEngine;
using System.Collections;

public class MemCellControl : MonoBehaviour {

	public GameObject memCell;
	public GameObject [,] memIndex;

	public int xSize;
	public int ySize;

	public float iVector;
	public float jVector;

	public Vector3 spawnSpot;

	private float cellSideSize;
	private float sinVal;
	private float lastCounter = 0.0f;

	private float counter = 0f;
	// Use this for initialization
	void Start () {
		cellSideSize = memCell.transform.localScale.x;
		memIndex = new GameObject[xSize,ySize];
		for (int i = 0; i<xSize; i++) {
			counter=0f;
			for (int j = 0 ; j<ySize; j++){
				iVector= (float) i * cellSideSize;
				jVector=(float)j * cellSideSize;

				spawnSpot = new Vector3(iVector, -0.9f, jVector);

				memIndex[i,j]=Instantiate(memCell, spawnSpot, Quaternion.identity) as GameObject;
				memIndex[i,j].GetComponent <ElevateMemCell>().waveOffset = counter++;
					
			}		
		}

	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i<xSize; i++) {

			sinVal = sinWave();

			for (int j = 0 ; j<ySize; j++){

				memIndex[i,j].GetComponent <ElevateMemCell>().waveOffset = sinVal;

			}
			counter++;
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
*/