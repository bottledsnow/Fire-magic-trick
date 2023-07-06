using UnityEngine;
using Cinemachine;
using StarterAssets;
using System.Threading.Tasks;
using UnityEngine.ProBuilder;
using MoreMountains.Feedbacks;

public class ThirdPersonShooterController_Charge : MonoBehaviour
{
    [SerializeField] private MMFeedbacks _ChargeFeedback;
    [SerializeField] private Transform pfChargeProjectile;
    [SerializeField] private ParticleSystem ChargePartical;
    [SerializeField] private float MaxChargePower;

    private ShootingSystem shootingSystem;
    private ControllerInput _Input;
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
        ChargeSystem();
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
                _ChargeFeedback.PlayFeedbacks();
                shootingSystem.shoot(pfChargeProjectile, EnergySystem.EnergyMode.Fire);
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
}
