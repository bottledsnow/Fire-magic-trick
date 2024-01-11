using UnityEngine;

public class NGP_ChargeSkill : NGP_Basic_Charge
{
    //VFX
    private ParticleSystem VFX_Charge_Wind;
    private ParticleSystem VFX_Charge_Fire;
    private ParticleSystem VFX_ChargeFinish_Wind;
    private ParticleSystem VFX_ChargeFinish_Fire;

    protected override void Start()
    {
        base.Start();
        chargeType = ChargeType.Skill;

        VFX_Charge_Wind = GameManager.singleton.VFX_List.VFX_Charge_Wind;
        VFX_Charge_Fire = GameManager.singleton.VFX_List.VFX_Charge_Fire;
        VFX_ChargeFinish_Wind = GameManager.singleton.VFX_List.VFX_ChargeFinish_Wind;
        VFX_ChargeFinish_Fire = GameManager.singleton.VFX_List.VFX_ChargeFinish_Fire;

        VFX_Charge = VFX_Charge_Wind;
        VFX_ChargeFinish = VFX_ChargeFinish_Wind;
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void ChargePower(int power)
    {
        base.ChargePower(power);
        Debug.Log("ChargePower : " + power);
    }
}
