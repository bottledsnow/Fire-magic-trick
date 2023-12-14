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
        OnLegSlash_A.Invoke();
    }
    public void VFX_LegSlash_B()
    {
        OnLegSlash_B.Invoke();
    }
    public void VFX_LegSlash_C()
    {
        OnLegSlash_C.Invoke();
    }
}
