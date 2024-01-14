using UnityEngine;

public class NGP_ChargeSkill : NGP_Basic_ChargeSkill
{
    [Header("Wind Skill")]
    [SerializeField] private Transform spawn;
    [SerializeField] private Transform circleCard;
    [SerializeField] private Transform circleCardFloat;
    [SerializeField] private Transform circleCardBoom;

    [Header("Fire Skill")]
    [SerializeField] private Transform beacom;
    private GameObject beacomTarget;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void FireSkillStart()
    {
        base.FireSkillStart();
        beacomTarget = shot.Shot_Gameobj(beacom,0);
        Debug.Log("Fire Skill Start");
    }
    protected override void ChargeSkillFire(int power)
    {
        Debug.Log("Fire Skill power");
        
        if(beacomTarget != null)
        {
            Beacon beacon = beacomTarget.GetComponent<Beacon>();
            beacon.StopRightNow();
        }
    }
    protected override void ChargeSkillWind(int power)
    {
        switch (power)
        {
            case 0:
                Instantiate(circleCard, spawn.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(circleCardFloat, spawn.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(circleCardBoom, spawn.position, Quaternion.identity);
                break;
        }
    }
}
