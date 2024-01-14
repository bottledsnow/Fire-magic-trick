using UnityEngine;

public class NGP_SkillPower : NGP_Basic_SkillPower
{
    
    [Header("Wind")]
    [SerializeField] private int powerParticle_wind = 5;
    [SerializeField] private float lifeTime_wind = 0.5f;
    [SerializeField] private float multiple_wind = 2;

    [Header("Fire")]
    [SerializeField] private int powerParticle_fire = 5;
    [SerializeField] private float lifeTime_fire = 0.5f;
    [SerializeField] private float multiple_fire = 2;

    //VFX
    private ParticleSystem VFX_WindStart;
    private ParticleSystem VFX_FireStart;
    private ParticleSystem VFX_UI_Wind;
    private ParticleSystem VFX_UI_Fire;

    //variable
    public bool IsMax
    {
        get { return isMax; }
    }
    private bool isMax;


    protected override void Start()
    {
        base.Start();

        //vfx
        VFX_WindStart = GameManager.singleton.VFX_List.VFX_WindMaxStart;
        VFX_FireStart = GameManager.singleton.VFX_List.VFX_FireMaxStart;
        VFX_UI_Wind = GameManager.singleton.VFX_List.VFX_UI_Wind;
        VFX_UI_Fire = GameManager.singleton.VFX_List.VFX_UI_Fire;
    }
    protected override void Update() { base.Update(); }
    protected override bool IsFire()
    {
        if(skillState.State == NGP_SkillState.SkillState.Fire)
        {
            return true;
        }else
        {
            return false;
        }
    }
    protected override bool IsWind()
    {
        if (skillState.State == NGP_SkillState.SkillState.Wind)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void AddWindPower()
    {
        if(!IsMax)
        {
            base.AddWindPower();
            if (FirePower > 0) InitializeFirePower();
        }
    }
    public override void AddFirePower()
    {
        if(!IsMax)
        {
            base.AddFirePower();
            if (FirePower > 0) InitializeWindPower();
        }
        
    }
    public void UseWind()
    {
        WindStop();
        setIsMax(false);
    }
    public void UseFire()
    {
        FireStop();
        setIsMax(false);
    }
    protected override void InitializeWindPower()
    {
        WindPower = 0;
        _powerParticle = powerParticle_wind;
        _lifeTime = lifeTime_wind;
    }
    protected override void InitializeFirePower()
    {
        FirePower = 0;
        _powerParticle = powerParticle_fire;
        _lifeTime = lifeTime_fire;
    }
    protected override void WindStar()
    {
        base.WindStar();

        //Initialize
        setIsMax(true);
        InitializeWindPower();
        InitializeFirePower();
        chargeSkill.PowerMax(NGP_ChargeSkill.FireOrWind.Wind);

        //vfx
        VFX_WindStart.Play();
        VFX_UI_Wind.Play();

        //Set
        SetEmmision_wind(WindPower * _powerParticle * multiple_wind);
        SetLifeTime_wind(_lifeTime * multiple_wind);
    }
    protected override void WindStop()
    {
        base.WindStop();

        //vfx
        VFX_UI_Wind.Stop();

        //Set
        SetWinPower(0);
        SetEmmision_wind(WindPower * _powerParticle);
        SetLifeTime_wind(_lifeTime);
    }
    protected override void FireStar()
    {
        base.FireStar();

        //Initialize
        setIsMax(true);
        InitializeWindPower();
        InitializeFirePower();
        chargeSkill.PowerMax(NGP_ChargeSkill.FireOrWind.Fire);

        //vfx
        VFX_FireStart.Play();
        VFX_UI_Fire.Play();

        //Set
        SetEmmision_fire(FirePower * _powerParticle * multiple_fire);
        SetLifeTime_fire(_lifeTime * multiple_fire);
    }
    protected override void FireStop()
    {
        base.FireStop();

        //vfx
        VFX_UI_Fire.Stop();

        //Set
        SetFirePower(0);
        SetEmmision_fire(FirePower * _powerParticle);
        SetLifeTime_fire(_lifeTime);
    }
    private void setIsMax(bool isMax)
    {
        this.isMax = isMax;
    }
}
