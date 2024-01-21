using UnityEngine;

public class NGP_SuperJump : NGP_Basic_SuperJump
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override bool canUseSuperJump()
    {
        if(skillPower.isFireMax || skillPower.isWindMax)
        {
            return true;
        }else
        {
            return false;
        }
    }
    protected override void SuperJump_wind()
    {
        jump.SuperJump(SuperJumpHeight);
    }
    protected override void SuperJump_fire()
    {
        jump.SuperJump(SuperJumpHeight);
    }
    
}
