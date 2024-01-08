using UnityEngine;

public class NewGamePlay_Basic_Wind : MonoBehaviour
{
    [Header("Power")]
    public bool isWind;
    [SerializeField] protected int _MaxWindPower = 9;
    public int WindPower;

    [Header("Basic")]
    [SerializeField] protected float WindTime = 3;
    protected int _powerParticle = 5;
    protected float _lifeTime = 0.5f;
    protected float _MaxWindlifeTime = 1f;

    //VFX
    protected ParticleSystem WindVFX;
    protected ParticleSystem.EmissionModule emissionModule;
    protected ParticleSystem.MainModule mainModule;

    //variable
    private float timer;

    protected virtual void Start()
    {
        WindVFX = GameManager.singleton.VFX_List.VFX_WindPower;
        emissionModule = WindVFX.emission;
        mainModule = WindVFX.main;

        InitializeWindPower();
    }
    protected virtual void Update()
    {
        WindTimer();
    }
    public void AddWindPower()
    {
        if(!isWind)
        {
            WindPower++;

            SetEmmision(WindPower * _powerParticle);

            if (WindPower >= _MaxWindPower)
            {
                WindStar();
            }
        }
    }
    protected virtual void InitializeWindPower() { }
    protected virtual void WindStar() { SetIsWind(true); }
    protected virtual void WindStop() { SetIsWind(false); }
    private void WindTimer()
    {
        if(isWind)
        {
            timer += Time.deltaTime;
        }

        if(timer > WindTime)
        {
            WindStop();
            timer = 0;
        }
    }
    private void SetIsWind(bool isWind)
    {
        this.isWind = isWind;
    }
    protected void SetEmmision(float rate)
    {
          emissionModule.rateOverTime = rate;
    }
    protected void SetLifeTime(float lifeTime)
    {
        mainModule.startLifetime = lifeTime;
    }
    protected void SetWinPower(int power)
    {
        WindPower = power;
    }
}
