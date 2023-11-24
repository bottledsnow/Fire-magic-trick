using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class LookAtTarget : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;

    [Header("Rotate")]
    [SerializeField] private float rotateSpeed = 150;
    

    public override void OnStart()
    {

    }

    public override TaskStatus OnUpdate()
    {
        RotateToTarget();
        return TaskStatus.Success;
    }
    
    private void RotateToTarget()
    {
        Vector3 targetPosition = new Vector3(targetObject.Value.transform.position.x, transform.position.y, targetObject.Value.transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);

        float angle = Quaternion.Angle(transform.rotation, rotation);

        float maxRotationSpeed = rotateSpeed * Time.deltaTime;
        if (angle > maxRotationSpeed)
        {
            float t = maxRotationSpeed / angle;
            rotation = Quaternion.Slerp(transform.rotation, rotation, t);
        }
        transform.rotation = rotation;
    }
}