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
      enemyAggroSystem.SetAggroTarget(GameObject.Find("Player"));
      return TaskStatus.Success;
   }
}