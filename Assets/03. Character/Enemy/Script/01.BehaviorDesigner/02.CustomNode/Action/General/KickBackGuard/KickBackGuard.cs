using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class KickBackGuard : Action
{
	[Header("ActiveSetting")]
    [SerializeField] private bool isActive;

	private EnemyHealthSystem enemyHealthSystem;

	public override void OnStart()
	{
		enemyHealthSystem = GetComponent<EnemyHealthSystem>();
	}

	public override TaskStatus OnUpdate()
	{
		enemyHealthSystem.kickBackGuard = isActive;
		return TaskStatus.Success;
	}
}