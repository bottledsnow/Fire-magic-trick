using UnityEngine;
using MoreMountains.Feedbacks;

public class Shooting_Charge : MonoBehaviour
{
    public int explodeDamage;
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
    private Shooting _shooting;
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
        _shooting = GetComponent<Shooting>();
        shootingSystem = _Input.GetComponent<ShootingSystem>();
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
                _shooting.Shoot_Fire(pfChargeProjectile);
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
            ReadyChargeFeedback.PlayFeedbacks();
            OnChargeCloseFeedback.PlayFeedbacks();
            CanCharge = true;
            ChargePartical.Stop();
            ReadyChageParticle.Play();
        }
    }
}
