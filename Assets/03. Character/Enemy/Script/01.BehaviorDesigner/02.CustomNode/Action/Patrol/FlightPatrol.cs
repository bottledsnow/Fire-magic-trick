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
         Movement();
      }
      return TaskStatus.Running;
   }

   private void Movement()
   {
      Vector3 movingTarget = enemyPatrolSystem.currentWaypoint.position;
      Vector3 direction = (movingTarget - transform.position).normalized;
      rigidbody.AddForce(direction * moveSpeed * Time.deltaTime);

      LookAtTarget(movingTarget);
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
}