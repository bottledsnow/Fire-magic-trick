using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.Events;

public class LegSlash_Ready : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedGameObject UnityEventEnemy;


    [Header("Ready")]
    [SerializeField] private float readyDuaction = 2.5f;

    [Header("LookAtPlayer")]
    [SerializeField] private float rotateSpeed = 70;

    private float readyTimer;
    private Rigidbody rb;
    private UnityEventEnemy_A unityEvent;

    public override void OnStart()
    {
        readyTimer = Time.time;
        unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_A>();
        unityEvent.VFX_LegSlashStart();
    }

    public override TaskStatus OnUpdate()
    {
        // if (targetObject.Value == null)
        // {
        //     return TaskStatus.Failure;
        // }
        if (Time.time - readyTimer <= readyDuaction)
        {
            Rotation();
        }
        if (Time.time - readyTimer >= readyDuaction)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    private void Rotation()
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
}