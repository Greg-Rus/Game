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
	// Use this for initialization
	void Start () {
		pool = poolControler.thePool;
		objects = new GameObject[numberOfCycles];
		stopWatch = new Stopwatch();
		file = new System.IO.StreamWriter("K:\\Logs\\BenchmarkPoolRetrieve_Isolated.txt"); // FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None
		storeTimes = new System.IO.StreamWriter("K:\\Logs\\BenchmarkPoolStore_Isolated.txt");
		
		for (int i =0; i<numberOfCycles; i++){
			stopWatch.Reset ();
			stopWatch.Start();
			
			GameObject spawnedPrefab = pool.retrieveObject();
			spawnedPrefab.transform.position = new Vector3 (i, 0, 0);
			
			stopWatch.Stop();
			TimeSpan ts = stopWatch.Elapsed;
			file.WriteLine(ts.TotalMilliseconds + "\t");
			
			objects[i] = spawnedPrefab;
		}
		
		for (int i =0; i<numberOfCycles; i++){
			stopWatch.Reset ();
			stopWatch.Start();
			
			pool.storeObject(objects[i]);
			
			stopWatch.Stop();
			TimeSpan ts = stopWatch.Elapsed;
			storeTimes.WriteLine(ts.TotalMilliseconds + "\t");
		}
		//TimeSpan ts = stopWatch.Elapsed;
		//UnityEngine.Debug.Log(ts.TotalMilliseconds);
	}
	void OnApplicationQuit(){
		file.Close();
		storeTimes.Close ();
	}
	


}
