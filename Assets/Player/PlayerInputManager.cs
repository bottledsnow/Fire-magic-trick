using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Horizontal")]
    public float Horizontal;
    [Header("Vertical")]
    public float Vertical;
    [Header("Mouse")]
    public float MouseX;
    public float MouseY;
    private void Update()
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
    }
}
