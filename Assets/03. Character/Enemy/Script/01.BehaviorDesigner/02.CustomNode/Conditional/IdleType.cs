using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IdleType : Conditional
{
    [Header("Action")]
    [SerializeField] private EnemyAggroSystem.IdleActionType targetAction;

    EnemyAggroSystem.IdleActionType idleActionType;

    public override TaskStatus OnUpdate()
    {
        idleActionType = GetComponent<EnemyAggroSystem>().idleActionType;
        if (idleActionType == targetAction)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}