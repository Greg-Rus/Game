using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;

public class Timer : MonoBehaviour {

	Stopwatch stopWatch;
	System.IO.StreamWriter file;
	TimeSpan ts;
	
	public Timer(string filePath){
		file = new System.IO.StreamWriter(filePath);
	}

	public void start(){
		stopWatch.Reset ();
		stopWatch.Start();
	}
	
	public void stop(){
		stopWatch.Stop();
		ts = stopWatch.Elapsed;
		file.WriteLine(ts.TotalMilliseconds + "\t");
	}
	
		void OnApplicationQuit(){
	file.Close();
	}

}
