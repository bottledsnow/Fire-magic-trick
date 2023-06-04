using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Kabala : MonoBehaviour, VFX_Trigger
{
    [SerializeField] private ParticleSystem[] VFX_Circle;
    [SerializeField] private int showTime;

    private async void ShowKabala()
    {
        for(int i=0;i<VFX_Circle.Length;i++)
        {
            VFX_Circle[i].Play();
            await Task.Delay(showTime);
        }
    }

    public void Trigger_VFX()
    {
        ShowKabala();
    }
}
