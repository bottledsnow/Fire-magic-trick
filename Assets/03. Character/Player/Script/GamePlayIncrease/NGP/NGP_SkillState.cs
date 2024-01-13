using UnityEngine;

public class NGP_SkillState : NGP_Basic_SkillState
{
    //variable
    public int WindPower
    {
        get { return windPower; }
    }
    public int FirePower
    {
        get { return firePower; }
    }
    private int windPower = 0;
    private int firePower = 0;
    protected override void Start()
    {
        base.Start();
    }

    protected override void timerStop()
    {
        base.timerStop();
    }

    protected override void Update()
    {
        base.Update();
    }
}
