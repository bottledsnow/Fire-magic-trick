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
    public delegate void TripleShotHandler();
    public delegate void FireCardHandler();
    public delegate void BoomCardHandler();
    public delegate void WindCardHandler();
    public event ChargeShotHandler OnUseMaxShot;
    public event ChargeShotHandler OnUseMinShot;
    public event ChargeShotHandler OnUseScatterShotCombo;
    public event TripleShotHandler OnUseTripleShotCombo;
    public event FireCardHandler OnUseFireCard;
    public event BoomCardHandler OnUseBoomCard;
    public event WindCardHandler OnUseWindCard;


    [Header("Charge shot")]
    [SerializeField] private int MaxShotCount;
    [Header("Scatter Shot")]
    [SerializeField] private float scatterShotAngle;

    [Header("Triple Shot")]
    [SerializeField] private float tripleShotIntervalTime;
    [SerializeField] private Vector3 positionOffset_min;
    [SerializeField] private Vector3 positionOffset_max;

    private bool isShotCombo;
    public enum ShotType
    {
        ScatterShot,
        TripleShot,
    }
    public ShotType shotType;

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
    protected override void ComboCheck()
    {
        base.ComboCheck();

        if(combo.CanUseShotToContinueCombo())
        {
            SetCanUseShotToContinueCombo(true);
        }else
        {
            SetCanUseShotToContinueCombo(false);
        }
    }
    protected override void ChargeStart()
    {
        base.ChargeStart();
        combo.ComboChargeStart();
    }
    protected override void ChargeStop()
    {
        base.ChargeStop();
        combo.ComboChargeStop(); //for Shot Combo

        if (chargeTimer + 1 > MaxShotCount)
        {
            if (!playerState.isGround)
            {
                ToHover();
            }

            chargeShot(MaxShotCount);
            OnUseMaxShot?.Invoke();
        }
        else if (chargeTimer > 1f)
        {
            chargeShot((int)chargeTimer + 1);
            OnUseMinShot?.Invoke();
        }
        else
        {
            shot.Normal_Shot();
        }
    }
    protected override void ComboSkill()
    {
        base.ComboSkill(); //Can Use Shot To Continue Combo.

        if(!isShotCombo)
        {
            if (!playerState.isGround)
            {
                ToHover();
            }

            if (combo.comboShotType == NewGamePlay_Combo.ComboShotType.ScatterShot)
            {
                ComboShot(NewGamePlay_Combo.ComboShotType.ScatterShot);
            }
            else if(combo.comboShotType == NewGamePlay_Combo.ComboShotType.TripleShot)
            {
                ComboShot(NewGamePlay_Combo.ComboShotType.TripleShot);
            }
            else if(combo.comboShotType == NewGamePlay_Combo.ComboShotType.FireCard)
            {
                ComboShot(NewGamePlay_Combo.ComboShotType.FireCard);
            }
            else if(combo.comboShotType == NewGamePlay_Combo.ComboShotType.FireCard_Fast)
            {
                ComboShot(NewGamePlay_Combo.ComboShotType.FireCard_Fast);
            }else if(combo.comboShotType == NewGamePlay_Combo.ComboShotType.WindCard)
            {
                ComboShot(NewGamePlay_Combo.ComboShotType.WindCard);
            }
        }
    }
    private void ComboShot(NewGamePlay_Combo.ComboShotType comboShotType)
    {
        switch (comboShotType)
        {
            case NewGamePlay_Combo.ComboShotType.ScatterShot:
                shotType = ShotType.ScatterShot;
                chargeShot(MaxShotCount);
                SetIsShotCombo(true);
                shotType = ShotType.TripleShot;
                OnUseMaxShot?.Invoke();
                OnUseScatterShotCombo?.Invoke();
                break;

            case NewGamePlay_Combo.ComboShotType.TripleShot:
                shotType = ShotType.TripleShot;
                chargeShot(MaxShotCount);
                SetIsShotCombo(true);
                shotType = ShotType.TripleShot;
                OnUseMaxShot?.Invoke();
                OnUseTripleShotCombo?.Invoke();
                break;

            case NewGamePlay_Combo.ComboShotType.FireCard:
                shot.Shot(0,NewGamePlay_Shot.ShotType.Fire);
                SetIsShotCombo(true);
                OnUseFireCard?.Invoke();
                break;

            case NewGamePlay_Combo.ComboShotType.FireCard_Fast:
                shot.Shot(0,NewGamePlay_Shot.ShotType.FireFast);
                SetIsShotCombo(true);
                OnUseFireCard?.Invoke();
                break;
            case NewGamePlay_Combo.ComboShotType.WindCard:
                TripleShot(MaxShotCount);
                SetIsShotCombo(true);
                OnUseWindCard?.Invoke();
                OnUseMaxShot?.Invoke();
                OnUseTripleShotCombo?.Invoke();
                break;
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
            float x = Random.Range(positionOffset_min.x, positionOffset_max.x);
            float y = Random.Range(positionOffset_min.y, positionOffset_max.y);
            float z = Random.Range(positionOffset_min.z, positionOffset_max.z);
            Vector3 positionOffset = new Vector3(x, y, z);

            shot.Shot(positionOffset,0,0,NewGamePlay_Shot.ShotType.Wind);
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
