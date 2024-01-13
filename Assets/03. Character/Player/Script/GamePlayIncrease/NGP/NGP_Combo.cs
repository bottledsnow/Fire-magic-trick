using UnityEngine;

public class NGP_Combo : NGP_Basic_Combo
{
    private ParticleSystem VFX_ComboDashCooling;

    protected override void Start()
    {
        base.Start();

        VFX_ComboDashCooling = GameManager.singleton.VFX_List.VFX_ComboDashCooling;
    }
    protected override void Update()
    {
        base.Update();

    }
}
