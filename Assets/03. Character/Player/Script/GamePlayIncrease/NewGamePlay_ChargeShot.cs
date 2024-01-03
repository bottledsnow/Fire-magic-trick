using System.Threading.Tasks;
using UnityEngine;

public class NewGamePlay_ChargeShot : NewGamePlay_Basic_Charge
{
    public delegate void ComboShotHandler();
    public event ComboShotHandler OnUseMaxShot;

    private NewGamePlay_Shot shot;

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

        if (chargeTimer + 1 > MaxShotCount)
        {
            chargeShot(MaxShotCount);
            OnUseMaxShot?.Invoke();
        }
        else if (chargeTimer > 1f)
        {
            chargeShot((int)chargeTimer + 1);
        }
        else
        {
            shot.Normal_Shot();
        }
    }
    protected override void ComboSkill()
    {
        base.ComboSkill();

        if(!isShotCombo)
        {
            shotType = ShotType.TripleShot;
            chargeShot(MaxShotCount);
            SetIsShotCombo(true);
            shotType = ShotType.ScatterShot;
            OnUseMaxShot?.Invoke();

            Debug.Log("Shot Combo");
        }
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
