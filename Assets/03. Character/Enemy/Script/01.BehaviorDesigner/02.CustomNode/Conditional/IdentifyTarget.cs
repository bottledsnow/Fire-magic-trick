using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IdentifyTarget : Conditional
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;

    [Header("DetectArea")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;
    
    [Header("Alert")]
    [SerializeField] private float maxAlert = 100;

    private float alert;

    public override TaskStatus OnUpdate()
    {
        FieldOfView();
        if (alert > 0)
        {
            return TaskStatus.Success;
        }
        else
        {
            targetObject.Value = null;
            return TaskStatus.Failure;
        }
    }

    private void FieldOfView()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2 && !Physics.Raycast(transform.position, directionToTarget, radius, obstructionMask))
            {
                targetObject.Value = rangeChecks[0].gameObject;
                alert = maxAlert;
            }
            else
            {
                alert--;
            }
        }
        else
        {
            alert--;
        }
    }
}