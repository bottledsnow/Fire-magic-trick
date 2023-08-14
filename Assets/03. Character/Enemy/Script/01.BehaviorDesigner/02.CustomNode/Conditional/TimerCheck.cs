using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TimerCheck : Conditional
{
	public float time;
	public TimerStart timerStart;
	public override TaskStatus OnUpdate()
	{	
		return (Time.time - timerStart.time >= time) ? TaskStatus.Success : TaskStatus.Failure;
	}
}