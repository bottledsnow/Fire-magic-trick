using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour, VFX_Trigger
{
    private ParticleSystem particle;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    public void Trigger_VFX()
    {
        particle.Play();
    }
}
