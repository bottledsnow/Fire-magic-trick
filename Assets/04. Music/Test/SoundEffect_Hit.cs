using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect_Hit : MonoBehaviour
{
    [SerializeField] private Transform pfSoundEffect;
    private void OnCollisionEnter(Collision collision)
    {
        if(pfSoundEffect != null)
        {
            Instantiate (pfSoundEffect, transform.position, Quaternion.identity);
        }
    }
}
