using System.Threading.Tasks;
using UnityEngine;

public class NewGamePlay_ChargeShot : NewGamePlay_Basic_Charge
{
    private NewGamePlay_Shot shot;
    private NewGamePlay_Combo combo;

    [Header("Charge shot")]
    [SerializeField] private int MaxShotCount;
    [Header("Scatter Shot")]
    [SerializeField] private float scatterShotAngle;

    [Header("Triple Shot")]
    [SerializeField] private float tripleShotIntervalTime;

    private bool isShotCombo;
    public enum ShotType
    {
        ScatterShot,
        TripleShot,
    }
    private ShotType shotType;

    public override void Start()
    {
        base.Start();
        shot = GetComponent<NewGamePlay_Shot>();
        combo = GetComponent<NewGamePlay_Combo>();
        combo.OnUseSkill += OnUseSkill;
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void Charge()
    {
        base.Charge();
        combo.ComboChargeStart();
    }
    protected override void StopCharge()
    {
        base.StopCharge();
        combo.ComboChargeStop();

        if (chargeTimer+1> MaxShotCount)
        {
            chargeShot(MaxShotCount);
            combo.ComboTrigger();
        }
        else
        {
            chargeShot((int)chargeTimer+1);
            Debug.Log("chargeTimer : " + chargeTimer);
        }
    }
    protected override void ComboSkill()
    {
        base.ComboSkill();

        if(!isShotCombo)
        {
            shotType = ShotType.TripleShot;
            chargeShot(MaxShotCount);
            combo.ComboTrigger();
            SetIsShotCombo(true);
            shotType = ShotType.ScatterShot;
        }
        Debug.Log("combo Skill");
    }
    private void chargeShot(int count)
    {
        switch (shotType)
        {
            case ShotType.ScatterShot:
                scatterShot(count);
                break;
            case ShotType.TripleShot:
                TripleShot(count);
                break;
        }
    }
    private void scatterShot(int count)
    {
        if(count % 2 ==0)
        {
            //even
            float angle = scatterShotAngle / 2;
            for (int i = 0; i < count / 2; i++)
            {
                shot.Shot( angle* (i + 1));
                shot.Shot(-angle* (i + 1));
            }
        }
        else
        {
            //odd
            shot.Shot(0);

            for (int i = 0; i < (count - 1) / 2; i++)
            {
                shot.Shot(scatterShotAngle * (i + 1));
                shot.Shot(-scatterShotAngle * (i + 1));
            }
        }
        
    }
    private async void TripleShot(int count)
    {
        for(int i = 0; i < count; i++)
        {
            shot.Shot(0);
            await Task.Delay((int)(tripleShotIntervalTime*1000));
        }
    }
    private void SetIsShotCombo(bool value)
    {
        isShotCombo = value;
    }
    private void OnUseSkill()
    {
        SetIsShotCombo(false);
    }
}
