using UnityEngine;

public class NGP_Basic_SkillState : MonoBehaviour
{
    [Header("Skill State")]
    public SkillState State;

    //variable
    protected float stateKeepTime = 5f;
    protected float orbitalSpeed = 5f;

    //Script 
    private NGP_Dash dash;

    //skill state
    private bool isSkill;
    private float timer;

    //VFX
    private ParticleSystem VFX_UseSkill_Fire;
    private ParticleSystem.VelocityOverLifetimeModule orbital_fire;
    private ParticleSystem VFX_UseSkill_Wind;
    private ParticleSystem.VelocityOverLifetimeModule orbital_wind;
    private ParticleSystem VFX_Foot_L;
    private ParticleSystem VFX_Foot_R;
    private ParticleSystem VFX_WindState;
    public enum SkillState
    {
        None,
        Wind,
        Fire,
    }
    protected virtual void Start()
    {
        //Script
        dash = GetComponent<NGP_Dash>();

        //VFX
        VFX_UseSkill_Wind = GameManager.singleton.VFX_List.VFX_UseSkill_Wind;
        orbital_wind = VFX_UseSkill_Wind.velocityOverLifetime;
        VFX_UseSkill_Fire = GameManager.singleton.VFX_List.VFX_UseSkill_Fire;
        orbital_fire = VFX_UseSkill_Fire.velocityOverLifetime;
        VFX_Foot_L = GameManager.singleton.VFX_List.VFX_Foot_L;
        VFX_Foot_R = GameManager.singleton.VFX_List.VFX_Foot_R;
        VFX_WindState = GameManager.singleton.VFX_List.VFX_WindState;

        //Initialize
        OnDashNone();

        //Subscribe
        dash.OnDashForward += OnDashForward;
        dash.OnDashBackward += OnDashBackward;
    }
    protected virtual void Update()
    {
        skillTimer();
    }
    public bool GetIsSkill() { return isSkill; }
    private void skillTimer()
    {
        if(isSkill)
        {
            timer -= Time.deltaTime;
        }
        if(timer < 0)
        {
            stateTimerStop();
        }
    }
    private void OnDashNone()
    {
        setSkillState(SkillState.None);
        setIsSkill(false);
        timer = 0;

        //vfx
        VFX_ToNone();
        orbital_wind.orbitalZ = -orbitalSpeed;
    }
    private void OnDashForward()
    {
        setSkillState(SkillState.Fire);
        setIsSkill(true);
        timer = stateKeepTime;

        //vfx
        VFX_ToFire();
        orbital_fire.orbitalZ = -orbitalSpeed;
    }
    private void OnDashBackward()
    {
        setSkillState(SkillState.Wind);
        setIsSkill(true);
        timer = stateKeepTime;

        //vfx
        VFX_ToWind();
        orbital_wind.orbitalZ = -orbitalSpeed;
    }
    private void setSkillState(SkillState state)
    {
        State = state;
    }
    protected virtual void stateTimerStop() 
    {
        timer = 0;
        setIsSkill(false);
        setSkillState(SkillState.None);
        VFX_UseSkill_Fire.gameObject.SetActive(false);
        VFX_UseSkill_Wind.gameObject.SetActive(false);
    }
    private void setIsSkill(bool value)
    {
        isSkill = value;
    }
    private void VFX_ToWind()
    {
        VFX_Foot_L.Stop();
        VFX_Foot_L.Clear();
        VFX_Foot_R.Stop();
        VFX_Foot_R.Clear();
        VFX_WindState.Clear();
        VFX_WindState.Play();
        VFX_UseSkill_Wind.gameObject.SetActive(true);
        VFX_UseSkill_Wind.Clear();
        VFX_UseSkill_Wind.Play();
        VFX_UseSkill_Fire.gameObject.SetActive(false);
    }
    private void VFX_ToFire()
    {
        VFX_Foot_L.Play();
        VFX_Foot_R.Play();
        VFX_WindState.Stop();
        VFX_WindState.Clear();
        VFX_UseSkill_Wind.gameObject.SetActive(false);
        VFX_UseSkill_Fire.gameObject.SetActive(true);
        VFX_UseSkill_Fire.Clear();
        VFX_UseSkill_Fire.Play();
    }
    private void VFX_ToNone()
    {
        VFX_Foot_L.Stop();
        VFX_Foot_L.Clear();
        VFX_Foot_R.Stop();
        VFX_Foot_R.Clear();
        VFX_WindState.Stop();
        VFX_WindState.Clear();
        VFX_UseSkill_Wind.gameObject.SetActive(false);
        VFX_UseSkill_Fire.gameObject.SetActive(false);
    }
}
