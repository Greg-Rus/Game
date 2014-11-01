using UnityEngine;
using System.Collections;

public class MemCellBuffer : MonoBehaviour {
	public GameObject memCell;
	private Vector3 CurrentPosition;
	private float curX;
	private float curZ;
	public float bufferSize = 2f;
	private float memCellSize = 1.0f;
	private Vector3 spawnSpot;
	private float i;
	private float j;

	private float oldX;
	private float oldZ;
	// Use this for initialization
	void Start () {
		oldX = CurrentPosition.x;
		oldZ = CurrentPosition.z;

		for (int i = (int)(oldX - bufferSize); i < (int)(oldX + bufferSize); i++) {
			for (int j = (int)(oldZ - bufferSize); j < (int)(oldZ + bufferSize); j++){
				spawnSpot = new Vector3(i*memCellSize, 0.0f, j*memCellSize);
				Instantiate(memCell, spawnSpot, Quaternion.identity);

			}
		
		}
	}
	
	// Update is called once per frame
	void Update () {
		CurrentPosition = transform.position;
		curX = CurrentPosition.x;
		curZ = CurrentPosition.z;
		curX = (int)curX;
		curZ = (int)curZ;
		print (curX + " " + curZ);
		if (curZ != oldZ) {
			i = curX - bufferSize;
			//for (int i = (int)(curX + (bufferSize * (curX - oldX))); i < (int)(oldX + bufferSize); i++) 
			while (i <= curX + bufferSize) {
					spawnSpot = new Vector3 (i, 0.0f, curZ + ((curZ - oldZ) * bufferSize));
					Instantiate (memCell, spawnSpot, Quaternion.identity);
					print (spawnSpot);
					i = i + memCellSize;

			}
			oldZ = curZ;
		}
		if (curX != oldX) {
			j = curZ - bufferSize;
			//for (int i = (int)(curX + (bufferSize * (curX - oldX))); i < (int)(oldX + bufferSize); i++) 
			while (j <= curZ + bufferSize) {
				spawnSpot = new Vector3 (curX + ((curX - oldX) * bufferSize), 0.0f, j);
					Instantiate (memCell, spawnSpot, Quaternion.identity);
					print (spawnSpot);
					j = j + memCellSize;
	
			}
			oldX = curX;
		}
/*			for (int i = (int)(curX - bufferSize); i < (int)(curX + bufferSize); i++)
			{
				spawnSpot = new Vector3(i*memCellSize, 0.0f, (curZ+(oldZ-curZ)*bufferSize)	);
				Instantiate(memCell, spawnSpot, Quaternion.identity);
				print (spawnSpot);
			}

			oldZ=curZ;
		}
		*/
//		if (curX != oldX)
//		{
//			for (int i = (int)(curZ - bufferSize); i < (int)(curZ + bufferSize); i++)
//			{
//				spawnSpot = new Vector3(curX+(oldX-curX)*bufferSize, 0.0f, i*memCellSize 	);
//				Instantiate(memCell, spawnSpot, Quaternion.identity);
//			}
//			oldX=curX;


	}
}
