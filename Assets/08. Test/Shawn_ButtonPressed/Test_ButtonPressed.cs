using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_ButtonPressed : MonoBehaviour
{
    public ControllerInput _Input;
    public float PressedTime = 0;
    public bool Pressed = false;
    public bool KeepPressed;
    private void Update()
    {
        ButtonPressed();
        ButtonRelease();
    }

    private void ButtonPressed()
    {
        if(_Input.LB)
        {
            PressedTime += Time.deltaTime;
            if(PressedTime < _Input.PressedSensitivity)
            {
                if (!Pressed)
                {
                    //Debug.Log("Button Pressed ,Time : " + PressedTime);
                    Pressed = true;
                }
            }else
            {
                if(!KeepPressed)
                {
                    KeepPressed = true;
                    Debug.Log("KeepPressed");
                }
                Debug.Log("Keep Update");
            }
        }
    }
    private void ButtonRelease()
    {
        if(!_Input.LB && Pressed)
        {
            //Debug.Log("Button Release ,Time : " + PressedTime);
            ButtonInitialization();
        }
    }
    private void ButtonInitialization()
    {
        Pressed = false;
        KeepPressed = false;
        PressedTime = 0;
    }
}
