using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BlackWall : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] VFX_Wall;
    [SerializeField] private ParticleSystem VFX_Fog;
    [SerializeField] private GameObject Fog;
    [SerializeField] private Collider FogCollider;
    [SerializeField] private int unlockNumber;

    private int absorbNumber=0;
    private bool trigger;
    private Rigidbody rb;
    private Collider boxCollider;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Soul")
        {
            VFX_Trigger vfx = other.GetComponent<VFX_Trigger>();
            vfx.Trigger_VFX();
            VFX_Wall[absorbNumber].Play();
            absorbNumber++;
        }

        if(absorbNumber>=unlockNumber)
        {
            unlock();
        }
    }

    private async void unlock()
    {
        await Task.Delay(3000);
        rb.drag = 10;
        boxCollider.enabled = false;

        for(int i=0;i<VFX_Wall.Length;i++)
        {
            VFX_Wall[i].Stop();
        }

        VFX_Fog.Stop();
        FogCollider.enabled = false;
    }
}
