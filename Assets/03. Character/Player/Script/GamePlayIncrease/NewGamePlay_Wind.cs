using UnityEngine;

public class NewGamePlay_Wind : NewGamePlay_Basic_Wind
{
    [Header("Wind")]
    [SerializeField] private int powerParticle = 5;
    [SerializeField] private float lifeTime = 0.5f;
    [SerializeField] private float multiple = 2;

    //delegate 
    public delegate void WindPowerDelegateHandler();
    public event WindPowerDelegateHandler OnWind;
    public event WindPowerDelegateHandler OnWindStop;

    //VFX
    private ParticleSystem VFX_WindStart;


    protected override void Start()
    {
        base.Start();

        VFX_WindStart = GameManager.singleton.VFX_List.VFX_WindStart;
    }
    protected override void Update()
    {
        base.Update();
    }
    public void UseWind()
    {
        WindStop();
    }
    protected override void InitializeWindPower()
    {
        base.InitializeWindPower();

        _powerParticle = powerParticle; 
        _lifeTime = lifeTime;
    }

    
    protected override void WindStar()
    {
        base.WindStar();

        //event
        OnWind?.Invoke();

        //vfx
        VFX_WindStart.Play();

        //Set
        SetEmmision(WindPower * _powerParticle * multiple);
        SetLifeTime(_lifeTime * multiple);
    }
    protected override void WindStop()
    {
        base.WindStop();

        //event
        OnWindStop?.Invoke();

        //Set
        SetWinPower(0);
        SetEmmision(WindPower * _powerParticle);
        SetLifeTime(_lifeTime);
    }
}
