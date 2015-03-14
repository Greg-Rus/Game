using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;

public class arrayTest : MonoBehaviour {
	public GameObject memCell;
	public int cycles;
	public GameObject[] array1;
	public GameObject[] array2;
	
	Stopwatch stopWatch;
	System.IO.StreamWriter file;
	System.IO.StreamWriter storeTimes;
	
	// Use this for initialization
	void Start () {
		file = new System.IO.StreamWriter("K:\\Logs\\ArrayTest.txt");
		array1 = new GameObject[cycles];
		array2 = new GameObject[cycles];
		stopWatch = new Stopwatch();
		GameObject test = Instantiate(memCell, Vector3.zero, Quaternion.identity) as GameObject;
		for (int i = 0; i< cycles; i++){
			array1[i] = Instantiate(memCell, new Vector3(i,0f,0f), Quaternion.identity) as GameObject;
			array1[i].SetActive(false);
		}
		

		for (int i=0; i<cycles; i++){
			stopWatch.Reset ();
			stopWatch.Start();
			
			//write(array1[i], i);
			write(i);
		
			stopWatch.Stop();
			TimeSpan ts = stopWatch.Elapsed;
			file.WriteLine(ts.TotalMilliseconds + "\t");
		
		}
		
		
	
	}
	void write(int i){
		//item.SetActive(false);
		GameObject holder = array1[cycles-1];
		if(holder.activeSelf){holder.SetActive(false);}
		else {holder.SetActive(true);}
	
	}
	void OnApplicationQuit(){
		file.Close();
	}
	
	

	

}
