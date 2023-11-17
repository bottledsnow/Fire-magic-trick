using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CalculateSurroundPosition : Action
{
    public SharedGameObject targetObject;
    public SharedGameObjectList teammateEnemies;
    public SharedInt surroundingNumber;
    public SharedBool isSurrounding;
    public float minDistanceBetweenEnemies;
    public float circleRadius;

    public override TaskStatus OnUpdate()
    {
        if (!isSurrounding.Value)
        {
            surroundingNumber.Value = 0;
            isSurrounding.Value = true;

            for (int i = 0; i < teammateEnemies.Value.Count; i++)
            {
                teammateEnemies.Value[i].GetComponent<BehaviorTree>().SetVariableValue("surroundingNumber", i + 1);
                teammateEnemies.Value[i].GetComponent<BehaviorTree>().SetVariableValue("isSurrounding", true);
            }
        }

        return TaskStatus.Success;
    }
}
