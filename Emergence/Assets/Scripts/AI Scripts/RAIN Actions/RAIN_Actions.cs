using UnityEngine;
using System.Collections;
using RAIN;
using RAIN.Core;
using RAIN.Action;
using RAIN.Serialization;

[RAINAction]
public class Targeting : RAINAction
{
	private F_Targetting myTargetting;
	
	[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Target to aim for")]
	public GameObject Target;
	public Targeting()
	{
		actionName = "Targetting";
	}
	
	public override string ActionName {
		get {
			return base.ActionName;
		}
		set {
			base.ActionName = value;
		}
	}
	
	public override void Start (AI ai)
	{
		base.Start (ai);
	}
	
	public override ActionResult Execute (AI ai)
	{
		myTargetting.aimTurret(Target.transform);
		return ActionResult.RUNNING;
	}
	
	public override void Stop (AI ai)
	{
		base.Stop (ai);
	}
}

