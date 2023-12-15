using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SideDodge : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedGameObject UnityEventEnemy;

    [Header("Rotate")]
    [SerializeField] private float rotateSpeed = 15;

    [Header("Dodge")]
    public float initialMoveSpeed = 40;
    public float slowdownRate = 0.5f;
    public float stopVelocity = 0.6f;
    private Rigidbody rb;
    private float timer;
    private UnityEventEnemy_B unityEvent;

    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();
        timer = Time.time;
        StartDodge();

        unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_B>();
        unityEvent.VFX_SideDodge();
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
        rb.velocity = Vector3.zero;

        int randomDirection = (Random.value > 0.5f) ? 1 : -1;
        rb.AddForce(transform.right * randomDirection * initialMoveSpeed, ForceMode.Impulse);
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