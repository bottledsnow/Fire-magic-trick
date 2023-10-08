using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RangedShoot_Aimming : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [Header("Aimming")]
    [SerializeField] private float aimmingDuaction = 2.5f;
    [SerializeField] private float rotateSpeed = 5;
    [SerializeField] private Transform aimmingLinePoint;
    [Header("Player")]
    [SerializeField] private float playerHeight = 0.3f;
    
    private Vector3 targetPoint;
    private float aimmingTimer;
    private LineRenderer lineRenderer;

    public override void OnStart()
    {
        aimmingTimer = Time.time;
        AimmingEnable();
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - aimmingTimer <= aimmingDuaction - 0.5f)
        {
            _LookAtTarget();
            AimmingLineRunning();
        }
        else
        {
            AimmingLineDisable();
        }
        if (Time.time - aimmingTimer >= aimmingDuaction)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    private void _LookAtTarget()
    {
        Quaternion rotation = Quaternion.LookRotation(new Vector3(targetObject.Value.transform.position.x, transform.position.y, targetObject.Value.transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
    }
    #region AimmingLine
    private void AimmingEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
    }
    private void AimmingLineRunning()
    {
        targetPoint = new Vector3(targetObject.Value.transform.position.x, targetObject.Value.transform.position.y + playerHeight, targetObject.Value.transform.position.z);
        lineRenderer.SetPosition(0, aimmingLinePoint.position);
        lineRenderer.SetPosition(1, targetPoint);
    }
    private void AimmingLineDisable()
    {
        lineRenderer.enabled = false;
    }
    #endregion
}