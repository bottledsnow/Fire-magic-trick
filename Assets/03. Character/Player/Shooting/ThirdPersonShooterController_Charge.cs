using UnityEngine;
using Cinemachine;
using StarterAssets;
using System.Threading.Tasks;
using UnityEngine.ProBuilder;
using MoreMountains.Feedbacks;

public class ThirdPersonShooterController_Charge : MonoBehaviour
{
    [SerializeField] private MMFeedbacks ReadyChargeFeedback;
    [SerializeField] private MMFeedbacks ReadyChargeCloseFeedback;
    [SerializeField] private MMFeedbacks OnChargeFeedback;
    [SerializeField] private MMFeedbacks OnChargeCloseFeedback;
    [SerializeField] private MMFeedbacks _ChargeFeedback;
    [SerializeField] private Transform pfChargeProjectile;
    [SerializeField] private ParticleSystem ChargePartical;
    [SerializeField] private ParticleSystem ReadyChageParticle;
    [SerializeField] private float chargeTime;

    private ParticleSystem.MainModule ChargeParticleMain;
    private ShootingSystem shootingSystem;
    private ControllerInput _Input;
    private bool triggerParticle;
    private bool Charge;
    private bool CanCharge;
    private bool readyCharge;
    private float chargePower;

    private void Start()
    {
        ChargeParticleMain = ChargePartical.main;
        ChargeParticleMain.duration = chargeTime;
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
                readyCharge = false;
                ReadyChargeCloseFeedback.PlayFeedbacks();
                _ChargeFeedback.PlayFeedbacks();
                shootingSystem.shoot(pfChargeProjectile, EnergySystem.EnergyMode.Fire);
                CanCharge = false;
                ReadyChageParticle.Stop();
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
            if (!triggerParticle)
            {
                triggerParticle = true;
                ChargePartical.Play();
                OnChargeFeedback.PlayFeedbacks();
            }
            chargePower += Time.deltaTime;
            if (chargePower > chargeTime)
            {
                ReadyCharge();
                Debug.Log("A-0");

            }
        } else
        {
            chargePower = 0;
            if(triggerParticle)
            {
                OnChargeCloseFeedback.PlayFeedbacks();
                ChargePartical.Stop();
                triggerParticle = false;
            }
        }
    }
    private void ReadyCharge()
    {
        if(!readyCharge)
        {
            readyCharge = true;
            Debug.Log("A-1");
            ReadyChargeFeedback.PlayFeedbacks();
            OnChargeCloseFeedback.PlayFeedbacks();
            CanCharge = true;
            ChargePartical.Stop();
            ReadyChageParticle.Play();
        }
    }
}
