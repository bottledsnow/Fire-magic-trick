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
            GiveShootingEnergy(shootingEnergyRecover);
        }
    }
    public void TakeFireEnergy(float Energy)
    {
        FireEnergy -= Energy;
    }
    public void GiveFireEnergy(float Energy)
    {
        FireEnergy += Energy;
    }
    public void TakeShootingEnergy(float Energy)
    {
        ShootingEnergy -= Energy;
    }
    public void GiveShootingEnergy(float Energy)
    {
        ShootingEnergy += Energy;
    }
}
