using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LookAtTarget : Action
{
    public float rotateSpeed = 200;
    public SharedGameObject targetObject;

    public override void OnStart()
    {

    }

    public override TaskStatus OnUpdate()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(targetObject.Value.transform.position.x, transform.position.y, targetObject.Value.transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        return TaskStatus.Success;
    }
}