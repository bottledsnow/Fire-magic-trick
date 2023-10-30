using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ShieldShoot_Aimming : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedGameObject shield;
    
    [Header("Aimming")]
    [SerializeField] private float aimmingDuaction = 2.5f;
    [SerializeField] private float rotateSpeed = 15;
    [SerializeField] private float AimmingLineDisableDelay = 0f;
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
        ShieldEnable();
    }

    public override TaskStatus OnUpdate()
    {
        LookAtTarget();
        if (Time.time - aimmingTimer <= aimmingDuaction - AimmingLineDisableDelay)
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

    private void LookAtTarget()
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