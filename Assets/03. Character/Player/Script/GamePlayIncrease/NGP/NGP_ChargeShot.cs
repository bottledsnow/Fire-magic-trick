using UnityEngine;

public class NGP_ChargeShot : NGP_Basic_ChargeShot
{
    //variable
    private int comboShotCount = 3;
    protected override void ChargeShot_Normal(int power)
    {
        NormalShot(power + 1);
        shot.SetShotType(NGP_Shot.ShotType.Normal);
    }
    protected override void ChargeShot_Wind(int power)
    {
        TripleShot(power + 1);
        shot.SetShotType(NGP_Shot.ShotType.Wind);
    }
    protected override void ChargeShot_Fire(int power)
    {
        scatterShot(power + 1);
        shot.SetShotType(NGP_Shot.ShotType.Boom);
    }
    protected override bool isCombo()
    { 
        return combo.CanComboShot; 
    }
    protected override void Combo()
    {
        if(combo.CanComboShot)
        {
            ChargeShot(comboShotCount-1);
            combo.UseComboShot();
        }
    }
}
