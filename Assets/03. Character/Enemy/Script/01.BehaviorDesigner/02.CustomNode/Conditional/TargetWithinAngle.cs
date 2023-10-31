using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TargetWithinAngle : Conditional
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;

    [Header("Angle")]
    [SerializeField] private float max;

    private float distanceToTarget;

    public override TaskStatus OnUpdate()
    {
        if (isWithinAngle())
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }

    private bool isWithinAngle()
    {
        Vector3 toTarget = targetObject.Value.transform.position - transform.position;
        toTarget.Normalize();

        // 计算敌人正前方的方向向量
         Vector3 forwardDirection = transform.forward;

        // 使用向量夹角计算
        float angle = Vector3.Angle(forwardDirection, toTarget);

        if (angle <= max)
        {
            return true;
        }
        return false;
    }
}