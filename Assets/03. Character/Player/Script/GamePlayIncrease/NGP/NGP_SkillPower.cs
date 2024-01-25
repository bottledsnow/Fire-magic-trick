using TMPro;
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

    [Header("Debug UI")]
    [SerializeField] private TextMeshProUGUI firePower_UI;
    [SerializeField] private TextMeshProUGUI windPower_UI; 
    //VFX
    private ParticleSystem VFX_WindStart;
    private ParticleSystem VFX_FireStart;
    private ParticleSystem VFX_UI_Wind;
    private ParticleSystem VFX_UI_Fire;
    private ParticleSystem VFX_UI_State_Fire;
    private ParticleSystem VFX_UI_State_Wind;

    private ParticleSystem.MainModule VFX_main_StateFire;
    private ParticleSystem.MainModule VFX_main_StateWind;

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
        VFX_UI_State_Fire = GameManager.singleton.VFX_List.VFX_UI_State_Fire;
        VFX_main_StateFire = VFX_UI_State_Fire.main;
        VFX_UI_State_Wind = GameManager.singleton.VFX_List.VFX_UI_State_Wind;
        VFX_main_StateWind = VFX_UI_State_Wind.main;
    }
    protected override void Update() { base.Update(); }
    public override void AddWindPower()
    {
        base.AddWindPower();
        UpdateWindUI();
    }
    public override void AddFirePower()
    {
        base.AddFirePower();
        UpdateFireUI();
    }
    public void UseWind()
    {
        //vfx
        VFX_UI_Wind.Stop();

        //Set
        SetWinPower(0);
        SetIsWindMax(false);
        SetEmmision_wind(WindPower * _powerParticle);
        SetLifeTime_wind(_lifeTime);

        //UI
        UpdateWindUI();
    }
    public void UseFire()
    {
        //vfx
        VFX_UI_Fire.Stop();

        //Set
        SetFirePower(0);
        SetIsFireMax(false);
        SetEmmision_fire(FirePower * _powerParticle);
        SetLifeTime_fire(_lifeTime);

        //UI
        UpdateFireUI();
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
        chargeSkill.PowerMax(NGP_ChargeSkill.FireOrWind.Wind);

        //vfx
        VFX_WindStart.Clear();
        VFX_WindStart.Play();
        VFX_UI_Wind.Clear();
        VFX_UI_Wind.Play();

        //Set
        SetEmmision_wind(WindPower * _powerParticle * multiple_wind);
        SetLifeTime_wind(_lifeTime * multiple_wind);
    }
    protected override void WindStop()
    {
        base.WindStop();
    }
    protected override void FireStar()
    {
        base.FireStar();

        //Initialize
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

        
    }
    private void UpdateWindUI()
    {
        windPower_UI.text = WindPower.ToString() + "/6";
        UI.ToUpdateWind(WindPower);

        if (WindPower >= 3)
        {
            UI.ToRe();
        }
        else
        {
            UI.ToUp();
        }
    }
    private void UpdateFireUI()
    {
        firePower_UI.text = FirePower.ToString() + "/6";
        UI.ToUpdateFire(FirePower);

        if (FirePower >= 3)
        {
            UI.ToRe();
        }
        else
        {
            UI.ToUp();
        }

    }
}
