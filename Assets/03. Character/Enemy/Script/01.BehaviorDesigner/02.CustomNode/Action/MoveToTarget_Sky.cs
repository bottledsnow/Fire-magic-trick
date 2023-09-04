using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MoveToTarget_Sky : Action
{
   public float moveSpeed = 850;
   public Vector3 movingTarget;
   public SharedGameObject targetObject;

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
   void PositionFind()
   {
      Vector3 movingTarget = new Vector3(targetObject.Value.transform.position.x, targetObject.Value.transform.position.y+3,targetObject.Value.transform.position.z);
   }
   void Movement()
   {
      Vector3 direction = (movingTarget - transform.position).normalized;
      rb.AddForce(direction * moveSpeed * Time.deltaTime);
   }
   void SpeedControl()
   {
      Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

      if (flatVel.magnitude > moveSpeed * Time.deltaTime)
      {
         Vector3 limitedVel = flatVel.normalized * moveSpeed  * Time.deltaTime;
         rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
      }
   }
}