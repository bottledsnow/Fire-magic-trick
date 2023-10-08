using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IgniteState : Conditional
{
	public override TaskStatus OnUpdate()
	{
		if(this.GetComponent<EnemyHealthSystem>().isIgnite)
		{
			return TaskStatus.Success;
		}
		else
		{
			return TaskStatus.Failure;
		}
	}
}