using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UserInterfaceManager : MonoBehaviour
{
    private InputAction_A inputAction;
    [SerializeField]
    private Image Poker_up;
    [SerializeField]
    private Image Poker_down;
    [SerializeField]
    private Image Poker_left;
    [SerializeField]
    private Image Poker_right;
    //this area need to encapsulation other Script
    private void Awake()
    {
        inputAction = new InputAction_A();
        inputAction.InputMap_A.Enable();
        inputAction.InputMap_A.CrossKeyUp.performed += CrossKeyUp;
        inputAction.InputMap_A.CrossKeyDown.performed += CrossKeyDown;
        inputAction.InputMap_A.CrossKeyLeft.performed += CrossKeyLeft;
        inputAction.InputMap_A.CrossKeyRight.performed += CrossKeyRight;
    }
    private void Update()
    {
        
    }

    private void CrossKeyUp(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Poker_up.color = Color.red;
        }
    }
    private void CrossKeyDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Poker_down.color = Color.red;
        }
    }
    private void CrossKeyLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Poker_left.color = Color.red;
        }
    }
    private void CrossKeyRight(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Poker_right.color = Color.red;
        }
    }

}
