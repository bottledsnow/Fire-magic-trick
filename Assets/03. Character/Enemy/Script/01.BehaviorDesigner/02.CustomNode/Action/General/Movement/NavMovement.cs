using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class NavMovement : Action
{
   [Header("SharedVariable")]
   [SerializeField] private SharedGameObject targetObject;
   [SerializeField] private SharedGameObject modelObject;
   
   [Header("Movement")]
   [SerializeField] private float moveSpeed = 9;

   [Header("ActEndDistance")]
   [SerializeField] private float actEndDistance  = 3;


   private Animator animator;
   NavMeshAgent navMeshAgent;

   public override void OnStart()
   {
      navMeshAgent = GetComponent<NavMeshAgent>();
      navMeshAgent.speed = moveSpeed;

      AnimationStart();
   }

   public override TaskStatus OnUpdate()
   {
      Movement();

      if(NearbyTarget())
      {
         return TaskStatus.Success;
      }
      return TaskStatus.Running;
   }

   private void Movement()
   {
      navMeshAgent.SetDestination(targetObject.Value.transform.position);
   }

   bool NearbyTarget()
   {
      if(Vector3.Distance(transform.position, targetObject.Value.transform.position) < actEndDistance)
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
   }
}