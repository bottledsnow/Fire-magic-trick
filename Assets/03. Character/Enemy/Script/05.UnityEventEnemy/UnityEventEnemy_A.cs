using UnityEngine;
using UnityEngine.Events;

public class UnityEventEnemy_A : MonoBehaviour
{
    public UnityEvent OnRushStart;
    public UnityEvent OnRushReady;
    public UnityEvent OnLegSlashStart;
    public UnityEvent OnLegSlash_A;
    public UnityEvent OnLegSlash_B;
    public UnityEvent OnLegSlash_C;

    public void VFX_RushStart()
    {
        OnRushStart.Invoke();
    }
    public void VFX_RushReady()
    {
        OnRushReady.Invoke();
    }
    public void VFX_LegSlashStart()
    {
        OnLegSlashStart.Invoke();
    }
    public void VFX_LegSlash_A()
    {
        OnLegSlashStart.Invoke();
    }
    public void VFX_LegSlash_B()
    {
        OnLegSlashStart.Invoke();
    }
    public void VFX_LegSlash_C()
    {
        OnLegSlashStart.Invoke();
    }
}
