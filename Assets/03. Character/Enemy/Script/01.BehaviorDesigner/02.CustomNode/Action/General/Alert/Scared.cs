using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Scared : Action
{
   [Header("JumpForce")]
   [SerializeField] private float upForce;

   [Header("GroundCheck")]
   [SerializeField] private float raycastDistance = 0.3f;

   private Rigidbody rb;
   private float timer;

   public override void OnStart()
   {
      rb = GetComponent<Rigidbody>();
      timer = Time.time;
      rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
   }

   public override TaskStatus OnUpdate()
   {
      if (Time.time - timer > 0.2f && isGrounded())
        {
            return TaskStatus.Success;
        }
      return TaskStatus.Running;
   }

   bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}