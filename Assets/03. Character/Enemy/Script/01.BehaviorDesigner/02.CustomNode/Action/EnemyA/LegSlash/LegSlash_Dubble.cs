using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LegSlash_Dubble : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedTransform behaviorObject;
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedGameObject UnityEventEnemy;


    [Header("AttackObject")]
    [SerializeField] private GameObject legSlashObject_L;
    [SerializeField] private GameObject legSlashObject_R;

    [Header("Movement")]
    [SerializeField] private float forwardForce;
    [SerializeField] private float maxRotateAngle = 30;
    
    [Header("Rotation")]
    [SerializeField] private float rotateSpeed;

    private Transform legSlashPoint;
    private Rigidbody rb;
    private UnityEventEnemy_A unityEvent;
    EnemyAggroSystem enemyAggroSystem;

    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();
        legSlashPoint = behaviorObject.Value.Find("LegSlashPoint");

        InstantiateAttackObject();
        Rotation();
        Movement();

        unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_A>();
        unityEvent.VFX_LegSlash_C();

        enemyAggroSystem = GetComponent<EnemyAggroSystem>();
        enemyAggroSystem.StopReducingController(true);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    private void InstantiateAttackObject()
    {
        GameObject slashColliderL = Object.Instantiate(legSlashObject_L, legSlashPoint.position, legSlashPoint.rotation);
        GameObject slashColliderR = Object.Instantiate(legSlashObject_R, legSlashPoint.position, legSlashPoint.rotation);
        slashColliderL.transform.parent = transform;
        slashColliderR.transform.parent = transform;
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