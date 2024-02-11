using UnityEngine;

public class NGP_ChargeShot : NGP_Basic_ChargeShot
{
    protected override void Update()
    {
        base.Update();
    }
    //variable
    private int comboShotCount = 3;
    protected override void ChargeShot_Normal(int power)
    {
        shot.SetShotType(NGP_Shot.ShotType.Normal);
        if(power>0)
        {
            NormalShot(power + 1);
        }
        else
        {
            shot.Normal_Shot();
        }
        
    }
    protected override void ChargeShot_Wind(int power)
    {
        shot.SetShotType(NGP_Shot.ShotType.Wind);
        TripleShot(power + 1);
    }
    protected override void ChargeShot_Fire(int power)
    {
        shot.SetShotType(NGP_Shot.ShotType.Boom);
        scatterShot(power + 1);
    }
    protected override bool isCombo()
    { 
        return combo.CanComboShot; 
    }
    protected override void Combo()
    {
        if(combo.CanComboShot)
        {
            if(!playerState.isGround) hover.ToHover();

            if(combo.CanSuperDashShot)
            {
                if(skillState.State == NGP_SkillState.SkillState.Fire)
                {
                    shot.SetShotType(NGP_Shot.ShotType.Fire);
                }
                else if(skillState.State == NGP_SkillState.SkillState.Wind)
                {
                    shot.SetShotType(NGP_Shot.ShotType.Money); 
                }
                else if(skillState.State == NGP_SkillState.SkillState.None)
                {
                    //None.
                }
                scatterShot(1);
            }
            else
            {
                ChargeShot(comboShotCount - 1);
            }
            combo.UseComboShot();

        }
    }
}
