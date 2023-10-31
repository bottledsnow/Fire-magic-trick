using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ReadjustAngle : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [Header("Rotate")]
    [SerializeField] private float rotateSpeed = 50;
    [SerializeField] private float minAngleToSlowdown = 25f;
    [SerializeField] private float minRotationSpeed = 20f;


    public override void OnStart()
    {

    }

    public override TaskStatus OnUpdate()
    {
        // 计算目标相对于敌人的方向向量
        Vector3 toTarget = targetObject.Value.transform.position - transform.position;
        toTarget.Normalize();

        // 计算敌人正前方的方向向量
        Vector3 forwardDirection = transform.forward;

        // 使用向量夹角计算
        float angle = Vector3.Angle(forwardDirection, toTarget);

        if (angle > minAngleToSlowdown)
        {
            rotate();
        }
        else
        {
            // 如果角度低于某一值，开始减速
            float t = Mathf.InverseLerp(minAngleToSlowdown, 0, angle);
            float currentRotationSpeed = Mathf.Lerp(rotateSpeed, minRotationSpeed, t);

            Quaternion rotation = Quaternion.LookRotation(toTarget);
            float maxRotationSpeed = currentRotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, maxRotationSpeed);
        }

        return TaskStatus.Success;
    }

    void rotate()
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
    }
}