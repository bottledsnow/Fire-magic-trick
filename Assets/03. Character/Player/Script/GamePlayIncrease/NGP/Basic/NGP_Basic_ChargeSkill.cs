using UniGLTF;
using UnityEngine;

public class NGP_Basic_ChargeSkill : NGP_Basic_Charge
{
    //Script
    protected NGP_SkillPower skillPower;
    //VFX
    private ParticleSystem VFX_Charge_Wind;
    private ParticleSystem VFX_Charge_Fire;
    private ParticleSystem VFX_ChargeFinish_Wind;
    private ParticleSystem VFX_ChargeFinish_Fire;

    //type
    public enum FireOrWind
    {
        Wind,
        Fire
    }
    public FireOrWind fireOrWind;

    //variable
    private bool canSkill;
    protected override void Start()
    {
        base.Start();

        //Script
        skillPower = GameManager.singleton.NewGamePlay.GetComponent<NGP_SkillPower>();

        //Initialize
        chargeType = ChargeType.Skill;

        //Script
        VFX_Charge_Wind = GameManager.singleton.VFX_List.VFX_Charge_Wind;
        VFX_Charge_Fire = GameManager.singleton.VFX_List.VFX_Charge_Fire;
        VFX_ChargeFinish_Wind = GameManager.singleton.VFX_List.VFX_ChargeFinish_Wind;
        VFX_ChargeFinish_Fire = GameManager.singleton.VFX_List.VFX_ChargeFinish_Fire;

        //test
        VFX_Charge = VFX_Charge_Wind;
        VFX_ChargeFinish = VFX_ChargeFinish_Wind;
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void ChargePower()
    {
        if(canSkill)
        {
            base.ChargePower();
        }
    }
    protected override void ChargePower(int power)
    {
        if(canSkill)
        {
            setCanSkill(false);
            if (fireOrWind == FireOrWind.Wind)
            {
                ChargeSkillWind(power);
                skillPower.UseWind();
            }
            else
            {
                ChargeSkillFire(power);
                skillPower.UseFire();
            }
        }
        Debug.Log("ChargePower : " + power);
    }
    protected virtual void ChargeSkillWind(int power) { }
    protected virtual void ChargeSkillFire(int power) { }
    public void PowerMax(FireOrWind ForW)
    {
        this.fireOrWind = ForW;
        setCanSkill(true);

        if (fireOrWind == FireOrWind.Wind)
        {
            VFX_Charge = VFX_Charge_Wind;
            VFX_ChargeFinish = VFX_ChargeFinish_Wind;
        }
        else
        {
            VFX_Charge = VFX_Charge_Fire;
            VFX_ChargeFinish = VFX_ChargeFinish_Fire;
        }
    }
    public void setCanSkill(bool value) { canSkill = value; }
}
