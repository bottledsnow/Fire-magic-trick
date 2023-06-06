using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DemoEnd : MonoBehaviour
{
    [SerializeField] private int waitTime;
    [SerializeField] private GameObject FogWall;
    [SerializeField] private Animator DemoEndAnimator;
    private async void OnTriggerEnter(Collider other)
    {
        await Task.Delay(waitTime * 1000);
        FogWall.SetActive(false);
        DemoEndAnimator.Play("DemoEnd");
    }
}
