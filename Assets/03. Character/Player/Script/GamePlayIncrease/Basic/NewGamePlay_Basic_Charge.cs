using UnityEngine;

public class NewGamePlay_Basic_Charge : MonoBehaviour
{
    private ControllerInput _input;
    protected NewGamePlay_Combo combo;

    [Header("Test")]
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
        bool canUseCombo;

        if(_input.RT && !isButton)
        {
            SetIsButton(true);
            combo.CanUseShotToCombo(out canUseCombo);

            if(canUseCombo)
            {
                ComboSkill();
                Debug.Log("Use Shot To Contunue Combo");
            }
            else
            {
                SetIsCharge(true);
                Charge();
            }
        }

        if (!_input.RT)
        {
            if(isCharge)
            {
                SetIsCharge(false);
                StopCharge();
            }
            if(isButton)
            {
                SetIsButton(false);
            }
        }
    }
    protected virtual void Charge()
    {
        chargeParticle.Play();
        chargeTimer = 0;
    }
    protected virtual void StopCharge()
    {
        chargeParticle.Stop();
    }
    protected virtual void ComboSkill() { }
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
}
