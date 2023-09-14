using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    private ControllerInput _input;
    private bool isChooseNormal = true;

    [Header("Shooting System Component")]
    public Shooting _Shooting;
    public Shooting_Check _ShootingCheck;
    public Shooting_Aim _ShootingAim;
    public Shooting_Normal _ShootingNormal;
    public Shooting_Charge _ShootingCharge;
    private void Awake()
    {
        _ShootingCheck = _Shooting.GetComponent<Shooting_Check>();
        _ShootingAim = _Shooting.GetComponent<Shooting_Aim>();
        _ShootingNormal = _Shooting.GetComponent<Shooting_Normal>();
        _ShootingCharge = _Shooting.GetComponent<Shooting_Charge>(); 
    }
    private void Start()
    {
        _input = GameManager.singleton._input;
        ToChooseNormal();
    }
    private void Update()
    {
        //ChooseMode();
    }
    private void ChooseMode()
    {
        if (_input.ArrowKeyLeft && !isChooseNormal)
        {
            isChooseNormal = true;
            ToChooseNormal();
        }
        else if (_input.ArrowKeyRight && isChooseNormal)
        {
            isChooseNormal = false;
            ToChooseCharge();
        }
    }
    private void ToChooseNormal()
    {
        _ShootingNormal.enabled = true;
        _ShootingCharge.enabled = false;
    }
    private void ToChooseCharge()
    {
        _ShootingNormal.enabled = false;
        _ShootingCharge.enabled = true;
    }
}
