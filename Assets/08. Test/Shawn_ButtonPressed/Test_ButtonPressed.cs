using UnityEngine;
using UnityEngine.Rendering;

public class Test_ButtonPressed : MonoBehaviour
{
    public ControllerInput _Input;
    //copy
    public float PressedTime = 0;
    public bool Pressed = false;
    public bool KeepPressed;
    private void Update()
    {
        ButtonSystem();
    }
    private void ButtonSystem()
    {
        ButtonTrigger();
        ButtonRelease();
    }
    
    private void ButtonTrigger()
    {
        if(_Input.LB)
        {
            PressedTime += Time.deltaTime;
            ButtonClick();
            ButtonPressedStart();
        }
    }
    private void ButtonClick()
    {
        if (!Pressed)
        {
            Pressed = true;
            Debug.Log("Button");
        }
    }
    private void ButtonPressedStart()
    {
        if (PressedTime > _Input.PressedSensitivity)
        {
            if (!KeepPressed)
            {
                KeepPressed = true;
                Debug.Log("Button Pressed Start");
            }
            Debug.Log("Keep Update");
        }
    }
    private void ButtonRelease()
    {
        if (!_Input.LB)
        {
            ButtonClickOnly();
            ButtonPressedEnd();
            ButtonInitialization();
        }
    }
    private void ButtonClickOnly()
    {
        if (PressedTime < _Input.PressedSensitivity && PressedTime != 0)
        {
            Debug.Log("Button Click Only");
        }
    }
    private void ButtonPressedEnd()
    {
        if(PressedTime > _Input.PressedSensitivity)
        {
            Debug.Log("Button Pressed End");
        }
    }
    
    private void ButtonInitialization()
    {
        Pressed = false;
        KeepPressed = false;
        PressedTime = 0;
    }
}
