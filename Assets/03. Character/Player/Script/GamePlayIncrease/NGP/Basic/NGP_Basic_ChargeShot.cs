using UnityEngine;
using System.Threading.Tasks;

public class NGP_Basic_ChargeShot : NGP_Basic_Charge
{
    [Header("Scatter Shot")]
    [SerializeField] private float scatterShotAngle;

    [Header("Triple Shot")]
    [SerializeField] private float tripleShotIntervalTime;
    [SerializeField] private Vector3 positionOffset_min;
    [SerializeField] private Vector3 positionOffset_max;

    //Script
    protected NGP_Shot shot;
    protected NGP_SkillState skillState;
    protected NGP_Combo combo;

    protected override void Start()
    {
        base.Start();
        //Script
        skillState = GetComponent<NGP_SkillState>();
        shot = GetComponent<NGP_Shot>();
        combo = GetComponent<NGP_Combo>();

        chargeType = ChargeType.Shot;
    }
    protected override void Update() { base.Update(); }
    protected override void ChargePower(int power)
    {
        base.ChargePower(power);

        ChargeShot(power);
    }
    protected virtual void ChargeShot(int power)
    {
        if (skillState != null)
        {
            switch (skillType())
            {
                case NGP_SkillState.SkillState.None:
                    ChargeShot_Normal(power);
                    break;
                case NGP_SkillState.SkillState.Wind:
                    ChargeShot_Wind(power);
                    break;
                case NGP_SkillState.SkillState.Fire:
                    ChargeShot_Fire(power);
                    break;
            }
        }
    }
    private NGP_SkillState.SkillState skillType() { return skillState.State; }
    protected virtual void ChargeShot_Normal(int power) { }
    protected virtual void ChargeShot_Wind(int power) { }
    protected virtual void ChargeShot_Fire(int power) { }
    protected override bool isCombo() { return false; }
    protected override void Combo() { }
    protected async void NormalShot(int count)
    {
        for (int i = 0; i < count; i++)
        {
            shot.Shot();
            await Task.Delay((int)(tripleShotIntervalTime * 1000));
        }
    }
    protected async void TripleShot(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(positionOffset_min.x, positionOffset_max.x);
            float y = Random.Range(positionOffset_min.y, positionOffset_max.y);
            float z = Random.Range(positionOffset_min.z, positionOffset_max.z);
            Vector3 positionOffset = new Vector3(x, y, z);

            shot.Shot(positionOffset, 0, 0, getShotType());
            await Task.Delay((int)(tripleShotIntervalTime * 1000));
        }
    }
    protected void scatterShot(int count)
    {
        if (count % 2 == 0)
        {
            //even
            float angle = scatterShotAngle / 2;
            for (int i = 0; i < count / 2; i++)
            {
                shot.Shot(angle * (i + 1), getShotType());
                shot.Shot(-angle * (i + 1), getShotType());
            }
        }
        else
        {
            //odd
            shot.Shot(0, getShotType());

            for (int i = 0; i < (count - 1) / 2; i++)
            {
                shot.Shot(scatterShotAngle * (i + 1), getShotType());
                shot.Shot(-scatterShotAngle * (i + 1), getShotType());
            }
        }
    }
    private NGP_Shot.ShotType getShotType()
    {
        return shot.shotType;
    }
}
