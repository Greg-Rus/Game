using UnityEngine;
using System.Collections;

public class ShieldControl : MonoBehaviour {
	MeshRenderer rend;
	Color originalColour;
	public float minAlpha;
	public float maxAlpha;
	
	public float newAlpha;
	// Use this for initialization
	void Start () {
	
		//rend = GetComponent<Renderer>();
		rend = GetComponent<MeshRenderer>();
		originalColour = rend.material.color;
		rend.enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.R)){
			rend.enabled = true;
		}
		if (Input.GetKey(KeyCode.R))
		{

			rend.material.color = new Color(originalColour.r, originalColour.g, originalColour.b, maxAlpha);
			Debug.Log(rend.material.color);
		}
		if (Input.GetKeyUp(KeyCode.R)){
			rend.material.color = originalColour;
			rend.enabled = false;
		}
	}
	
}

