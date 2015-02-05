using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
using RAIN.Motion;

[RAINAction("Distance Check")]
public class DistanceToTarget : RAINAction
{
	public Expression Target = new Expression();
	public Expression LessThan = new Expression();
	
	public override ActionResult Execute(AI ai)
	{
		if (Target.IsValid && Target.IsVariable)
		{
			MoveLookTarget tTarget = MoveLookTarget.GetTargetFromVariable(ai.WorkingMemory, Target.VariableName);
			float tDistance = (tTarget.Position - ai.Kinematic.Position).magnitude;
			
			if (LessThan.IsValid && (tDistance >= LessThan.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory)))
			{
				//Debug.Log("Distance: " + tDistance + "  LessThan: " +  LessThan.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory) );
				ai.WorkingMemory.SetItem<bool>("targetInRange",false);
				return ActionResult.FAILURE;
			}
				
		}
		
		ai.WorkingMemory.SetItem<bool>("targetInRange",true);
		Debug.Log ("Done!!!");
		return ActionResult.SUCCESS;
		
	}
}