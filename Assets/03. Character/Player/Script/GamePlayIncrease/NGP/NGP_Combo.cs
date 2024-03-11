using UnityEngine;

public class NGP_Combo : NGP_Basic_Combo
{
    //vfx
    private ParticleSystem VFX_ComboDashCooling;

    //variable
    
    protected override void Start()
    {
        base.Start();

        VFX_ComboDashCooling = GameManager.singleton.VFX_List.VFX_ComboDashCooling;
    }
    protected override void Update()
    {
        base.Update();
    }
    
    public override void UseDash()
    {
        base.UseDash();
    }
    public override void UseMaxChargeShot()
    {
        base.UseMaxChargeShot();
    }
    public override void UseSuperDashKick()
    {
        base.UseSuperDashKick();
    }
}
