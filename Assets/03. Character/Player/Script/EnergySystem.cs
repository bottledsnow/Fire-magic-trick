using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    private EnergySystemUI _energySystemUI;
    [Header("Energy")]
    public float Energy;
    [Header("Recover")]
    [SerializeField] private float recoverRange;
    [SerializeField] private float recoverTime;
    [SerializeField] private float recover;
    private float timer;
    private bool isRecover;

    [Header("Cost")]
    [SerializeField] private float SuperDashCost = 10;
    [SerializeField] private float ReloadCost = 10;
    [SerializeField] private float ChargeCost = 10;
    [SerializeField] private float FloatCost = 10;
    [SerializeField] private float DashCost = 10;
    [SerializeField] private float KickCost = 10;


    [Header("Get")]
    [SerializeField] private float LampGet = 40;
    [SerializeField] private float KillGet = 10;
    [SerializeField] private float SuperKillGet = 10;

    private void Start()
    {
        _energySystemUI = GameManager.singleton.UISystem.GetComponent<EnergySystemUI>();
    }
    private void Update()
    {
        RecoverSystem();
    }
    
    #region Increase Decrease
    private void Increase(float energy)
    {
        Energy += energy;
        UpdateUI(Energy);
    }
    private void Decrease(float energy)
    {
        Energy -= energy;
        UpdateUI(Energy);
    }
    private void UpdateUI(float Value)
    {
        float value = Value / 100;
        _energySystemUI.UpdateBar(value);
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
    #endregion
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
}
