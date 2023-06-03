using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Soul")
        {
            VFX_Trigger vfx = other.GetComponent<VFX_Trigger>();
            vfx.Trigger_VFX();
        }
    }
}
