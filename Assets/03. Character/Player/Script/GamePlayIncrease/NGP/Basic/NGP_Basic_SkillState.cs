using UnityEngine;

public class NGP_Basic_SkillState : MonoBehaviour
{
    //protect
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
    public enum SkillState
    {
        None,
        Wind,
        Fire,
    }
    public SkillState State;
    protected virtual void Start()
    {
        //Script
        dash = GetComponent<NGP_Dash>();

        //VFX
        VFX_UseSkill_Wind = GameManager.singleton.VFX_List.VFX_UseSkill_Wind;
        orbital_wind = VFX_UseSkill_Wind.velocityOverLifetime;
        VFX_UseSkill_Fire = GameManager.singleton.VFX_List.VFX_UseSkill_Fire;
        orbital_fire = VFX_UseSkill_Fire.velocityOverLifetime;

        //Subscribe
        dash.OnDashForward += OnDashForward;
        dash.OnDashBackward += OnDashBackward;
    }
    protected virtual void Update()
    {
        skillTimer();
    }
    private void skillTimer()
    {
        if(isSkill)
        {
            timer -= Time.deltaTime;
        }
        if(timer < 0)
        {
            timer = 0;
            timerStop();
            setIsSkill(false);
        }
    }
    private void OnDashForward()
    {
        setSkillState(SkillState.Fire);
        setIsSkill(true);
        timer = stateKeepTime;

        //vfx
        VFX_UseSkill_Wind.gameObject.SetActive(false);
        VFX_UseSkill_Fire.gameObject.SetActive(true);
        orbital_fire.orbitalZ = -orbitalSpeed;
    }
    private void OnDashBackward()
    {
        setSkillState(SkillState.Wind);
        setIsSkill(true);
        timer = stateKeepTime;

        //vfx
        VFX_UseSkill_Wind.gameObject.SetActive(true);
        VFX_UseSkill_Fire.gameObject.SetActive(false);
        orbital_wind.orbitalZ = -orbitalSpeed;
    }
    private void setSkillState(SkillState state)
    {
        State = state;
    }
    protected virtual void timerStop() 
    {
        setSkillState(SkillState.None);
        VFX_UseSkill_Fire.gameObject.SetActive(false);
        VFX_UseSkill_Wind.gameObject.SetActive(false);
    }
    private void setIsSkill(bool value)
    {
        isSkill = value;
    }
}
