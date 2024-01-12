using UnityEngine;

public class NGP_ChargeShot : NGP_Basic_Charge
{
    //shot
    private Transform bullet;

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
    private void ChargeShot(int power)
    {
        switch (power)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}
