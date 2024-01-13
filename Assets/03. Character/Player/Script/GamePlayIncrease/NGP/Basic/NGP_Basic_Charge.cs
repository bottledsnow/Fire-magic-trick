using UnityEngine;

public class NGP_Basic_Charge : MonoBehaviour
{
    //Script
    private ControllerInput _input;
    protected ParticleSystem VFX_Charge;
    protected ParticleSystem VFX_ChargeFinish;

    //variable
    private float chargeTimer;
    private bool isCharge;
    private bool isButton;
    private int MaxCharge = 2;
    private int chargeCount;
    private float chargeTime = 1.0f;

    //enum
    protected enum ChargeType
    {
        Shot,
        Skill
    }
    protected ChargeType chargeType;
    protected virtual void Start()
    {
        _input = GameManager.singleton.Player.GetComponent<ControllerInput>();
        VFX_Charge = GameManager.singleton.VFX_List.VFX_Charge;
        VFX_ChargeFinish = GameManager.singleton.VFX_List.VFX_ChargeFinish;

        chargeType = ChargeType.Shot;
    }
    protected virtual void Update()
    {
        ChargePower();
        ChargeTimer();
    }
    private void ChargePower()
    {
        if(ChargeButton() && !isButton)
        {
            SetIsButton(true);
            SetIsCharge(true);
            ChargeStart();
        }

        if (!ChargeButton())
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
    private bool ChargeButton()
    {
        if(chargeType == ChargeType.Shot) return _input.RT;
        if(chargeType == ChargeType.Skill) return _input.RB;
        return false;
    }
    protected virtual void ChargePower(int power) { }
    
    private void ChargeStart()
    {
        VFX_Charge_Play();
        chargeTimer = 0;

        if(isCombo()) ChargeStopRightNow();
    }
    protected virtual bool isCombo()
    {
        return false;
    }
    private void ChargeStopRightNow()
    {
        if (isCharge)
        {
            SetIsCharge(false);
            VFX_Charge_Stop();
            chargeCount = 0;
            Combo();
        }
    }
    protected virtual void Combo() { }
    private void ChargeStop()
    {
        VFX_Charge_Stop();
        ChargePower(chargeCount);
        chargeCount = 0;
    }
    private void ChargeTimer()
    {
        if(chargeCount < MaxCharge)
        {
            if (isCharge)
            {
                chargeTimer += Time.deltaTime;
            }

            if (chargeTimer >= chargeTime)
            {
                chargeTimer = 0;
                chargeCount++;

                if (chargeCount < MaxCharge)
                {
                    VFX_Charge_Stop();
                    VFX_Charge_Play();
                    VFX_ChargeFinish.Play();
                }
                else
                {
                    VFX_Charge_Stop();
                    VFX_ChargeFinish.Play();
                }
            }
        }
    }
    private void VFX_Charge_Play()
    {
        VFX_Charge.gameObject.SetActive(true);
        VFX_Charge.Play();
    }
    private void VFX_Charge_Stop()
    {
        VFX_Charge.Stop();
        VFX_Charge.gameObject.SetActive(false);
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
