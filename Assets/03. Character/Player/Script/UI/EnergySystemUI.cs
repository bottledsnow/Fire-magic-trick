using MoreMountains.Tools;
using UnityEngine;

public class EnergySystemUI : MonoBehaviour
{
    [SerializeField] private MMProgressBar MMProgressBar;
    public void UpdateBar(float value)
    {
        if (MMProgressBar != null)
        {
            MMProgressBar.UpdateBar01(value);
        }
    }
}
