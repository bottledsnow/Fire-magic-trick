using MoreMountains.Tools;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class EnergySystemUI : MonoBehaviour
{
    [SerializeField] private MMProgressBar MMProgressBar;
    public void UpdateBar(float value)
    {
        MMProgressBar.UpdateBar01(value);
    }
}
