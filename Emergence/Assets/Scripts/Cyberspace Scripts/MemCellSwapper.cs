using UnityEngine;
using System.Collections;

public class MemCellSwapper : MonoBehaviour {
	public GameObject memCell;
	
	public float bufferSquareSize;
	//public int objectPoolSize;
	private ObjectPool memCellPool;
	public MemCellPoolCreator pool;
	float curX;
	float curZ;
	float lBound;
	float rBound;
	float tBound;
	float bBound;
	float ySpawnPosition;
	Vector3 position;
	Vector3 spawnSpot;
	enum direction {left, right, up, down};

	void Awake(){


	
	}

	

	// Use this for initialization
	void Start () {

		//Debug.Log ("Starting");
		memCellPool = pool.thePool;
		Debug.Log ("Pool refernce set");
		//memCellPool = *pool.getPoolRef ();

		//memCellPool = new ObjectPool (memCell, objectPoolSize);


		ySpawnPosition = memCell.transform.localScale.y * -0.5f;
		position = transform.position;
		curX = position.x;
		curZ = position.z;
		lBound = (int)curX;
		rBound = (int)(curX + 1f);
		tBound = (int)(curZ + 1f);
		bBound = (int)curZ;
		

	}
	
	// Update is called once per frame
	void Update () {
		
		position = transform.position;
		curX = position.x;
		curZ = position.z;
		//Debug.Log (curX + " -- " + curZ +  "-- " + position);
		//Debug.Log (position);
		
		
		
		if (curX < lBound) {
			
			getXbounds();
			//getZbounds();
			//Debug.Log ( "Left " + lBound);
			spawnMemCellRow(lBound - bufferSquareSize, bBound - bufferSquareSize, direction.up);
		}
		
		if (curX > rBound) {
			getXbounds();
			//getZbounds();
			//Debug.Log ( "Right " + rBound);
			spawnMemCellRow(lBound + bufferSquareSize , bBound - bufferSquareSize, direction.up);
		}
		
		if (curZ > tBound) {
			//getXbounds();
			getZbounds();
			//Debug.Log ( "Top "+ bBound);
			spawnMemCellRow(lBound - bufferSquareSize, bBound + bufferSquareSize , direction.right );
			
		}
		if (curZ < bBound) {
			//getXbounds();
			getZbounds();
			//Debug.Log ( "Bottom " + tBound);
			spawnMemCellRow(lBound - bufferSquareSize, bBound - bufferSquareSize, direction.right );
		}
	}
	
	void spawnMemCellRow(float i, float j, direction dir)
	{
		
		i = i + 0.5f;
		j = j + 0.5f;
		
		//Debug.Log (i + " " + j + " " + dir);
		//Debug.Log (lBound + " -- " + bBound);
		if (dir == direction.right) {
			float rowEnd = i + bufferSquareSize * 2 + 1f;	
			
			while (i < rowEnd) {
				spawnSpot = new Vector3 (i, ySpawnPosition, j);
				
				GameObject spawnedPrefab = memCellPool.retrieveObject();
				spawnedPrefab.transform.position = spawnSpot;
				i = i + 1f;
			}
		}
		else if (dir == direction.up) {
			float rowEnd = j + bufferSquareSize * 2 + 1f;
			
			while (j < rowEnd) {
				spawnSpot = new Vector3 (i, ySpawnPosition, j);
				GameObject spawnedPrefab = memCellPool.retrieveObject();
				spawnedPrefab.transform.position = spawnSpot;		
				j = j + 1f;
			}
			
		}
	}
	
	void getXbounds(){
		lBound = (int)position.x;
		rBound = lBound + 1f;
	}
	void getZbounds(){
		bBound = (int)position.z;
		tBound = bBound + 1f;
		
	}

	void OnTriggerExit(Collider other){
		other.GetComponent<ElevateMemCell>().reset();
		memCellPool.storeObject (other.gameObject);

	}
}
