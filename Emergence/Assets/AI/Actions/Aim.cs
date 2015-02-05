using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
using RAIN.Motion;

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
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		MoveLookTarget tTarget = MoveLookTarget.GetTargetFromVariable(ai.WorkingMemory, target.VariableName);
		myTargetting.aimTurret(tTarget.TransformTarget);
		return ActionResult.NONE;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}