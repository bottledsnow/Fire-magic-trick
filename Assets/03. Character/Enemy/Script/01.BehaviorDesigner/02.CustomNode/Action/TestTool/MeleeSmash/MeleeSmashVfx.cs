using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MeleeSmashVfx : Action
{
	[Header("WeaponObject")]
	[SerializeField] private GameObject vfx;
	[Header("FiredTransform")]
	public Transform meleeSmashPoint;
	
	public override TaskStatus OnUpdate()
	{
		Object.Instantiate(vfx, meleeSmashPoint.position , Quaternion.identity);
		return TaskStatus.Success;
	}
}