using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakage : MonoBehaviour
{
    [SerializeField] private GameObject DemoEndText;
    [SerializeField] private GameObject TriggerObj;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Bullet")
        {
            VFX_Trigger trigger = TriggerObj.GetComponent<VFX_Trigger>();
            trigger.Trigger_VFX();

            TriggerEvent();
        }
    }

    private void TriggerEvent()
    {
        DemoEndText.SetActive(true);
    }
}
