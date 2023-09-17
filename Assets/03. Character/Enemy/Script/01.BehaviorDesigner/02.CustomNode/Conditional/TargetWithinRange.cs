using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TargetWithinRange : Conditional
{
    public bool useMax = false;
    public float max;
    public bool useMin = false;
    public float min;
    public bool isVector2 = false;
    public SharedGameObject targetObject;

    [SerializeField]
    private float distanceToTarget;

    public override void OnStart()
    {
        if (isVector2)
        {
            distanceToTarget = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetObject.Value.transform.position.x, 0, targetObject.Value.transform.position.z));
        }
        else
        {
            distanceToTarget = Vector3.Distance(transform.position, targetObject.Value.transform.position);
        }

    }
    public override TaskStatus OnUpdate()
    {
        if (isWithinRange())
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }

    bool isWithinRange()
    {
        if (!useMax)
        {
            if (min <= distanceToTarget)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (!useMin)
        {
            if (distanceToTarget <= max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (min <= distanceToTarget && distanceToTarget <= max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}