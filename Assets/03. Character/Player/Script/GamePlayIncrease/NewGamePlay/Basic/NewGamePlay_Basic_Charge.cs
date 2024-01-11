using UnityEngine;

public class NewGamePlay_Basic_Charge : MonoBehaviour
{
    private ControllerInput _input;
    protected NewGamePlay_Combo combo;

    protected bool canUseCombo;
    private bool isCharge;
    private bool isButton;
    private ParticleSystem chargeParticle;

    protected float chargeTimer;
    public virtual void Start()
    {
        _input = GameManager.singleton.Player.GetComponent<ControllerInput>();
        chargeParticle = GameManager.singleton.VFX_List.VFX_Charge;
        combo = GetComponent<NewGamePlay_Combo>();
    }
    protected virtual void Update()
    {
        ChargePower();
        ChargeTimer();
    }
    
    private void ChargePower()
    {
        if(_input.RT && !isButton)
        {
            SetIsButton(true);

            ComboCheck();

            if(canUseCombo)
            {
                ComboSkill();
            }
            else
            {
                SetIsCharge(true);
                ChargeStart();
            }
        }

        if(!_input.RT)
        {
            if(isCharge)
            {
                SetIsCharge(false);
                ChargeStop();
            }
            if(isButton)
            {
                SetIsButton(false);
            }
        }
    }
    protected virtual void ChargeStart()
    {
        chargeParticle.Play();
        chargeTimer = 0;
    }
    protected virtual void ChargeStop()
    {
        chargeParticle.Stop();
    }
    protected virtual void ComboSkill() { }
    protected virtual void ComboCheck() { }
    private void ChargeTimer()
    {
        if(isCharge)
        {
            chargeTimer += Time.deltaTime;
        }
    }
    private void SetIsCharge(bool value)
    {
        isCharge = value;
    }
    private void SetIsButton(bool value)
    {
        isButton = value;
    }
    protected void SetCanUseShotToContinueCombo(bool value)
    {
        canUseCombo = value;
    }
}
