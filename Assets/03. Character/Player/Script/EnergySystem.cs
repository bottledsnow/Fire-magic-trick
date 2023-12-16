using MoreMountains.Feedbacks;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    private EnergySystemUI _energySystemUI;
    [Header("Energy")]
    public float Energy;
    [SerializeField] private float StartEnergy;
    [Header("Recover")]
    [SerializeField] private float recoverRange;
    [SerializeField] private float recoverTime;
    [SerializeField] private float recover;
    [Header("GetEnergy")]
    [SerializeField] private MMF_Player Feedback_GetEnergy;

    private float timer;
    private bool isRecover;

    [Header("Cost")]
    [SerializeField] private float SuperDashCost = 10;
    [SerializeField] private float ReloadCost = 10;
    [SerializeField] private float ChargeCost = 10;
    [SerializeField] private float FloatCost = 10;
    [SerializeField] private float DashCost = 10;
    [SerializeField] private float KickCost = 10;
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player Feedbacks_NoEnegy;

    //[Header("Get")]
    //[SerializeField] private float LampGet = 40;
    //[SerializeField] private float KillGet = 10;
    //[SerializeField] private float SuperKillGet = 10;

    private void Start()
    {
        _energySystemUI = GameManager.singleton.UISystem.GetComponent<EnergySystemUI>();

        //SetEnegy(StartEnergy);
    }
    private void Update()
    {
        RecoverSystem();
    }
    #region Set
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
            Debug.Log("Increase Energy is over 100");
            Energy = 100;
        }

        UpdateUI(Energy);
    }
    private void Decrease(float energy)
    {
        Energy -= energy;

        if (Energy <0)
        {
            Debug.Log("Decrease Energy is over 100");
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
    public void UseDash(out bool CanUse)
    {
        CanUse = CheckEnergyCanUse(DashCost);

        if(CanUse)
        {
            Decrease(DashCost);
        } else
        {
            noEnegyFeedbacks();
            Debug.Log("Energy is not enough");
        }
    }
    public void UseSuperDash(out bool CanUse)
    {
        CanUse = CheckEnergyCanUse(SuperDashCost);

        if(CanUse)
        {
            Decrease(SuperDashCost);
        } else
        {
            noEnegyFeedbacks();
            Debug.Log("Energy is not enough");
        }
    }
    public void UseKick(out bool CanUse)
    {
        CanUse = CheckEnergyCanUse(KickCost);

        if (CanUse)
        {
            Decrease(KickCost);
        }else
        {
            noEnegyFeedbacks();
            Debug.Log("Energy is not enough");
        }
    }
    public void UseFloat(out bool CanUse)
    {
        CanUse = CheckEnergyCanUse(FloatCost);

        if (CanUse)
        {
            Decrease(FloatCost);
        }
        else
        {
            noEnegyFeedbacks();
            Debug.Log("Energy is not enough");
        }
    }
    public void UseReload(out bool CanUse)
    {
        CanUse = CheckEnergyCanUse(ReloadCost);

        if (CanUse)
        {
            Decrease(ReloadCost);
        }
        else
        {
            noEnegyFeedbacks();
            Debug.Log("Energy is not enough");
        }
    }
    public void UseCharge(out bool CanUse)
    {
        CanUse = CheckEnergyCanUse(ChargeCost);

        if (CanUse)
        {
            Decrease(ChargeCost);
        }
        else
        {
            noEnegyFeedbacks();
            Debug.Log("Energy is not enough");
        }
    }
    private bool CheckEnergyCanUse(float value)
    {
        float energy = Energy - value;
        if(energy<0)
        {
            return false;
        }else
        {
            return true;
        }
    }
    private void noEnegyFeedbacks()
    {
        Feedbacks_NoEnegy.PlayFeedbacks();
    }
    #endregion
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
    public void FullEnergy()
    {
        float value = 100-Energy;
        Debug.Log(value);
        Increase(value);
    }
    #endregion
    #region GetEnergy
    public void GetEnergy(float value)
    {
        Feedback_GetEnergy.PlayFeedbacks();
        Increase(value);
    }
    #endregion
    #region TakeDamage
    public void DecreaseEnergy(int Energy)
    {
        Decrease(Energy);
    }
    #endregion 
}
