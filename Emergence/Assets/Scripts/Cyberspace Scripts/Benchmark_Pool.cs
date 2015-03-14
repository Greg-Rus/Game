using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;

public class Benchmark_Pool : MonoBehaviour {
	public MemCellPoolCreator poolControler;
	private ObjectPool pool;
	public int numberOfCycles = 660;
	private GameObject[] objects;
	Stopwatch stopWatch;
	System.IO.StreamWriter file;
	System.IO.StreamWriter storeTimes;
	GameObject spawnedPrefab;
	TimeSpan ts;
	// Use this for initialization
	void Start () {
		pool = poolControler.thePool;
		objects = new GameObject[numberOfCycles];
		stopWatch = new Stopwatch();
		file = new System.IO.StreamWriter("K:\\Logs\\BenchmarkPoolRetrieve_Isolated.txt"); // FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None
		storeTimes = new System.IO.StreamWriter("K:\\Logs\\BenchmarkPoolStore_Isolated.txt");
			
}
	void Update(){
	
		runTest();
	
	}
		
	
		//TimeSpan ts = stopWatch.Elapsed;
		//UnityEngine.Debug.Log(ts.TotalMilliseconds);
	
	void OnApplicationQuit(){
		file.Close();
		storeTimes.Close ();
	}
	
	void runTest(){
	
		stopWatch.Reset ();
		stopWatch.Start();
		
		spawnedPrefab = pool.retrieveObject();
		spawnedPrefab.transform.position = new Vector3 (0, 0, 0);
		
		stopWatch.Stop();
		ts = stopWatch.Elapsed;
		file.WriteLine(ts.TotalMilliseconds + "\t");
		
		stopWatch.Reset ();
		stopWatch.Start();
		
		pool.storeObject(spawnedPrefab);
		
		stopWatch.Stop();
		ts = stopWatch.Elapsed;
		storeTimes.WriteLine(ts.TotalMilliseconds + "\t");
		
	
	}
	


}
