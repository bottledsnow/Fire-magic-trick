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
        Debug.Log("敵人"+idleActionType);
        Debug.Log("判斷"+targetAction);
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