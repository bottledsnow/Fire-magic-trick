using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class FindTarget : Conditional
{
    public float radius;
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public SharedFloat distanceToTarget;
    public SharedTransform targetTransform;

	public override void OnStart()
	{
		
	}

	public override TaskStatus OnUpdate()
	{
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                distanceToTarget.Value = Vector3.Distance(transform.position,target.transform.position);
                targetTransform.Value = rangeChecks[0].transform;

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget.Value, obstructionMask))
                {
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Failure;
                }
            }
            else
                return TaskStatus.Failure;
        }
        else
            return TaskStatus.Failure;
    }
}