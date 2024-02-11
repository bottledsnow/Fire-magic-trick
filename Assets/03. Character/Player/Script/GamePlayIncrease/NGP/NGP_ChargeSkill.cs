using UnityEngine;
using System.Collections;

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
    public override int GetChargeCount()
    {
        return base.GetChargeCount();
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
            skillPower.UseFire();
        }
    }
    protected override void ChargeSkillWind(int power)
    {
        switch (power)
        {
            case 0:
                StartCoroutine(SpawnObjectWithDelay(circleCard,0));
                break;
            case 1:
                StartCoroutine(SpawnObjectWithDelay(circleCardFloat, 0));
                StartCoroutine(SpawnObjectWithDelay(circleCardFloat, 0.25f));
                break;
            case 2:
                StartCoroutine(SpawnObjectWithDelay(circleCardBoom, 0));
                StartCoroutine(SpawnObjectWithDelay(circleCardBoom, 0.25f));
                StartCoroutine(SpawnObjectWithDelay(circleCardBoom, 0.50f));
                break;
        }
    }
    IEnumerator SpawnObjectWithDelay(Transform objectToSpawn,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Instantiate(objectToSpawn, spawn.position, Quaternion.identity);
    }
}
