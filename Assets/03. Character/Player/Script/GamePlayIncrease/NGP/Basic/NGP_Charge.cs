using UnityEngine;

public class NGP_Charge : MonoBehaviour
{
    //Script
    private ControllerInput _input;
    private ParticleSystem chargeParticle;

    //variable
    protected float chargeTimer;
    protected bool canUseCombo;
    private bool isCharge;
    private bool isButton;

    public virtual void Start()
    {
        _input = GameManager.singleton.Player.GetComponent<ControllerInput>();
        chargeParticle = GameManager.singleton.VFX_List.VFX_Charge;
    }
    protected virtual void Update()
    {
        ChargePower();
        ChargeTimer();
    }

    private void ChargePower()
    {
        if (_input.RT && !isButton)
        {
            SetIsButton(true);
            SetIsCharge(true);
            ChargeStart();
        }

        if (!_input.RT)
        {
            if (isCharge)
            {
                SetIsCharge(false);
                ChargeStop();
            }
            if (isButton)
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
    private void ChargeTimer()
    {
        if (isCharge)
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
}
