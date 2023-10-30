using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Lazer_ShieldAimming : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedGameObject shield;
    
    [Header("Behavior")]
    [SerializeField] private float aimmingDuaction = 3.5f;
    [SerializeField] private float rotateSpeed = 15;
    [SerializeField] private float AimmingEndDelay = 0.1f;

    [Header("AimmingLine")]
    [SerializeField] private Transform aimmingLinePoint;
    [SerializeField] private float aimmingLineLength;

    [Header("Player")]
    [SerializeField] private float playerHeight = 0.3f;
    
    private Vector3 targetPoint;
    private float aimmingTimer;
    private LineRenderer lineRenderer;

    public override void OnStart()
    {
        aimmingTimer = Time.time;
        AimmingEnable();
        ShieldEnable();
    }

    public override TaskStatus OnUpdate()
    {
        LimitedRotation();
        if (Time.time - aimmingTimer <= aimmingDuaction - AimmingEndDelay)
        {
            AimmingLineRunning();
            ShieldEnable();
        }
        else
        {
            AimmingLineDisable();
            ShieldDisable();
        }
        if (Time.time - aimmingTimer >= aimmingDuaction)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    private void LimitedRotation()
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
    #region AimmingLine
    private void AimmingEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
    }
    private void AimmingLineRunning()
    {
        targetPoint = aimmingLinePoint.position + aimmingLinePoint.forward * aimmingLineLength;
        lineRenderer.SetPosition(0, aimmingLinePoint.position);
        lineRenderer.SetPosition(1, targetPoint);
    }
    private void AimmingLineDisable()
    {
        lineRenderer.enabled = false;
    }
    #endregion
    #region Shield
    private void ShieldEnable()
    {
        if(shield != null)
        {
            shield.Value.SetActive(true);
        }
    }
    private void ShieldDisable()
    {
        if(shield != null)
        {
            shield.Value.SetActive(false);
        }
    }
    #endregion
}