using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class reset_turret : RAINAction
{
	private F_Targetting myTargetting;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		myTargetting = ai.Body.GetComponentInChildren<F_Targetting>();
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		myTargetting.resetTurret();
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}