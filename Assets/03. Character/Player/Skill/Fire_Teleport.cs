using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fire_Teleport : MonoBehaviour
{
    private ControllerInput _input;
    private ThirdPersonController _Player;

    private void Update()
    {
        ignit();
    }
    private void Start()
    {
        _input = GameManager.singleton._input;
        _Player = GetComponent<ThirdPersonController>();
    }
    private void ignit()
    {
        if(_input.RB)
        {
            
        }
    }

    private void ClosePlayerController() => _Player.enabled = false;
}
