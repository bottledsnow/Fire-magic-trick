using UnityEngine;

public class NGP_ChargeShot : NGP_Basic_ChargeShot
{
    protected override void ChargeShot(int power)
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
}
