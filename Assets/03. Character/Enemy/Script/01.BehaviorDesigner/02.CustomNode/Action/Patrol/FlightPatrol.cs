using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class FlightPatrol : Action
{
   [Header("SharedVariable")]
   [SerializeField] private SharedGameObject modelObject;
   
   [Header("Movement")]
   [SerializeField] private float moveSpeed = 6;

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
      if(enemyPatrolSystem != null)
      {
         Movement();
      }
      return TaskStatus.Running;
   }

   private void Movement()
   {
      Vector3 movingTarget = enemyPatrolSystem.currentWaypoint.position;
      Vector3 direction = (movingTarget - transform.position).normalized;
      Debug.Log(direction);
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

   public override void OnEnd()
   {
      if(animator != null)
      {
         animator.SetBool("isMove",false);
      }
   }
}