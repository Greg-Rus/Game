using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class explode : RAINAction
{
	RAIN_Minion_Explosion myDeathExplosion;
    public override void Start(RAIN.Core.AI ai)
    {
		myDeathExplosion = ai.Body.GetComponentInChildren<RAIN_Minion_Explosion>();
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		myDeathExplosion.setOffExplosion();
        return ActionResult.SUCCESS;
		
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}