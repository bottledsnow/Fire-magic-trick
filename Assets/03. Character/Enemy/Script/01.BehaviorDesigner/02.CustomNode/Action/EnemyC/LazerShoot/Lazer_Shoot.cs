using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.VFX;

public class LazerShoot : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedTransform behaviorObject;
    [SerializeField] private SharedGameObject UnityEventEnemy;

    private VisualEffect lazerVFX;
    private GameObject lazerCollider;
    private float vfxDuration;
    private float timer;
    private UnityEventEnemy_C unityEvent;

    public override void OnStart()
    {
        lazerVFX = behaviorObject.Value.Find("Lazer_VFX").GetComponent<VisualEffect>();
        lazerCollider = behaviorObject.Value.Find("Lazer_Collider").gameObject;
        vfxDuration = lazerVFX.GetFloat("Lifetime");
        timer = Time.time;

        if (lazerVFX != null && lazerCollider != null)
        {
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
}