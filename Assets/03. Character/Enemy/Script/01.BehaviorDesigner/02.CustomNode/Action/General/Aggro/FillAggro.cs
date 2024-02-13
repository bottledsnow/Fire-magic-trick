using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FillAggro : Action
{
   EnemyAggroSystem enemyAggroSystem;

   public override void OnStart()
   {
      enemyAggroSystem = GetComponent<EnemyAggroSystem>();
   }

   public override TaskStatus OnUpdate()
   {
        GameObject player = GameObject.Find("Player");
        if (enemyAggroSystem != null)
        {
            enemyAggroSystem.SetAggroTarget(player);
            enemyAggroSystem.CallNearbyEnemy(player);
        }
        return TaskStatus.Success;
    }
}