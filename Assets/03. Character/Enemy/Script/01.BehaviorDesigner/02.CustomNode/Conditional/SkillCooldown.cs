using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SkillCooldown : Conditional
{
	[Header("CooldownTime")]
	[SerializeField] private float cooldown;

	[HideInInspector]public float timer;

	public override TaskStatus OnUpdate()
	{
		if(Time.time - timer > cooldown)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}