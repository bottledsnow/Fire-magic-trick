using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.VFX;

public class LazerShoot : Action
{
    [Header("VFX")]
    [SerializeField] private VisualEffect lazerVfx;

    [Header("Collider")]
    [SerializeField] private GameObject lazerCollider;

    private float vfxDuration;
    private float timer;

    public override void OnStart()
    {
        timer = Time.time;
        vfxDuration = lazerVfx.GetFloat("Lifetime");

        if (lazerVfx != null && lazerCollider != null)
        {
            lazerVfx.Play();
            lazerCollider.SetActive(true);
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - timer > vfxDuration)
        {
            lazerCollider.SetActive(false);
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}