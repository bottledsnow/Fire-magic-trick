using UnityEngine;

public class Shooting_Mode : MonoBehaviour
{
    [SerializeField] private bool isNormal;
    [SerializeField] private bool isCharge;
    private ControllerInput _input;
    private Shooting_Normal _shooting_Normal;
    private Shooting_Charge _shooting_Charge;
    private ShootingModeUI _shootingModeUI;
    private bool needChoose;

    enum Mode
    {
        Normal,
        Charge
    }
    private Mode mode;

    private bool ArrowKeyUp =true;
    private bool Trigger;
    private void Start()
    {
        _input = GameManager.singleton._input;
        _shooting_Normal = GetComponent<Shooting_Normal>();
        _shooting_Charge = GetComponent<Shooting_Charge>();
        _shootingModeUI = GameManager.singleton.UISystem.GetComponent<ShootingModeUI>();

        ShootingMode(Mode.Normal);
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
            needChoose = true;
        }
    }
    #endregion
    #region ModeSystem
    private void ChooseMode()
    {
        if(ArrowKeyUp)
        {
            if(needChoose)
            {
                ShootingMode(Mode.Normal);
                needChoose = false;
            }
        }
        else
        {
            if(needChoose)
            {
                ShootingMode(Mode.Charge);
                needChoose = false;
            }
        }
    }
    private void ShootingMode(Mode mode)
    {
        if(mode == Mode.Normal)
        {
            _shootingModeUI.ChooseNormal();
            Mode_Normal(true);
            Mode_Charge(false);
        }
        if(mode == Mode.Charge)
        {
            _shootingModeUI.ChooseCharge();
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
