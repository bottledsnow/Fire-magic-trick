using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ShieldEnable : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedTransform behaviorObject;
    
    public override void OnStart()
    {
        GameObject shield = behaviorObject.Value.Find("Shield").gameObject;

        if(shield != null)
        {
            shield.SetActive(true);
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }
}