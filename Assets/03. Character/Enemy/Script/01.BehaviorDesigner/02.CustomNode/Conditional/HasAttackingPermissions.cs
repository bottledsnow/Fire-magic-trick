using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HasAttackingPermissions : Conditional
{
	[Header("SharedVariable")]
    [SerializeField] private SharedBool attackingPermissions;
	
	public override TaskStatus OnUpdate()
	{
		if(attackingPermissions.Value)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}