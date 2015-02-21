using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class groundedTest : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		float distanceToGround = ai.Body.collider.bounds.extents.y;
		if (Physics.Raycast(ai.Body.transform.position, -ai.Body.transform.up, distanceToGround))
		{
			ai.WorkingMemory.SetItem<bool>("isGrounded", true);
		}
		else
		{
			ai.WorkingMemory.SetItem<bool>("isGrounded", false);
		}
		return ActionResult.NONE;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
    
}