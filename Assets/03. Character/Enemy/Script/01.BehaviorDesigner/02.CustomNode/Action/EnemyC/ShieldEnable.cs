using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ShieldEnable : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject shield;
    
    public override void OnStart()
    {
        if(shield != null)
        {
            shield.Value.SetActive(true);
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}