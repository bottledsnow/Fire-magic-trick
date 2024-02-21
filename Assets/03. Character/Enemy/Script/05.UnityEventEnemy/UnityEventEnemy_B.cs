using UnityEngine;
using UnityEngine.Events;

public class UnityEventEnemy_B : MonoBehaviour
{
    public UnityEvent OnSideDodge;
    public UnityEvent OnAimingStart;
    public UnityEvent OnAimingFinishReady;
    public UnityEvent OnShootingFinishWait;
    public UnityEvent OnMoveStart;
    public UnityEvent OnMoveEnd;

    public void VFX_SideDodge()
    {
        OnSideDodge.Invoke();
    }
    public void VFX_AimingStart()
    {
        OnAimingStart.Invoke();
    }
    public void VFX_AimingFinishReady()
    {
        OnAimingFinishReady.Invoke();
    }
    public void VFX_ShootingFinishWait()
    {
        OnShootingFinishWait.Invoke();
    }
    public void VFX_MoveStart()
    {
        OnMoveStart.Invoke();
    }
    public void VFX_MoveEnd()
    {
        OnMoveEnd.Invoke();
    }
}
