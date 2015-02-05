using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
using RAIN.Motion;

[RAINAction]
public class Fire : RAINAction
{
	private RAIN_Minion_Fire attackScript;
	
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		attackScript = ai.Body.GetComponentInChildren<RAIN_Minion_Fire>();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		attackScript.fire();
		return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}