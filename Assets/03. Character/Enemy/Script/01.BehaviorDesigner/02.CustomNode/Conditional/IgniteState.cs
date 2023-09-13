using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IgniteState : Conditional
{
	public override TaskStatus OnUpdate()
	{
		if(this.GetComponent<Enemy_Health>().isIgnite)
		{
			return TaskStatus.Success;
		}
		else
		{
			return TaskStatus.Failure;
		}
	}
}