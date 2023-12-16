using UnityEngine;
using UnityEngine.Events;

public class UnityEventEnemy_C : MonoBehaviour
{
    public UnityEvent OnTargetWithinRange;
    public UnityEvent OnReadyGroundSlam;
    public UnityEvent OnGroundSlam;
    public UnityEvent OnGroundSlamStiff;
    public UnityEvent OnAimStart;
    public UnityEvent OnAimKeep;
    public UnityEvent OnAimEnd;
    public UnityEvent OnLazer;
    public UnityEvent OnLazerStiff;

    public void VFX_TargetWithinRange()
    {
        OnTargetWithinRange.Invoke();
    }
    public void VFX_ReadyGroundSlam()
    {
        OnReadyGroundSlam.Invoke();
    }
    public void VFX_GroundSlam()
    {
        OnGroundSlam.Invoke();
    }
    public void VFX_GroundSlamStiff()
    {
        OnGroundSlamStiff.Invoke();
    }
    public void VFX_AimStart()
    {
        OnAimStart.Invoke();
        Debug.Log("AimStart");
    }
    public void VFX_AimKeep()
    {
        OnAimKeep.Invoke();
    }
    public void VFX_AimEnd()
    {
        OnAimEnd.Invoke();
    }
    public void VFX_Lazer()
    {
        OnLazer.Invoke();
    }
    public void VFX_LazerStiff()
    {
        OnLazerStiff.Invoke();
    }
}
