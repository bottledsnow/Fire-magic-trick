using UnityEngine;
using UnityEngine.ProBuilder;

public class Shooting_Mode : MonoBehaviour
{
    [SerializeField] private bool isNormal;
    [SerializeField] private bool isCharge;
    private ControllerInput _input;
    private Shooting_Normal _shooting_Normal;
    private Shooting_Charge _shooting_Charge;

    enum Mode
    {
        Normal,
        Charge
    }
    private Mode mode;

    private bool ArrowKeyUp;
    private bool Trigger;
    private void Start()
    {
        _input = GameManager.singleton._input;
        _shooting_Normal = GetComponent<Shooting_Normal>();
        _shooting_Charge = GetComponent<Shooting_Charge>();
    }

    private void Update()
    {
        Initialization();
        CheckArrowKey();
        ChooseMode();
    }
    #region Key
    private void Initialization()
    {
        if (!_input.ArrowKeyUp && Trigger)
        {
            Trigger = false;
        }
    }
    private void CheckArrowKey()
    {
        if(_input.ArrowKeyUp && !Trigger)
        {
            ArrowKeyUp = !ArrowKeyUp;
            Trigger = true;
        }
    }
    #endregion
    #region ModeSystem
    private void ChooseMode()
    {
        if(ArrowKeyUp)
        {
            ShootingMode(Mode.Normal);
        }else
        {
            ShootingMode(Mode.Charge);
        }
    }
    private void ShootingMode(Mode mode)
    {
        if(mode == Mode.Normal)
        {
            Mode_Normal(true);
            Mode_Charge(false);
        }
        if(mode == Mode.Charge)
        {
            Mode_Normal(false);
            Mode_Charge(true);
        }
    }
    #endregion
    private void Mode_Normal(bool Active)
    {
        isNormal = Active;
        _shooting_Normal.enabled = isNormal? true : false;
    }
    private void Mode_Charge(bool Active)
    {
        isCharge = Active;
        _shooting_Charge.enabled = isCharge ? true : false;
    }
}
