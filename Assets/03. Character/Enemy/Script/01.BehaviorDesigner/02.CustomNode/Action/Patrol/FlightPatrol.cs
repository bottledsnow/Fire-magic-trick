using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class FlightPatrol : Action
{
   [Header("SharedVariable")]
   [SerializeField] private SharedGameObject modelObject;

   [Header("Movement")]
   [SerializeField] private float moveSpeed = 2500;
   [SerializeField] private float rotateSpeed = 2.5f;

   Animator animator;
   Rigidbody rigidbody;
   EnemyPatrolSystem enemyPatrolSystem;

   public override void OnStart()
   {
      enemyPatrolSystem = GetComponent<EnemyPatrolSystem>();
      enemyPatrolSystem.InitializationPatrol();

      rigidbody = GetComponent<Rigidbody>();

      AnimationStart();
   }

   public override TaskStatus OnUpdate()
   {
      if(enemyPatrolSystem != null && enemyPatrolSystem.currentWaypoint != null)
      {
         Patrol();
      }
      return TaskStatus.Running;
   }

   private void Patrol()
   {
      Vector3 movingTarget = enemyPatrolSystem.currentWaypoint.position;
      LookAtTarget(movingTarget);

      if(isLookingAtTarget(movingTarget))
      {
         MoveToNextWaypoint(movingTarget);
      }
   }

   private void MoveToNextWaypoint(Vector3 target)
   {
      // 朝向下一個路徑點移動
      Vector3 direction = (target - transform.position).normalized;
      rigidbody.AddForce(direction * moveSpeed * Time.deltaTime);
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

   private void LookAtTarget(Vector3 lookingTarget)
   {
      Quaternion rotation = Quaternion.LookRotation(new Vector3(lookingTarget.x, transform.position.y, lookingTarget.z) - transform.position);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
   }

   public override void OnEnd()
   {
      if(animator != null)
      {
         animator.SetBool("isMove",false);
      }
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
}