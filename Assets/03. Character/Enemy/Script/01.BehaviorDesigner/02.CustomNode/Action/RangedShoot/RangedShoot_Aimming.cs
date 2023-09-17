using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RangedShoot_Aimming : Action
{
    public float aimmingDuaction = 2.5f;
    public float rotateSpeed = 5;
    public Transform aimmingLinePoint;
    public float playerHeight = 1.3f;
    public SharedGameObject targetObject;
    private Vector3 targetPoint;
    private float timer;
    private LineRenderer lineRenderer;

    public override void OnStart()
    {
        timer = Time.time;
        AimmingEnable();
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - timer <= aimmingDuaction - 0.5f)
        {
            _LookAtTarget();
            AimmingLineRunning();
        }
        if (Time.time - timer >= aimmingDuaction)
        {
            AimmingLineDisable();
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    void _LookAtTarget()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(targetObject.Value.transform.position.x, transform.position.y, targetObject.Value.transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }

    void AimmingEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
    }
    void AimmingLineRunning()
    {
        targetPoint = new Vector3(targetObject.Value.transform.position.x, targetObject.Value.transform.position.y + playerHeight / 2, targetObject.Value.transform.position.z);
        lineRenderer.SetPosition(0, aimmingLinePoint.position);
        lineRenderer.SetPosition(1, targetPoint);
    }
    void AimmingLineDisable()
    {
        lineRenderer.enabled = false;
    }
}