using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Trigger_Area : MonoBehaviour
{
    [SerializeField] public GameObject TriggerObj;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            VFX_Trigger trigger = TriggerObj.GetComponent<VFX_Trigger>();
            trigger.Trigger_VFX();
        }
    }
}
