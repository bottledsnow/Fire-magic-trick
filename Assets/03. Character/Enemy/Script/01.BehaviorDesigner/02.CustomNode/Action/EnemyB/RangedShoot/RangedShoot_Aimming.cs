using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RangedShoot_Aimming : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedTransform behaviorObject;
    [SerializeField] private SharedGameObject UnityEventEnemy;
    
    [Header("Aimming")]
    [SerializeField] private float aimmingDuaction = 2.5f;
    [SerializeField] private float rotateSpeed = 15;
    [SerializeField] private float AimmingLineDisableDelay = 0.25f;

    [Header("Player")]
    [SerializeField] private float playerHeight = 0.3f;
    
    private Transform aimmingLinePoint;
    private Vector3 targetPoint;
    private float aimmingTimer;
    private LineRenderer lineRenderer;
    private UnityEventEnemy_B unityEvent;

    public override void OnStart()
    {
        aimmingLinePoint = behaviorObject.Value.Find("AimmingLinePoint");
        aimmingTimer = Time.time;
        AimmingEnable();
        
        unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_B>();
        unityEvent.VFX_AimingStart();
    }

    public override TaskStatus OnUpdate()
    {
        if(targetObject.Value == null)
        {
            return TaskStatus.Failure;
        }
        LookAtTarget();
        if (Time.time - aimmingTimer <= aimmingDuaction - AimmingLineDisableDelay)
        {
            AimmingLineRunning();
        }
        else
        {
            AimmingLineDisable();
            unityEvent.VFX_AimingFinishReady();
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

    public override void OnEnd()
    {
        AimmingLineDisable();
    }
}