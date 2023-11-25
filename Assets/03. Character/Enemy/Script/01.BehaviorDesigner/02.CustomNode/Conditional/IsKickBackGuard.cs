using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsKickBackGuard : Conditional
{
	private EnemyHealthSystem enemyHealthSystem;

	public override TaskStatus OnUpdate()
	{
		if(GetComponent<EnemyHealthSystem>().kickBackGuard)
		{
			return TaskStatus.Failure;
		}
		else
		{
			return TaskStatus.Success;
		}
	}
}