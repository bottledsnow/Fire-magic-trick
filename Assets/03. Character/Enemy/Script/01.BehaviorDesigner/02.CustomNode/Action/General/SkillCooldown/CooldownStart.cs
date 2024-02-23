using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CooldownStart : Action
{
	[Header("SkillCooldownTask")]
    [SerializeField] private SkillCooldown skillCooldown;

	public override TaskStatus OnUpdate()
	{
		skillCooldown.timer = Time.time;

		return TaskStatus.Success;
	}
}