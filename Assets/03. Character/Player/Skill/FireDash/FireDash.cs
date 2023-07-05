using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireDash : MonoBehaviour
{
    private ThirdPersonShooterController _playerController;
    private void Start()
    {
        _playerController = GetComponent<ThirdPersonShooterController>();
    }

    private void Update()
    {
        
    }
    private void fireDash()
    {
        OutControl();
    }
    private void OutControl()
    {
        _playerController.enabled = false;
    }
}
