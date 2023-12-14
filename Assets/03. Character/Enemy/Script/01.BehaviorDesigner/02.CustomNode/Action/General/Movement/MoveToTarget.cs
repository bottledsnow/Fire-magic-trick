using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

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

   public override void OnStart()
   {
      rb = GetComponent<Rigidbody>();

      AnimationStart();
   }

   public override TaskStatus OnUpdate()
   {
      Movement();
      RotateToTarget();
      SpeedLimit();

      if(NearbyTarget())
      {
         return TaskStatus.Success;
      }
      return TaskStatus.Running;
   }

   private void Movement()
   {
      Vector3 direction = (targetObject.Value.transform.position - transform.position).normalized;
      rb.AddForce(direction * moveSpeed * Time.deltaTime);
   }
   
   private void SpeedLimit()
   {
      Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

      if (flatVel.magnitude > moveSpeed * Time.deltaTime)
      {
         Vector3 limitedVel = flatVel.normalized * moveSpeed  * Time.deltaTime;
         rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
      }
   }

   private void RotateToTarget()
   {
      Vector3 targetPosition = new Vector3(targetObject.Value.transform.position.x, transform.position.y, targetObject.Value.transform.position.z);
      Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);

      float angle = Quaternion.Angle(transform.rotation, rotation);

      float maxRotationSpeed = rotateSpeed * Time.deltaTime;
      if (angle > maxRotationSpeed)
      {
         float t = maxRotationSpeed / angle;
         rotation = Quaternion.Slerp(transform.rotation, rotation, t);
      }
      transform.rotation = rotation;
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

      if(modelObject == null)
        {
            return;
        }
      if (modelObject != null)
      {
         animator = modelObject.Value.GetComponent<Animator>();
      }
      if (animator != null)
      {
         animator.SetBool("isMove",true);
      }
   }

   public override void OnEnd()
   {
      rb.velocity = Vector3.zero;

      if(animator != null)
      {
         animator.SetBool("isMove",false);
      }
   }
}