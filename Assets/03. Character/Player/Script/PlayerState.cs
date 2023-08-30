using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private ThirdPersonController controller;

    private void Start()
    {
        controller = GetComponent<ThirdPersonController>();
    }

    public void TakeControl()
    {
        controller.enabled = true;
    }
    public void OutControl()
    {
        controller.enabled = false;
    }
}
