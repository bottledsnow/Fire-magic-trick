using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SideDodge : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    
    [Header("Rotate")]
    [SerializeField] private float rotateSpeed = 200;

    [Header("Dodge")]
    public float initialMoveSpeed = 10.0f;
    public float slowdownRate = 0.5f; 
    public float stopVelocity = 0.6f;
    private Rigidbody rb;
    private float timer;
    
    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();
        timer = Time.time;
        StartDodge();
    }

    public override TaskStatus OnUpdate()
    {
        LookAtTarget();
        SlowdownByTime();

        if (Time.time - timer > 0.2f && rb.velocity.magnitude < stopVelocity)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    private void StartDodge()
    {
        int dodgeDirection = (Random.value > 0.5f) ? 1 : -1;
        rb.AddForce(transform.right * dodgeDirection * initialMoveSpeed * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void LookAtTarget()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(targetObject.Value.transform.position.x, transform.position.y, targetObject.Value.transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }

    private void SlowdownByTime()
    {
        rb.velocity *= 1.0f - slowdownRate * Time.deltaTime;
    }
}