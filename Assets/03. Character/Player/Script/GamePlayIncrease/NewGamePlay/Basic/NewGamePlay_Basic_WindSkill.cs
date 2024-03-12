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
    }
    protected override void ChargeStart()
    {
        base.ChargeStart();
    }
    protected override void ChargeStop()
    {
        base.ChargeStop();

        if(!canUseWindSkill)
        {
            useWindCheck();
        }

        if (canUseWindSkill)
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
    protected virtual void SuperJump() { useWindSkill(); }
    protected virtual void CircleCard() { useWindSkill(); }
    protected virtual void CircleCardFloat() { useWindSkill(); }
    protected virtual void CircleCardBoom() { useWindSkill(); }
    
    private void useWindCheck()
    {
        if (wind.isWind)
        {
            wind.UseWind();
            SetCanUseWindSkill(true);
        }
    }
    private void useWindSkill()
    {
        if(canUseWindSkill)
        {
            SetCanUseWindSkill(false);
        }
    }
    private void SetCanUseWindSkill(bool value)
    {
        canUseWindSkill = value;
    }
}
