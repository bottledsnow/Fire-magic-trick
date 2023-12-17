using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.VFX;

public class Lazer_Shoot : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedTransform behaviorObject;
    [SerializeField] private SharedGameObject UnityEventEnemy;

    [Header("Lazer")]
    [SerializeField] private float maxLength = 35;
    [SerializeField] private LayerMask obstacleLayer;

    private VisualEffect lazerVFX;
    private GameObject lazerCollider;
    private Transform aimmingLinePoint;
    private float vfxDuration;
    private float timer;
    private UnityEventEnemy_C unityEvent;

    public override void OnStart()
    {
        aimmingLinePoint = behaviorObject.Value.Find("AimmingLinePoint");
        lazerVFX = behaviorObject.Value.Find("Lazer_VFX").GetComponent<VisualEffect>();
        lazerCollider = behaviorObject.Value.Find("Lazer_Collider").gameObject;
        vfxDuration = lazerVFX.GetFloat("Lifetime");
        timer = Time.time;

        if (lazerVFX != null && lazerCollider != null)
        {
            LazerRangeSetting();
            lazerVFX.Play();
            lazerCollider.SetActive(true);
        }

        unityEvent = UnityEventEnemy.Value.GetComponent<UnityEventEnemy_C>();
        unityEvent.VFX_Lazer();
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - timer > vfxDuration)
        {
            lazerCollider.SetActive(false);
            unityEvent.VFX_LazerStiff();
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    void LazerRangeSetting()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxLength, obstacleLayer))
        {
            Vector3 hitPoint = hit.point;
            float distanceToPoint = Vector3.Distance(aimmingLinePoint.position, hitPoint);

            lazerVFX.SetFloat("Length", distanceToPoint);
            lazerCollider.transform.position = aimmingLinePoint.position + aimmingLinePoint.forward * distanceToPoint / 2;
            lazerCollider.transform.localScale = new Vector3(lazerCollider.transform.localScale.x, distanceToPoint, lazerCollider.transform.localScale.z);
        }
    }
}