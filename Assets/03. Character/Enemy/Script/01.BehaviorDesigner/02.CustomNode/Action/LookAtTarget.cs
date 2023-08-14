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
        Quaternion rotation = Quaternion.LookRotation(new Vector3(targetTransform.Value.position.x, transform.position.y, targetTransform.Value.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        return TaskStatus.Success;
    }
}