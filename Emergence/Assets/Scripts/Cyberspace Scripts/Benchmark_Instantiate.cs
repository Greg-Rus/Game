using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;


public class Benchmark_Instantiate : MonoBehaviour {
	public GameObject memCell;
	private GameObject[] objects;
	public int NumberOfCycles = 660;
	Stopwatch stopWatch;
	System.IO.StreamWriter instFile;
	System.IO.StreamWriter destFile;
	TimeSpan ts;
	// Use this for initialization
	void Start () {
		stopWatch = new Stopwatch();
		instFile = new System.IO.StreamWriter("K:\\Logs\\BenchmarkInstantiate_Isolated.txt"); // FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None
		destFile = new System.IO.StreamWriter("K:\\Logs\\BenchmarkDestroy_Isolated.txt"); // FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None

	
	}
	void Update(){
	
		runTest();
	
	}
	
	// Update is called once per frame
	void OnApplicationQuit(){
		instFile.Close();
		destFile.Close();
	}
	void runTest(){
		stopWatch.Reset();
		stopWatch.Start();
		
		GameObject spawnedPrefab = Instantiate(memCell, new Vector3(0f,0f,0f), Quaternion.identity) as GameObject;
		
		
		stopWatch.Stop();
		ts = stopWatch.Elapsed;
		instFile.WriteLine(ts.TotalMilliseconds + "\t");
		
		
		stopWatch.Reset();
		stopWatch.Start();
		
		Destroy(spawnedPrefab);
		
		stopWatch.Stop();
		ts = stopWatch.Elapsed;
		destFile.WriteLine(ts.TotalMilliseconds + "\t");
	
	}
}
