using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FlyToTarget : Action
{
   [Header("SharedVariable")]
   [SerializeField] private SharedGameObject targetObject;
   [SerializeField] private SharedGameObject UnityEventEnemy;

   [Header("Movement")]
   [SerializeField] private float moveSpeed = 850;
   [SerializeField] private float rotateSpeed = 2.5f;
   [SerializeField] private float keepHeight = 2.5f;
   [SerializeField] private float keepDistance = 12;

   private Rigidbody rb;
   private UnityEventEnemy_B unityEvent;

   public override void OnStart()
   {
      rb = GetComponent<Rigidbody>();

      unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_B>();
   }

   public override TaskStatus OnUpdate()
   {
      // 靠近目標
      if(!isNearKeepDistance(targetObject.Value.transform.position))
      {
         ApproachingTarget(targetObject.Value.transform.position);
         SpeedControl();
      }
      else
      {

      }
      Debug.Log(isNearKeepDistance(targetObject.Value.transform.position));
      
      // 看向目標
      LookAtTarget(targetObject.Value.transform.position);

      return TaskStatus.Success;
   }

   private void ApproachingTarget(Vector3 target)
   {
      // 移動方向
      Vector3 moveDirection = (movingTarget(target) - transform.position).normalized;
      rb.AddForce(moveDirection * moveSpeed * Time.deltaTime);
   }

   private void LookAtTarget(Vector3 lookingTarget)
   {
      Quaternion rotation = Quaternion.LookRotation(new Vector3(lookingTarget.x, transform.position.y, lookingTarget.z) - transform.position);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
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
   
   Vector3 movingTarget(Vector3 target)
   {
      // 計算移動保持距離後的目標點
      Vector3 horizantalSelf = new Vector3(transform.position.x , target.y , transform.position.z);
      Vector3 playerDirection = (horizantalSelf - target).normalized;
      Vector3 horizantalTarget = target + playerDirection * keepDistance;
      Vector3 movingTarget = horizantalTarget + new Vector3(0 , keepHeight , 0);

      return movingTarget;
   }

   bool isNearKeepDistance(Vector3 target)
   {
      // 是否靠近保持距離後的目標點
      float distanceToTarget = Vector3.Distance(movingTarget(target) , transform.position);
      if(distanceToTarget <= 1.5f) return true;
      return false;
   }
}