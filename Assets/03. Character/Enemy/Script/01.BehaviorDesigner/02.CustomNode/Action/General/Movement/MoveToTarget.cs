using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class MoveToTarget : Action
{
   [Header("SharedVariable")]
   [SerializeField] private SharedGameObject targetObject;
   [SerializeField] private SharedGameObject modelObject;
   
   [Header("Movement")]
   [SerializeField] private float moveSpeed = 8500;

   [Header("Rotate")]
   [SerializeField] private float rotateSpeed = 150;

   [Header("ActEndDistance")]
   [SerializeField] private float actEndDistance  = 3;

   private Animator animator;
   Rigidbody rb;
   private NavMeshAgent agent;

   public override void OnStart()
   {
      rb = GetComponent<Rigidbody>();
      agent = GetComponent<NavMeshAgent>();
      if (modelObject != null)
      {
         animator = modelObject.Value.GetComponent<Animator>();
      }

      AnimationController(true);
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
      Vector3 direction = (targetObject.Value.transform.position - transform.position).normalized;
      agent.SetDestination(targetObject.Value.transform.position);
   }

   bool NearbyTarget()
   {
      if(Vector3.Distance(transform.position, targetObject.Value.transform.position) < actEndDistance)
      {
         return true;
      }
      return false;
   }

   private void AnimationController(bool isTrue)
   {
      if (animator != null)
      {
         animator.SetBool("isMove",isTrue);
      }
   }

   public override void OnEnd()
   {
      rb.velocity = Vector3.zero;
      AnimationController(false);   
   }
}