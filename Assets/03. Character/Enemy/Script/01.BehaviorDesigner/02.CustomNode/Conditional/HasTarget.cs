using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HasTarget : Conditional
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;

    [Header("Reverse")]
    [SerializeField] private bool reverse;

    public override TaskStatus OnUpdate()
    {
        if (!reverse)
        {
            if (targetObject.Value != null)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
        else
        {
            if (targetObject.Value != null)
            {
                return TaskStatus.Failure;
            }
            else
            {
                return TaskStatus.Success;
            }
        }

    }
}