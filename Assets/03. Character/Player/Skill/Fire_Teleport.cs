using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Teleport : MonoBehaviour
{
    [SerializeField] private ThirdPersonController _Player;

    private void Update()
    {
        
    }

    private void ClosePlayerController() => _Player.enabled = false;
}
