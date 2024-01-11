using UnityEngine;

public class NGP_ChargeShot : NGP_Basic_Charge
{
    protected override void Start()
    {
        base.Start();
        chargeType = ChargeType.Shot;
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
