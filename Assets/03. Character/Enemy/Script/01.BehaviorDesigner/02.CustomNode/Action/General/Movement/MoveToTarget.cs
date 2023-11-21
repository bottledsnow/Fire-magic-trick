using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveToTarget : Action
{
   [Header("SharedVariable")]
   [SerializeField] private SharedGameObject targetObject;
   [SerializeField] private SharedGameObject modelObject;
   
   [Header("Movement")]
   [SerializeField] private float moveSpeed = 850;

   [Header("Rotate")]
   [SerializeField] private float rotateSpeed = 200;

   [Header("Animator")]
   [SerializeField] private Animator animator;

   Rigidbody rb;

   public override void OnStart()
   {
      rb = GetComponent<Rigidbody>();
      animator = modelObject.Value.GetComponent<Animator>();

      if(animator != null)
      {
         animator.SetBool("isMove",true);
      }
   }
   public override TaskStatus OnUpdate()
   {
      Movement();
      LookAtTarget();
      SpeedControl();
      return TaskStatus.Running;
   }
   private void Movement()
   {
      Vector3 direction = (targetObject.Value.transform.position - transform.position).normalized;
      rb.AddForce(direction * moveSpeed * Time.deltaTime);
   }
   private void SpeedControl()
   {
      Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

      if (flatVel.magnitude > moveSpeed * Time.deltaTime)
      {
         Vector3 limitedVel = flatVel.normalized * moveSpeed  * Time.deltaTime;
         rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
      }
   }

   private void LookAtTarget()
   {
      Quaternion rotation = Quaternion.LookRotation(new Vector3(targetObject.Value.transform.position.x, transform.position.y, targetObject.Value.transform.position.z) - transform.position);
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