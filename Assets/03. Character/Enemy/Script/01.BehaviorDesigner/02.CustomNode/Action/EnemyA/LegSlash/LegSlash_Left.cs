using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LegSlash_Left : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedTransform behaviorObject;
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedGameObject UnityEventEnemy;

    [Header("AttackObject")]
    [SerializeField] private GameObject legSlashObject;

    [Header("Movement")]
    [SerializeField] private float forwardForce;
    [SerializeField] private float maxRotateAngle = 30;

    [Header("Rotation")]
    [SerializeField] private float rotateSpeed;

    [Header("WaitTime")]
    [SerializeField] private float duration = 0.5f;

    private Transform legSlashPoint;
    private Rigidbody rb;
    private UnityEventEnemy_A unityEvent;
    EnemyAggroSystem enemyAggroSystem;
    private float timer;

    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();
        legSlashPoint = behaviorObject.Value.Find("LegSlashPoint");
        timer = Time.time;

        InstantiateAttackObject();
        Movement();
        
        unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_A>();
        unityEvent.VFX_LegSlash_B();

        enemyAggroSystem = GetComponent<EnemyAggroSystem>();
        enemyAggroSystem.StopReducingController(true);
    }

    public override TaskStatus OnUpdate()
    {
        Rotation();
    

        if(Time.time - timer > duration) return TaskStatus.Success;
        return TaskStatus.Running;
    }

    private void InstantiateAttackObject()
    {
        GameObject slashCollider = Object.Instantiate(legSlashObject, legSlashPoint.position, legSlashPoint.rotation);
        slashCollider.transform.parent = transform;
    }

    private void Movement()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
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

    public override void OnEnd()
    {
        enemyAggroSystem.StopReducingController(false);
    }
}