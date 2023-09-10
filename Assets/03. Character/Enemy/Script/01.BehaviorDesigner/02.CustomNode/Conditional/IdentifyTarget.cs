using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IdentifyTarget : Conditional
{
    public float radius;
    public float angle;
    public float maxDetect;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public SharedGameObject targetObject;

    private float detect;

    public override TaskStatus OnUpdate()
    {
        FindTarget();
        if (detect > 0)
        {
            return TaskStatus.Success;
        }
        else
        {
            targetObject.Value = null;
            return TaskStatus.Failure;
        }
    }

    void FindTarget()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2 && !Physics.Raycast(transform.position, directionToTarget, radius, obstructionMask))
            {
                targetObject.Value = rangeChecks[0].gameObject;
                detect = maxDetect;
            }
            else
            {
                detect--;
            }
        }
        else
        {
            detect--;
        }
    }
}