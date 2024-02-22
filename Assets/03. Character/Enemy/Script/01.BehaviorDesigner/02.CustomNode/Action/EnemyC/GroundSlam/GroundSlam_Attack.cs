using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GroundSlam_Attack : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedTransform behaviorObject;
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedGameObject UnityEventEnemy;

    [Header("AttackObject")]
    [SerializeField] private GameObject groundSlamObject;

    [Header("Movement")]
    [SerializeField] private float forwardForce;

    private Transform groundSlamPoint;
    private Rigidbody rb;
    private UnityEventEnemy_C unityEvent;
    EnemyAggroSystem enemyAggroSystem;

    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();
        groundSlamPoint = behaviorObject.Value.Find("GroundSlamPoint");

        InstantiateAttackObject();
        unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_C>();
        
        unityEvent.VFX_GroundSlam();
        unityEvent.VFX_GroundSlamStiff();

        enemyAggroSystem = GetComponent<EnemyAggroSystem>();
        enemyAggroSystem.StopReducingController(true);
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    private void InstantiateAttackObject()
    {
        GameObject slamCollider = Object.Instantiate(groundSlamObject, groundSlamPoint.position, groundSlamPoint.rotation);
    }

    private void Movement()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
    }

    public override void OnEnd()
    {
        enemyAggroSystem.StopReducingController(false);
    }
}