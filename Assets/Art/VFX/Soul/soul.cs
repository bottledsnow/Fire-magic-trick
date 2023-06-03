using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class soul : MonoBehaviour,VFX_Trigger
{
    [SerializeField] private Transform TargetPoint;
    [SerializeField] private ParticleSystem[] VFX_Soul;
    [SerializeField] float speed;

    private bool canfly;

    private void Start()
    {
        TargetPoint = GameObject.Find("SoulTarget").transform;
        triggerSoul();
    }
    private void FixedUpdate()
    {
        MoveToTarget();
    }
    private void MoveToTarget()
    {
        if(canfly)
        transform.position = Vector3.MoveTowards(transform.position, TargetPoint.position, speed);
    }
    private async void triggerSoul()
    {
        VFX_Soul[0].Play();
        await Task.Delay(2680);
        VFX_Soul[1].Play();
        canfly = true;
    }

    public void Trigger_VFX()
    {
        VFX_Soul[1].Stop();
        VFX_Soul[2].Play();
    }
}
