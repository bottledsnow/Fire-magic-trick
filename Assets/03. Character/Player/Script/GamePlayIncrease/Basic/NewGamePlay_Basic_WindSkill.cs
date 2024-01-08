using UnityEngine;

public class NewGamePlay_Basic_WindSkill : NewGamePlay_Basic_Charge
{
    //Script
    private NewGamePlay_Wind wind;
    private ControllerInput input;

    //variable
    private bool canUseWindSkill = false;

    public override void Start()
    {
        base.Start();
        wind = GetComponent<NewGamePlay_Wind>();
        input = GameManager.singleton._input;
    }
    protected override void Update()
    {
        base.Update();

        if(wind.isWind)
        {
            if(input.ButtonA || input.ButtonY)
            {
                SuperJump();
                wind.UseWind();
            }
        }
    }
    protected override void ChargeStart()
    {
        base.ChargeStart();

        if(wind.isWind)
        {
            wind.UseWind();
            SetCanUseWindSkill(true);
        }
    }
    protected override void ChargeStop()
    {
        base.ChargeStop();

        if(canUseWindSkill)
        {
            if (chargeTimer + 1 > 3)
            {
                CircleCardBoom();
            }
            else if (chargeTimer > 1f)
            {
                CircleCardFloat();
            }
            else
            {
                CircleCard();
            }
            SetCanUseWindSkill(false);
        }
    }
    protected virtual void SuperJump() { }
    protected virtual void CircleCard() { }
    protected virtual void CircleCardFloat() { }
    protected virtual void CircleCardBoom() { }
    
    private void SetCanUseWindSkill(bool value)
    {
        canUseWindSkill = value;
    }
}
