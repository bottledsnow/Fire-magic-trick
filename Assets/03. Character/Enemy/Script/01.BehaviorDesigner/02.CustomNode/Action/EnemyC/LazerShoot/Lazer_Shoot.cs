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
        // 雷射特效結束時
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
        // 射線偵測打中障礙物時
        Ray ray = new Ray(aimmingLinePoint.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxLength, obstacleLayer))
        {
            Debug.Log(hit.collider.gameObject);
            // 計算擊中障礙物的點與發射位置距離
            Vector3 hitPoint = hit.point;
            float distanceToPoint = Vector3.Distance(aimmingLinePoint.position, hitPoint);

            // 調整特效長度
            lazerVFX.SetFloat("Length", distanceToPoint);
            // 調整碰撞體長度
            lazerCollider.transform.position = aimmingLinePoint.position + aimmingLinePoint.forward * distanceToPoint / 2;
            lazerCollider.transform.localScale = new Vector3(lazerCollider.transform.localScale.x, distanceToPoint, lazerCollider.transform.localScale.z);
        }
    }
}