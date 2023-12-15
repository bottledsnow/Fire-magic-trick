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

    private Transform legSlashPoint;
    private Rigidbody rb;
    private UnityEventEnemy_A unityEvent;

    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();
        legSlashPoint = behaviorObject.Value.Find("LegSlashPoint");

        InstantiateAttackObject();
        Rotation();
        Movement();
        
        unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_A>();
        unityEvent.VFX_LegSlash_B();
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
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
        // 获取敌人到玩家的方向
        Vector3 directionToPlayer = targetObject.Value.transform.position - transform.position;
        directionToPlayer.y = 0;
        // 使用Quaternion.LookRotation直接设置敌人的朝向
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        // 限制转动角度不超过15度
        float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);
        if (angleDifference <= maxRotateAngle)
        {
            // 如果当前角度差在限定范围内，直接设置朝向
            transform.rotation = targetRotation;
        }
        else
        {
            // 如果超过限定范围，根据最大角度进行插值
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, maxRotateAngle / angleDifference);
        }
    }
}