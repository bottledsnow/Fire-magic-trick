using UnityEngine;
using System.Threading.Tasks;

public class EnergySystem : MonoBehaviour
{
    #region Enum
    public enum EnergyMode
    {
        Shooting,
        Fire,
    }
    #endregion
    [Header("Energy System Component")]
    [SerializeField] private FireDash _fireDash;
    [Header("State")]
    [Range(0, 100)]
    [SerializeField] private float fireEnergy;
    [Range(0, 100)]
    [SerializeField] private float shootingEnergy;
    public float FireEnergy
    {
        get { return fireEnergy; }
        private set
        {
            fireEnergy = Mathf.Clamp(value, 0f, 100f);
        }
    }
    public float ShootingEnergy
    {
        get { return shootingEnergy; }
        private set
        {
            shootingEnergy = Mathf.Clamp(value, 0f, 100f);
        }
    }

    [Header("shooting")]
    [SerializeField] float shootingEnergyRecover;
    [SerializeField] float shootingEnergyRecoverCD;
    private bool isRecover = true;
    public bool FireEnergyFull;
    public bool ShootingEnergyFull;

    private void Start()
    {
        EnergyRecover();
    }
    private void Update()
    {
        EnergyCheck();
    }
    private void EnergyCheck()
    {
        FireEnergyFull = (FireEnergy == 100);
        ShootingEnergyFull = (ShootingEnergy == 100);
    }
    private async void EnergyRecover()
    {
        while (isRecover)
        {
            await Task.Delay((int)(shootingEnergyRecoverCD * 1000));
            ReplenishShootingEnergy(shootingEnergyRecover);
        }
    }
    public void ConsumeFireEnergy(float cost,out bool canCostEnergy)
    {
        if(FireEnergy > cost)
        {
            canCostEnergy = true;
            FireEnergy -= cost;
        }else
        {
            canCostEnergy = false;
            Debug.Log("Not enough FireEnergy");
        }
    }
    public void ReplenishFireEnergy(float Energy)
    {
        FireEnergy += Energy;
    }
    public void CunsumeShootingEnergy(float cost, out bool canuseEnergy)
    {
        if(ShootingEnergy > cost)
        {
            canuseEnergy = true;
            ShootingEnergy -= cost;
        }else
        {
            canuseEnergy= false;
            Debug.Log("Not enough ShootingEnergy");
        }
    }
    public void ReplenishShootingEnergy(float Energy)
    {
        ShootingEnergy += Energy;
    }
}
