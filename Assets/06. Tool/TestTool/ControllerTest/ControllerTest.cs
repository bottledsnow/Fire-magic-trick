using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class ControllerTest : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs _Inputs;
    [SerializeField] private ThirdPersonController thirdPersonController;

    [Header("Stick")]
    [SerializeField] private RectTransform LeftStickPoint;
    [SerializeField] private RectTransform RightStickPoint;

    [Header("Button")]
    [SerializeField] private Image ButtonSouth;
    private Vector2 originLeftStickPoint;
    private Vector2 originRightStickPoint;

    private void Start()
    {
        getOriginStickPoint();
    }
    private void Update()
    {
        OnLeftStick();
        OnRightStick();
    }

    private void getOriginStickPoint()
    {
        originLeftStickPoint = LeftStickPoint.anchoredPosition;
        originRightStickPoint = RightStickPoint.anchoredPosition;
    }
    private void OnLeftStick() => LeftStickPoint.anchoredPosition = originLeftStickPoint + (_Inputs.move * 90f);
    private void OnRightStick()
    {
        Vector2 realPoint = new Vector2(_Inputs.look.x,- _Inputs.look.y);
        RightStickPoint.anchoredPosition = originRightStickPoint + (realPoint / 3.3f);
    }
    private void OnButtonSouth()
    {
        
    }

}
