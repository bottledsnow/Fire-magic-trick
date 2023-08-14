using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TimerStart : Action
{
    public float time;
	public override void OnStart()
	{
		time = Time.time;
	}
}