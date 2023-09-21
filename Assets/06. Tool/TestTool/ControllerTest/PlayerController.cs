using UnityEngine;
using StarterAssets;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs _Inputs;
    [SerializeField] private ControllerInput _ControllerInput;
    [SerializeField] private ThirdPersonController thirdPersonController;

    [Header("Stick")]
    [SerializeField] private RectTransform LSB_transform;
    [SerializeField] private RectTransform RSB_transform;

    [Header("Right")]
    [SerializeField] private Image RT;
    [SerializeField] private Image RB;
    [SerializeField] private Image RSB;

    [Header("Left")]
    [SerializeField] private Image LT;
    [SerializeField] private Image LB;
    [SerializeField] private Image LSB;

    [Header("Button")]
    [SerializeField] private Image ButtonY;
    [SerializeField] private Image ButtonX;
    [SerializeField] private Image ButtonA;
    [SerializeField] private Image ButtonB;

    [Header("Arrow")]
    [SerializeField] private Image Arrow_Up;
    [SerializeField] private Image Arrow_Left;
    [SerializeField] private Image Arrow_Down;
    [SerializeField] private Image Arrow_Right;

    [Header("Setting")]
    [SerializeField] private Image Option;
    [SerializeField] private Image Window;
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
        OnButtonA();
        OnButtonB();
        OnButtonX();
        OnButtonY();
        OnArrowUp();
        OnArrowLeft();
        OnArrowDown();
        OnArrowRight();
        OnRightTrigger();
        OnRightButton();
        OnRightStickButton();
        OnLeftTrigger();
        OnLeftButton();
        OnLeftStickButton();
        OnOption();
        OnWindow();
    }
    private void getOriginStickPoint()
    {
        originLeftStickPoint = LSB_transform.anchoredPosition;
        originRightStickPoint = RSB_transform.anchoredPosition;
    }
    private void OnLeftStick() => LSB_transform.anchoredPosition = originLeftStickPoint + (_Inputs.move * 90f);
    private void OnRightStick()
    {
        Vector2 realPoint = new Vector2(_Inputs.look.x,- _Inputs.look.y);
        RSB_transform.anchoredPosition = originRightStickPoint + (realPoint / 3.3f);
    }
    private void OnButtonA() => ButtonA.color = _ControllerInput.ButtonA ? Color.green : Color.white;
    private void OnButtonB() => ButtonB.color = _ControllerInput.ButtonB ? Color.red : Color.white;
    private void OnButtonX() => ButtonX.color = _ControllerInput.ButtonX ? Color.blue : Color.white;
    private void OnButtonY() => ButtonY.color = _ControllerInput.ButtonY ? Color.yellow : Color.white;
    private void OnArrowUp() => Arrow_Up.color = _ControllerInput.ArrowKeyUp ? Color.blue : Color.white;
    private void OnArrowLeft() => Arrow_Left.color = _ControllerInput.ArrowKeyLeft ? Color.blue : Color.white;
    private void OnArrowDown() => Arrow_Down.color = _ControllerInput.ArrowKeyDown ? Color.blue : Color.white;
    private void OnArrowRight() => Arrow_Right.color = _ControllerInput.ArrowKeyRight ? Color.blue : Color.white;
    private void OnRightTrigger() => RT.color = _ControllerInput.RT ? Color.blue : Color.white;
    private void OnRightButton() => RB.color = _ControllerInput.RB ? Color.blue : Color.white;
    private void OnRightStickButton() => RSB.color = _ControllerInput.RSB ? Color.blue : Color.white;
    private void OnLeftTrigger() => LT.color = _ControllerInput.LT ? Color.blue : Color.white;
    private void OnLeftButton() => LB.color = _ControllerInput.LB ? Color.blue : Color.white;
    //private void OnLeftStickButton() => LSB.color = _ControllerInput.LSB ? Color.blue : Color.white;
    private void OnLeftStickButton() => LSB.color = _ControllerInput.SprintMode ? Color.blue : Color.white;
    private void OnOption() => Option.color = _ControllerInput.Window ? Color.blue : Color.white;
    private void OnWindow() => Window.color = _ControllerInput.Option ? Color.blue : Color.white;
}
