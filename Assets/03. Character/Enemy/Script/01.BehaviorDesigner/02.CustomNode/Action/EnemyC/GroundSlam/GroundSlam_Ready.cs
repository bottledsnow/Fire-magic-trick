using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GroundSlam_Ready : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedGameObject UnityEventEnemy;


    [Header("Ready")]
    [SerializeField] private float readyDuaction = 2.5f;

    [Header("LookAtPlayer")]
    [SerializeField] private float rotateSpeed = 5;

    private float readyTimer;
    private Rigidbody rb;
    private UnityEventEnemy_C unityEvent;

    public override void OnStart()
    {
        readyTimer = Time.time;
        
        unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_C>();
        unityEvent.VFX_ReadyGroundSlam();
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - readyTimer <= readyDuaction)
        {
            LookAtTarget();
        }
        if (Time.time - readyTimer >= readyDuaction)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    private void LookAtTarget()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(targetObject.Value.transform.position.x, transform.position.y, targetObject.Value.transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }
}