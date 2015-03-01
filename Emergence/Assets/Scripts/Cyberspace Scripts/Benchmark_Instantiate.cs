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
	// Use this for initialization
	void Start () {
		stopWatch = new Stopwatch();
		instFile = new System.IO.StreamWriter("K:\\Logs\\BenchmarkInstantiate_Isolated.txt"); // FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None
		destFile = new System.IO.StreamWriter("K:\\Logs\\BenchmarkDestroy_Isolated.txt"); // FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None
		
		
		objects = new GameObject[NumberOfCycles];
		
		for(int i=0; i< NumberOfCycles; i++){
			stopWatch.Reset();
			stopWatch.Start();
			
			GameObject spawnedPrefab = Instantiate(memCell, new Vector3(i,0f,0f), Quaternion.identity) as GameObject;
			
			
			stopWatch.Stop();
			TimeSpan ts = stopWatch.Elapsed;
			instFile.WriteLine(ts.TotalMilliseconds + "\t");
			
			objects[i] = spawnedPrefab;
		
		}
		
		for (int i=0; i< NumberOfCycles; i++){
			stopWatch.Reset();
			stopWatch.Start();
			
			Destroy(objects[i]);
			
			stopWatch.Stop();
			TimeSpan ts = stopWatch.Elapsed;
			destFile.WriteLine(ts.TotalMilliseconds + "\t");
			
		}
	
	}
	
	// Update is called once per frame
	void OnApplicationQuit(){
		instFile.Close();
		destFile.Close();
	}
}
