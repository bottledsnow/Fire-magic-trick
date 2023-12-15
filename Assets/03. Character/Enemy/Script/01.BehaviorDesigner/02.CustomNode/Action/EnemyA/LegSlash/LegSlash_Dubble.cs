using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LegSlash_Dubble : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedTransform behaviorObject;
    [SerializeField] private SharedGameObject targetObject;

    [Header("AttackObject")]
    [SerializeField] private GameObject legSlashObject_L;
    [SerializeField] private GameObject legSlashObject_R;

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
        unityEvent.VFX_LegSlash_C();
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