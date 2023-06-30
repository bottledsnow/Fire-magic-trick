using UnityEngine;
using Cinemachine;
using StarterAssets;
using System.Threading.Tasks;
using UnityEngine.ProBuilder;

public class ThirdPersonShooterController_Charge : MonoBehaviour
{
    
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform pfChargeProjectile;
    [SerializeField] private ParticleSystem ChargePartical;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float MaxChargePower;

    private ShootingSystem shootingSystem;
    private ControllerInput _Input;
    private bool shooting;
    private bool pistolrate;
    private bool triggerParticle;
    private bool Charge;
    private bool CanCharge;
    private float chargePower;

    private void Start()
    {
        _Input = GameManager.singleton._input;
        shootingSystem = GetComponent<ShootingSystem>();
    }
    private void Update()
    {
        Shooting();
        ChargeSystem();
        pistol();
    }
    private void ChargeSystem()
    {
       ChargeInput();
       ChargePower();
       ChargeRelease();
    }
    private void ChargeRelease()
    {
        if(!_Input.RT)
        {
            if(CanCharge)
            {
                shootingSystem.shoot(pfChargeProjectile);
                CanCharge = false;
            }
        }
    }
    private void ChargeInput()
    {
        if (_Input.RT) Charge = true;
        if (!_Input.RT) Charge = false;
    }
    private void ChargePower()
    {
        if(Charge)
        {
            if(!triggerParticle)
            {
                triggerParticle = true;
                ChargePartical.Play();
            }
            chargePower += Time.deltaTime;
            if(chargePower > MaxChargePower) CanCharge = true;
        } else
        {
            chargePower = 0;
            if(triggerParticle)
            {
                ChargePartical.Stop();
                triggerParticle = false;
            }
        }
    }
    private void Shooting()
    {
        if (_Input.RT && !shooting && !pistolrate)
        {
            ShootCooldown(shootCooldown);
            pistolrate = true;
            shootingSystem.shoot(pfBulletProjectile);
        }
    }
    private void pistol()
    {
        if (!_Input.RT) pistolrate = false;
    }
    private async void ShootCooldown(float shootCooldown)
    {
        shooting = true;
        await Task.Delay((int)(shootCooldown *1000));
        shooting = false;
    }
}
