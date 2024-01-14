using UnityEngine;

public class NGP_Basic_SkillPower : MonoBehaviour
{
    [Header("Power")]
    public bool isWindMax;
    public bool isFireMax;
    public int WindPower;
    public int FirePower;

    //Script
    protected NGP_SkillState skillState;
    protected NGP_ChargeSkill chargeSkill;

    //VFX
    protected ParticleSystem VFX_WindPower;
    protected ParticleSystem.EmissionModule emissionModule_wind;
    protected ParticleSystem.MainModule mainModule_wind;
    protected ParticleSystem VFX_FirePower;
    protected ParticleSystem.EmissionModule emissionModule_fire;
    protected ParticleSystem.MainModule mainModule_fire;

    //variable
    protected int MaxPower = 6;
    protected int _powerParticle = 5;
    protected float _lifeTime = 0.5f;
    protected float _MaxlifeTime = 1f;

    protected virtual void Start()
    {
        //Script
        VFX_WindPower = GameManager.singleton.VFX_List.VFX_WindPower;
        VFX_FirePower = GameManager.singleton.VFX_List.VFX_FirePower;
        skillState = GameManager.singleton.NewGamePlay.GetComponent<NGP_SkillState>();
        chargeSkill = GameManager.singleton.NewGamePlay.GetComponent<NGP_ChargeSkill>();

        //VFX
        emissionModule_wind = VFX_WindPower.emission;
        mainModule_wind = VFX_WindPower.main;
        emissionModule_fire = VFX_FirePower.emission;
        mainModule_fire = VFX_FirePower.main;

        //set
        InitializeWindPower();
    }
    protected virtual void Update() { }
    public virtual void AddWindPower()
    {
        if (IsWind())
        {
            WindPower++;

            SetEmmision_wind(WindPower * _powerParticle);

            if (WindPower >= MaxPower)
            {
                WindStar();
            }
        }
    }
    public virtual void AddFirePower()
    {
        if (IsFire())
        {
            FirePower++;

            SetEmmision_fire(FirePower * _powerParticle);

            if (FirePower >= MaxPower)
            {
                FireStar();
            }
        }
    }
    protected virtual bool IsWind() { return false; }
    protected virtual bool IsFire() { return false; }
    protected virtual void InitializeWindPower() { }
    protected virtual void InitializeFirePower() { }
    protected virtual void WindStar() { SetIsWindMax(true); }
    protected virtual void WindStop() { SetIsWindMax(false); }
    protected virtual void FireStar() { SetIsFireMax(true); }
    protected virtual void FireStop() { SetIsFireMax(false); }
    private void SetIsWindMax(bool isWind)
    {
        this.isWindMax = isWind;
    }
    private void SetIsFireMax(bool isFire)
    {
        this.isFireMax = isFire;
    }
    protected void SetEmmision_wind(float rate)
    {
        emissionModule_wind.rateOverTime = rate;
    }
    protected void SetLifeTime_wind(float lifeTime)
    {
        mainModule_wind.startLifetime = lifeTime;
    }
    protected void SetWinPower(int power)
    {
        WindPower = power;
    }
    protected void SetEmmision_fire(float rate)
    {
        emissionModule_fire.rateOverTime = rate;
    }
    protected void SetLifeTime_fire(float lifeTime)
    {
        mainModule_fire.startLifetime = lifeTime;
    }
    protected void SetFirePower(int power)
    {
        FirePower = power;
    }
}
