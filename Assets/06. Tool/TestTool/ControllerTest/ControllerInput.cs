using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    [Header("Game")]
    public bool SprintMode;
    public float PressedSensitivity;
    private bool TurnOn = false;
    private bool trigger = false;
    [Header("Stick")]
    public Vector2 LeftStick;
    public Vector2 RightStick;

    [Header("Controller Input Buttons")]
    public bool ButtonY;
    public bool ButtonX;
    public bool ButtonA;
    public bool ButtonB;

    [Header("D-Pad")]
    public bool ArrowKeyUp;
    public bool ArrowKeyLeft;
    public bool ArrowKeyDown;
    public bool ArrowKeyRight;

    [Header("L")]
    public bool LT;
    public bool LB;
    public bool LSB;

    [Header("R")]
    public bool RT;
    public bool RB;
    public bool RSB;

    [Header("Setting")]
    public bool Option;
    public bool Window;

    #region InputSystem_Input
    public void OnController_Stick_Left(InputValue value)
    {
        LeftStick = value.Get<Vector2>();
    }
    public void OnController_Stick_Right(InputValue value)
    {
        RightStick = value.Get<Vector2>();
    }
    public void OnController_Button_A(InputValue value)
    {
        Input_Button_A(value.isPressed);
    }
    public void OnController_Button_B(InputValue value)
    {
        Input_Button_B(value.isPressed);
    }
    public void OnController_Button_X(InputValue value)
    {
        Input_Button_X(value.isPressed);
    }
    public void OnController_Button_Y(InputValue value)
    {
        Input_Button_Y(value.isPressed);
    }
    public void OnController_ArrowKey_Up(InputValue value)
    {
        Input_Arrow_Up(value.isPressed);
    }
    public void OnController_ArrowKey_Left(InputValue value)
    {
        Input_Arrow_Left(value.isPressed);
    }
    public void OnController_ArrowKey_Down(InputValue value)
    {
        Input_Arrow_Down(value.isPressed);
    }
    public void OnController_ArrowKey_Right(InputValue value)
    {
        Input_Arrow_Right(value.isPressed);
    }
    public void OnController_Right_Trigger(InputValue value)
    {
        Input_Right_Trigger(value.isPressed);
    }
    public void OnController_Right_Button(InputValue value)
    {
        Input_Right_Button(value.isPressed);
    }
    public void OnController_Right_StickButton(InputValue value)
    {
        Input_Right_StickButton(value.isPressed);
    }
    public void OnController_Left_Trigger(InputValue value)
    {
        Input_Left_Trigger(value.isPressed);
    }
    public void OnController_Left_Button(InputValue value)
    {
        Input_Left_Button(value.isPressed);
    }
    public void OnController_Left_StickButton(InputValue value)
    {
        Input_Left_StickButton(value.isPressed);
    }
    public void OnController_Option(InputValue value)
    {
        Input_Option(value.isPressed);
    }
    public void OnController_Window(InputValue value)
    {
        Input_Window(value.isPressed);
    }
    #endregion
    #region Button_Input
    public void Input_Button_A(bool newButtonState)
    {
        ButtonA = newButtonState;
    }
    public void Input_Button_B(bool newButtonState)
    {
        ButtonB = newButtonState;
    }
    public void Input_Button_X(bool newButtonState)
    {
        ButtonX = newButtonState;
    }
    public void Input_Button_Y(bool newButtonState)
    {
        ButtonY = newButtonState;
    }
    public void Input_Arrow_Up(bool newButtonState)
    {
        ArrowKeyUp = newButtonState;
    }
    public void Input_Arrow_Left(bool newButtonState)
    {
        ArrowKeyLeft = newButtonState;
    }
    public void Input_Arrow_Down(bool newButtonState)
    {
        ArrowKeyDown = newButtonState;
    }
    public void Input_Arrow_Right(bool newButtonState)
    {
        ArrowKeyRight = newButtonState;
    }
    public void Input_Right_Trigger(bool newButtonState)
    {
        RT = newButtonState;
    }
    public void Input_Right_Button(bool newButtonState)
    {
        RB = newButtonState;
    }
    public void Input_Right_StickButton(bool newButtonState)
    {
        RSB = newButtonState;
    }
    public void Input_Left_Trigger(bool newButtonState)
    {
        LT = newButtonState;
    }
    public void Input_Left_Button(bool newButtonState)
    {
        LB = newButtonState;
    }
    public void Input_Left_StickButton(bool newButtonState)
    {
        LSB = newButtonState;
    }
    public void Input_Option(bool newButtonState)
    {
        Option = newButtonState;
    }
    public void Input_Window(bool newButtonState)
    {
        Window = newButtonState;
    }
    #endregion

    private void Update()
    {
        SwitchLSB();
        TriggerLSB();
        StopSrint();
    }
    private void SwitchLSB()
    {
        if(LSB && !trigger)
        {
            trigger = true;
            if(TurnOn)
            {
                TurnOn = false;
                SprintMode = false;
            }else
            {
                TurnOn = true;
                SprintMode = true;
            }
        }
    }
    private void TriggerLSB()
    {
        if(!LSB) trigger = false;
    }
    private void StopSrint()
    {
        if(LeftStick.x == 0 && LeftStick.y == 0)
        {
            SprintMode = false;
        }
    }
}
