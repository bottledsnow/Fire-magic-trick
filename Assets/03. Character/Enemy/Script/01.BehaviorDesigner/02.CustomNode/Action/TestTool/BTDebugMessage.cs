using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BTDebugMessage : Action
{
	[Header("DebugMessage")]
	[SerializeField] private string message;

	public override TaskStatus OnUpdate()
	{
		Debug.Log(message);
		return TaskStatus.Success;
	}
}