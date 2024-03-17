using MoreMountains.Feedbacks;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public enum SkillType
    {
        Reload,
        Dash,
        SuperDash,
        Kick,
        Float
    }
    //Script
    private EnergySystemUI _energySystemUI;

    //Variable
    [Header("Energy")]
    public float Energy;
    [SerializeField] private float StartEnergy;
    [Header("Recover")]
    [SerializeField] private float recoverRange;
    [SerializeField] private float recoverTime;
    [SerializeField] private float recover;
    [Header("GetEnergy")]
    private float timer;
    private bool isRecover;

    [Header("Cost")]
    [SerializeField] private bool isTestMode;
    [SerializeField] private float SuperDashCost = 10;
    [SerializeField] private float ReloadCost = 10;
    [SerializeField] private float FloatCost = 10;
    [SerializeField] private float DashCost = 10;
    [SerializeField] private float KickCost = 10;
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player Feedbacks_NoEnegy;
    [SerializeField] private MMF_Player Feedback_GetEnergy;


    private void Start()
    {
        _energySystemUI = GameManager.singleton.UISystem.GetComponent<EnergySystemUI>();

        //SetEnegy(StartEnergy);
    }
    private void Update()
    {
        RecoverSystem();
    }
    public bool canUseEnegy(SkillType type)
    {
        float need =0;
        switch (type)
        {
            case SkillType.Reload:
                need = ReloadCost;
                break;
            case SkillType.Dash:
                need = DashCost;
                break;
            case SkillType.SuperDash:
                need = SuperDashCost;
                break;
            case SkillType.Kick:
                need = KickCost;
                break;
            case SkillType.Float:
                need = FloatCost;
                break;
        }

        if(Energy > need)
        {
            Energy -= need;
            return true;
        }else
        {
            return false;
        }
    }
    #region Set-UI
    public void SetEnegy(float value)
    {
        Energy = value;
        UpdateUI();
    }
    public void UpdateUI()
    {
        float value = Energy / 100;
        if (_energySystemUI != null)
        {
            _energySystemUI.UpdateBar(value);
        }
    }
    #endregion
    #region Increase Decrease
    private void Increase(float energy)
    {
        Energy += energy;

        if(Energy>100)
        {
            Energy = 100;
        }
        UpdateUI(Energy);
    }
    private void Decrease(float energy)
    {
        Energy -= energy;

        if (Energy <0)
        {
            Energy = 0;
        }
        UpdateUI(Energy);
    }
    private void UpdateUI(float Value)
    {
        float value = Value / 100;
        if(_energySystemUI != null)
        {
            _energySystemUI.UpdateBar(value);
        }
    }
    #endregion
    #region use Skill
    public void UseEnegy(int value)
    {
        Decrease(value);
    }
    #endregion
    public void GetEnergy(float value)
    {
        Feedback_GetEnergy.PlayFeedbacks();
        Increase(value);
    }
    public void DecreaseEnergy(int Energy)
    {
        Decrease(Energy);
    }
    #region Recover
    private void RecoverSystem()
    {
        RecoverCheck();
        recoverTimer();
    }
    private void RecoverCheck()
    {
        bool condition = Energy < recoverRange;
        isRecover = condition ? true : false;
    }
    private void recoverTimer()
    {
        if (isRecover)
        {
            timer += Time.deltaTime;

            if (timer > recoverTime)
            {
                Increase(recover);
                timer = 0;
            }
        }
    }
    #endregion
}
