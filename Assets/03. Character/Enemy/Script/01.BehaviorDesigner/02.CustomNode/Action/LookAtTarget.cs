using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LookAtTarget : Action
{
    public float rotateSpeed = 200;
    public SharedTransform targetTransform;

    public override void OnStart()
    {

    }

    public override TaskStatus OnUpdate()
    {
        transform.LookAt(new Vector3(targetTransform.Value.position.x, transform.position.y, targetTransform.Value.position.z));
        return TaskStatus.Success;
    }
}