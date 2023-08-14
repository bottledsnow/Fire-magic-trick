using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MeleeSmashVfx : Action
{
	public GameObject vfx;
	public Transform meleeSmashPoint;
	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
		Object.Instantiate(vfx, meleeSmashPoint.position , Quaternion.identity);
		return TaskStatus.Success;
	}
}