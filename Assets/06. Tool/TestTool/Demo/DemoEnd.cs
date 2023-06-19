using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DemoEnd : MonoBehaviour
{
    private bool trigger;
    [SerializeField] private int waitTime;
    [SerializeField] private GameObject FogWall;
    [SerializeField] private Animator DemoEndAnimator;
    [SerializeField] private Animator KabalaAnimator;
    private async void OnTriggerEnter(Collider other)
    {
        if(!trigger && other.tag =="Player")
        {
            KabalaAnimator.Play("DemoEndCamera");
            await Task.Delay(waitTime * 1000);
            FogWall.SetActive(false);
            DemoEndAnimator.Play("DemoEnd");
            trigger=true;   
        }
    }
}
