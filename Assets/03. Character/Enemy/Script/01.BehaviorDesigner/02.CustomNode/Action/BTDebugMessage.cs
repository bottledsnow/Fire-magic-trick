using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BTDebugMessage : Action
{
	public string message;
	public override void OnStart()
	{
		Debug.Log(message);
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}