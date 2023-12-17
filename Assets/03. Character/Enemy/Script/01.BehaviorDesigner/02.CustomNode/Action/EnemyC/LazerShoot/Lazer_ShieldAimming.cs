using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;

public class Lazer_ShieldAimming : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedTransform behaviorObject;
    [SerializeField] private SharedGameObject UnityEventEnemy;
    
    [Header("Behavior")]
    [SerializeField] private float aimmingDuaction = 3.5f;
    [SerializeField] private float rotateSpeed = 15;
    [SerializeField] private float AimmingEndDelay = 0.1f;

    [Header("AimmingLine")]
    [SerializeField] private float maxLength;
    [SerializeField] private LayerMask obstacleLayer;

    [Header("Player")]
    [SerializeField] private float playerHeight = 0.3f;
    
    private Transform aimmingLinePoint;
    private GameObject shield;
    private Vector3 targetPoint;
    private float aimmingTimer;
    private LineRenderer lineRenderer;
    private UnityEventEnemy_C unityEvent;
    private bool triggerEnd;
    public override void OnStart()
    {
        aimmingLinePoint = behaviorObject.Value.Find("AimmingLinePoint");
        shield = behaviorObject.Value.Find("Shield").gameObject;
        aimmingTimer = Time.time;
        AimmingEnable();
        ShieldEnable();

        unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_C>();
        unityEvent.VFX_AimStart();
    }

    public override TaskStatus OnUpdate()
    {
        LimitedRotation();
        if (Time.time - aimmingTimer <= aimmingDuaction - AimmingEndDelay)
        {
            unityEvent.VFX_AimKeep();
            AimmingLineRunning();
            ShieldEnable();
            triggerEnd = false;
        }
        else
        {
            if(!triggerEnd)
            {
                unityEvent.VFX_AimEnd();
                triggerEnd = true;
            }
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
        lineRenderer.SetPosition(0, aimmingLinePoint.position);

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxLength , obstacleLayer))
        {
            Vector3 hitPoint = hit.point;
            lineRenderer.SetPosition(1, hitPoint);
        }
        else
        {
            targetPoint = aimmingLinePoint.position + aimmingLinePoint.forward * maxLength;
            lineRenderer.SetPosition(1, targetPoint);
        }
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
            shield.SetActive(true);
        }
    }
    private void ShieldDisable()
    {
        if(shield != null)
        {
            shield.SetActive(false);
        }
    }
    #endregion
}