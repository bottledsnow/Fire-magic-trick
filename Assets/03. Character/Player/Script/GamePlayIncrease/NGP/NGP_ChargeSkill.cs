using UnityEngine;

public class NGP_ChargeSkill : NGP_Basic_ChargeSkill
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void ChargeSkillFire(int power)
    {
        Debug.Log("Fire Skill power");
    }
    protected override void ChargeSkillWind(int power)
    {
        Debug.Log("Wind Skill power");
    }
}
