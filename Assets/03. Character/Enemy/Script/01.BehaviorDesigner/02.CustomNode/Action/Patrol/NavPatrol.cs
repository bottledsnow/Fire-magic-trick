using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class NavPatrol : Action
{
   [Header("SharedVariable")]
   [SerializeField] private SharedGameObject modelObject;
   
   [Header("Movement")]
   [SerializeField] private float moveSpeed = 6;

   private Animator animator;
   NavMeshAgent navMeshAgent;
   EnemyPatrolSystem enemyPatrolSystem;

   public override void OnStart()   
   {
      navMeshAgent = GetComponent<NavMeshAgent>();
      navMeshAgent.speed = moveSpeed;

      enemyPatrolSystem = GetComponent<EnemyPatrolSystem>();
      enemyPatrolSystem.InitializationPatrol();

      AnimationStart();
   }

   public override TaskStatus OnUpdate()
   {
      if(navMeshAgent != null && enemyPatrolSystem != null)
      {
         Movement();
      }
      return TaskStatus.Running;
   }

   private void Movement()
   {
      navMeshAgent.SetDestination(enemyPatrolSystem.currentWaypoint.position);
   }

   private void AnimationStart()
   {
      if (modelObject.Value == null)
      {
         return;
      }
      if (modelObject.Value != null)
      {
         animator = modelObject.Value.GetComponent<Animator>();
      }
      if (animator != null)
      {
         animator.SetBool("isMove", true);
      }
   }

   public override void OnEnd()
   {
      if(animator != null)
      {
         animator.SetBool("isMove",false);
      }
      navMeshAgent.ResetPath();
   }
}