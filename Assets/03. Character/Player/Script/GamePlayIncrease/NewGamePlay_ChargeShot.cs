using System.Threading.Tasks;
using UnityEngine;

public class NewGamePlay_ChargeShot : NewGamePlay_Basic_Charge
{
    //Script
    private PlayerState playerState;
    private NewGamePlay_Shot shot;
    private NewGamePlay_Hover hover;

    //delegate
    public delegate void ChargeShotHandler();
    public delegate void FireCardHandler();
    public delegate void BoomCardHandler();
    public delegate void WindCardHandler();
    public event ChargeShotHandler OnUseMaxShot;
    public event ChargeShotHandler OnUseMinShot;
    public event FireCardHandler OnUseFireCard;
    public event BoomCardHandler OnUseBoomCard;
    public event WindCardHandler OnUseWindCard;


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
        playerState = GameManager.singleton.Player.GetComponent<PlayerState>();
        shot = GetComponent<NewGamePlay_Shot>();
        hover = GetComponent<NewGamePlay_Hover>();
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
            if (!playerState.isGround)
            {
                ToHover();
            }

            TripleShot(MaxShotCount);
            OnUseMaxShot?.Invoke();
        }
        else if (chargeTimer > 1f)
        {
            TripleShot((int)chargeTimer + 1);
            OnUseMinShot?.Invoke();
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
            if (!playerState.isGround)
            {
                ToHover();
            }

            shotType = ShotType.ScatterShot;
            chargeShot(MaxShotCount);
            SetIsShotCombo(true);
            shotType = ShotType.TripleShot;
            OnUseMaxShot?.Invoke();
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
                shot.Shot( angle* (i + 1), NewGamePlay_Shot.ShotType.Boom);
                shot.Shot(-angle* (i + 1), NewGamePlay_Shot.ShotType.Boom);
            }
        }
        else
        {
            //odd
            shot.Shot(0, NewGamePlay_Shot.ShotType.Boom);

            for (int i = 0; i < (count - 1) / 2; i++)
            {
                shot.Shot(scatterShotAngle * (i + 1), NewGamePlay_Shot.ShotType.Boom);
                shot.Shot(-scatterShotAngle * (i + 1), NewGamePlay_Shot.ShotType.Boom);
            }
        }
        OnUseBoomCard?.Invoke();
    }
    private async void TripleShot(int count)
    {
        for(int i = 0; i < count; i++)
        {
            shot.Shot(0,NewGamePlay_Shot.ShotType.Wind);
            OnUseWindCard?.Invoke();
            await Task.Delay((int)(tripleShotIntervalTime*1000));
        }
    }
    private void ToHover()
    {
        if(!playerState.isGround)
        {
            hover.ToHover();
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
