using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LegSlash_Right : Action
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

    [Header("Time")]
    [SerializeField] private float duration = 0.5f;

    private Transform legSlashPoint;
    private Rigidbody rb;
    private UnityEventEnemy_A unityEvent;

    public AnimationCurve rotationCurve = AnimationCurve.Linear(0f, 0f, 1f, 360f);

    private float rotateTimer;
    private float timer;

    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();
        legSlashPoint = behaviorObject.Value.Find("LegSlashPoint");
        timer = Time.time;

        InstantiateAttackObject();
        Rotation();
        Movement();
        unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_A>();
        unityEvent.VFX_LegSlash_A();
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
        float normalizedTime = Mathf.Clamp01((Time.time - timer) / duration);

        float targetRotationValue = rotationCurve.Evaluate(normalizedTime);

        Quaternion targetRotation = Quaternion.Euler(0f, targetRotationValue * 3, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }
}