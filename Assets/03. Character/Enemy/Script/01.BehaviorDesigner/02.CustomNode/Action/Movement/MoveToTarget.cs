using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveToTarget : Action
{
   [Header("SharedVariable")]
   [SerializeField] private SharedGameObject targetObject;
   [Header("Movement")]
   [SerializeField] private float moveSpeed = 850;

   Rigidbody rb;

   public override void OnStart()
   {
      rb = GetComponent<Rigidbody>();
   }
   public override TaskStatus OnUpdate()
   {
      Movement();
      SpeedControl();
      return TaskStatus.Success;
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
}