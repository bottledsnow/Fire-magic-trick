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
   [SerializeField] private float rotateSpeed = 2.5f;

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
      if(navMeshAgent != null && enemyPatrolSystem != null && enemyPatrolSystem.currentWaypoint != null)
      {
         Patrol();
      }
      return TaskStatus.Running;
   }

   private void Patrol()
   {
      Vector3 movingTarget = enemyPatrolSystem.currentWaypoint.position;
      LookAtTarget(movingTarget);

      // 面向後再移動
      if(isLookingAtTarget(movingTarget))
      {
         navMeshAgent.SetDestination(movingTarget);
      }
      else
      {
         navMeshAgent.ResetPath();
      }
   }

   private void LookAtTarget(Vector3 lookingTarget)
   {
      Quaternion rotation = Quaternion.LookRotation(new Vector3(lookingTarget.x, transform.position.y, lookingTarget.z) - transform.position);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
   }

   bool isLookingAtTarget(Vector3 target)
   {
      // 忽略Y軸
      Vector3 selfPosition = new Vector3(transform.position.x, 0f, transform.position.z);
      Vector3 targetPosition = new Vector3(target.x, 0f, target.z);

      // 標準化後判斷角度
      Vector3 directionToTarget = (targetPosition - selfPosition).normalized;
      if (Vector3.Angle(transform.forward, directionToTarget) < 2.5f)
      {
         return true;
      }
      return false;
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
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.ResetPath();
        }
        else
        {
            // 代理未被放置在導航網格上，可能需要進行初始化或放置
        }
    }
}