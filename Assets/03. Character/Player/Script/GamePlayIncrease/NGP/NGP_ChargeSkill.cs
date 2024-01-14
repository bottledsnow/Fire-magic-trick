using UnityEngine;

public class NGP_ChargeSkill : NGP_Basic_ChargeSkill
{
    [Header("Wind Skill")]
    [SerializeField] private Transform spawn;
    [SerializeField] private Transform circleCard;
    [SerializeField] private Transform circleCardFloat;
    [SerializeField] private Transform circleCardBoom;
    [SerializeField] private Transform cardPlatform;
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
