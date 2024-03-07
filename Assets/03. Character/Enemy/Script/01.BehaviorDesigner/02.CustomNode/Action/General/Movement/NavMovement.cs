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
      if(targetObject.Value != null && navMeshAgent != null)
      {
         Movement();
      }
      else
      {
         navMeshAgent.ResetPath();
         return TaskStatus.Failure;
      }
      if(NearbyTarget()) return TaskStatus.Success;
      return TaskStatus.Running;
   }

   private void Movement()
   {
        if(navMeshAgent.gameObject != null || navMeshAgent.gameObject.activeSelf !=false)
        {
            if(navMeshAgent != null || navMeshAgent.enabled != false)
            {
                if(navMeshAgent.isOnNavMesh)
                {
                    navMeshAgent.SetDestination(targetObject.Value.transform.position);
                }
            }
        }
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
        if (navMeshAgent.enabled)
        {
            if(navMeshAgent.gameObject != null || navMeshAgent.gameObject.activeSelf != false)
            {
                if(navMeshAgent.isOnNavMesh)
                {
                    navMeshAgent.ResetPath();
                }
            }
        }
        else
        {
            // 代理未處於活動狀態，可能需要進行初始化或啟用
        }
   }
}