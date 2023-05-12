using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class UserInterfaceManager : MonoBehaviour
{
    private InputAction_A inputAction;
    [SerializeField]
    private Animator PokerArea;
    [SerializeField]
    private Image Poker_mid;
    [Header("Now")]
    [SerializeField]
    private Image Poker_up;
    [SerializeField]
    private Image Poker_down;
    [SerializeField]
    private Image Poker_left;
    [SerializeField]
    private Image Poker_right;
    [Header("Next")]
    [SerializeField]
    private Image Poker_up_next;
    [SerializeField]
    private Image Poker_down_next;
    [SerializeField]
    private Image Poker_left_next;
    [SerializeField]
    private Image Poker_right_next;
    [Header("Ä²µo¶¡¹j")]
    [SerializeField]
    private int MilliSecond;
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
    private void CrossKeyUp(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            crossKey(Poker_up);
            CrossKeyUpToMid();
        }
    }
    private void CrossKeyUpToMid()
    {
        //Poker_up.rectTransform.anchoredPosition = Poker_mid.rectTransform.anchoredPosition;
        Debug.Log("PokerArea has " + PokerArea.transform.childCount + " children");
        Poker_up.gameObject.transform.SetSiblingIndex(PokerArea.transform.childCount);
        PokerArea.Play("UpToMid");
    }
    private void CrossKeyDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            crossKey(Poker_down);
        }
    }
    private void CrossKeyLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            crossKey(Poker_left);
        }
    }
    private void CrossKeyRight(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            crossKey(Poker_right);
        }
    }
    private async void crossKey(Image poker_obj)
    {
        bool trigger = false;
        if (!trigger)
        {
            trigger = true;
            poker_obj.color = Color.blue;
            await Task.Delay(MilliSecond);
            poker_obj.color = Color.red;
            trigger = false;
        }
    }
}
