using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestToolManager : MonoBehaviour
{
    private ThirdPersonController controller;

    [SerializeField] private GameObject player;
    [Header("Teleport")]
    [SerializeField] private GameObject teleportPoint;


    private void Awake()
    {
        controller = player.GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        teleport();
    }

    private void teleport()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            controller.enabled = false;
            player.transform.position = teleportPoint.transform.position;
            controller.enabled = true;
            Debug.Log("Test Tool : Teleport");
        }
    }
}
