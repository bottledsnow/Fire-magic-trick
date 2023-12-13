using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RadiusMovement : Action
{
   public SharedGameObject targetObject;
   public float radius;

   private float randomAngle;

	private Rigidbody rb;
	public  float moveSpeed = 8500;	
	private float rotateSpeed = 150;	


   public override void OnStart()
   {
      randomAngle = Random.Range(-90f, 90f);
      Debug.Log(randomAngle);
   	rb = GetComponent<Rigidbody>();
   }

   public override TaskStatus OnUpdate()
   {
		Movement(targetPosition());
      RotateToTarget(targetObject.Value.transform.position);
      SpeedLimit();

      return TaskStatus.Success;
   }

   Vector3 targetPosition()
   {
      Vector3 playerDirection = targetObject.Value.transform.position - transform.position;
      playerDirection.y = 0;

      // 计算目标角度
      float targetAngle = Mathf.Atan2(playerDirection.z, playerDirection.x) * Mathf.Rad2Deg;

      targetAngle += randomAngle;

      // 将角度限制在 -180 到 180 之间
      //targetAngle = Mathf.Clamp(targetAngle, -180f, 180f);

      // 转换为弧度
      float radians = targetAngle * Mathf.Deg2Rad;

      // 计算目标位置
      Vector3 target = targetObject.Value.transform.position + new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * radius;

      return target;
   }

   private void Movement(Vector3 targetPosition)
   {
      Vector3 direction = (targetPosition - transform.position).normalized;
	   direction.y = 0;
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

   private void RotateToTarget(Vector3 targetPosition)
   {
      Vector3 targetDirection = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
      Quaternion rotation = Quaternion.LookRotation(targetDirection - transform.position);

      float angle = Quaternion.Angle(transform.rotation, rotation);

      float maxRotationSpeed = rotateSpeed * Time.deltaTime;
      if (angle > maxRotationSpeed)
      {
         float t = maxRotationSpeed / angle;
         rotation = Quaternion.Slerp(transform.rotation, rotation, t);
      }
      transform.rotation = rotation;
   }

   public override void OnEnd()
   {
      rb.velocity = Vector3.zero;
   }
}