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
   [SerializeField] private float keepHeight = 2.5f;
   [SerializeField] private float rotateSpeed = 2.5f;

   private Rigidbody rb;
   private UnityEventEnemy_B unityEvent;

   public override void OnStart()
   {
      rb = GetComponent<Rigidbody>();

      unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_B>();
   }

   public override TaskStatus OnUpdate()
   {
      Movement();
      SpeedControl();
      return TaskStatus.Success;
   }

   private void Movement()
   {
      Vector3 movingTarget = new Vector3(targetObject.Value.transform.position.x, targetObject.Value.transform.position.y + keepHeight,targetObject.Value.transform.position.z);
      Vector3 direction = (movingTarget - transform.position).normalized;
      rb.AddForce(direction * moveSpeed * Time.deltaTime);

      LookAtTarget(movingTarget);
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
}