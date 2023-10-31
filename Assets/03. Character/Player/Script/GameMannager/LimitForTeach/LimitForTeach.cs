using UnityEngine;

public class LimitForTeach : MonoBehaviour
{
    [SerializeField] private bool useTeach;

    private FireFloat _fireFloat;
    private FireDash _fireDash;
    private SuperDash _superDash;
    private SuperDashCameraCheck _superDashCameraCheck;
    private SuperDashKick _superDashKick;
    private SuperDashKickDown _superDashKickDown;

    private Shooting_Mode _shooting_mode;
    private Shooting_Magazing _shooting_magazing;

    private EnergySystem _energySystem;
    [Header("UI")]
    [SerializeField] private GameObject UI_Energy;
    [SerializeField] private GameObject UI_ShootingMagazing;
    [SerializeField] private GameObject UI_ShootingMode;

    private void Start()
    {
        _fireFloat = GameManager.singleton.EnergySystem.GetComponent<FireFloat>();
        _fireDash = GameManager.singleton.EnergySystem.GetComponent<FireDash>();
        _superDash = GameManager.singleton.EnergySystem.GetComponent<SuperDash>();
        _superDashCameraCheck = GameManager.singleton.EnergySystem.GetComponent<SuperDashCameraCheck>();
        _superDashKick = GameManager.singleton.EnergySystem.GetComponent<SuperDashKick>();
        _superDashKickDown = GameManager.singleton.EnergySystem.GetComponent<SuperDashKickDown>();
        _shooting_mode = GameManager.singleton.ShootingSystem.GetComponent<Shooting_Mode>();
        _shooting_magazing = GameManager.singleton.ShootingSystem.GetComponent<Shooting_Magazing>();
        _energySystem = GameManager.singleton.Player.GetComponent<EnergySystem>();

        Initialization();
    }

    private void Initialization()
    {
        if(useTeach)
        {
            SetFloatingScript(false);
            SetBulletMagazingScript(false);
            SetShootingModeScript(false);
            SetDashScript(false);
            SetSuperDashScript(false);
            SetUI_EnergySystem(false);
            SetUI_ShootingMagazing(false);
            SetUI_ShootingMode(false);
        }
    }
    #region Set
    public void SetBulletMagazingScript(bool value)
    {
        _shooting_magazing.enabled = value;
    }
    public void SetShootingModeScript(bool value)
    {
        _shooting_mode.enabled = value;
    }
    public void SetFloatingScript(bool value)
    {
        _fireFloat.enabled = value;
    }
    public void SetDashScript(bool value)
    {
        _fireDash.enabled = value;
    }
    public void SetSuperDashScript(bool value)
    {
        _superDash.enabled = value;
        _superDashCameraCheck.enabled = value;
        _superDashKick.enabled = value;
        _superDashKickDown.enabled = value;
    }
    public void SetUI_EnergySystem(bool value)
    {
        UI_Energy.SetActive(value);
        _energySystem.enabled = value;
    }
    public void SetUI_ShootingMagazing(bool value)
    {
        UI_ShootingMagazing.SetActive(value);
    }
    public void SetUI_ShootingMode(bool value)
    {
        UI_ShootingMode.SetActive(value);
    }
    #endregion
}
