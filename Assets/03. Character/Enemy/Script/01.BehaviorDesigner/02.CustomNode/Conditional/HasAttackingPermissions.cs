using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HasAttackPermissions : Conditional
{
	[Header("SharedVariable")]
    [SerializeField] private bool attackingPermissions;
	
	public override TaskStatus OnUpdate()
	{
		if(attackingPermissions)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}