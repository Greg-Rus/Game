using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class Aim : RAINAction
{
	private F_Targetting myTargetting;
	public Expression target = new Expression();
	private List<string> objects;
	private GameObject myTarget;
	
    public override void Start(RAIN.Core.AI ai)
    {
		
		
        base.Start(ai);
		myTargetting = ai.Body.GetComponentInChildren<F_Targetting>();
		myTarget = ai.WorkingMemory.GetItem<GameObject>("DetectedPlayer");
		Debug.Log (myTarget.name);
		//Debug.Log (ai.WorkingMemory.GetItem<GameObject>("DetectedPlayer"));
		//Debug.Log (ai.WorkingMemory.GetItemType("DetectedPlayer"));
		//Debug.Log (target.ToString());
		//objects = ai.WorkingMemory.GetItems();
		
		//foreach (string s in objects){
		//Debug.Log (s);
		//}
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}