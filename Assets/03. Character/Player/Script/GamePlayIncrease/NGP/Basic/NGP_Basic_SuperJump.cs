using TMPro;
using UnityEngine;

public class NGP_Basic_SuperJump : MonoBehaviour
{
    [Header("Super Jump")]
    [SerializeField] protected float SuperJumpHeight = 7f;
    [SerializeField] protected float HeavyPressureTime = 1f;
    //Script
    protected PlayerJump jump;
    protected NGP_SkillPower skillPower;
    protected ControllerInput input;
    protected PlayerState state;

    //vfx
    private ParticleSystem VFX_SuperJump_Wind;
    private ParticleSystem VFX_SuperJump_Fire;

    //variable
    private float timer;
    private bool buttonTrigger;
    private bool isHeavyPrepare;
    private bool isHeavy;
    protected virtual void Start()
    {
        skillPower = GameManager.singleton.NewGamePlay.GetComponent<NGP_SkillPower>();
        input = GameManager.singleton._input;
        jump = GameManager.singleton.Player.GetComponent<PlayerJump>();
        state = GameManager.singleton.Player.GetComponent<PlayerState>();

        //vfx
        VFX_SuperJump_Wind = GameManager.singleton.VFX_List.VFX_SuperJump_Wind;
        VFX_SuperJump_Fire = GameManager.singleton.VFX_List.VFX_SuperJump_Fire;
    }
    protected virtual void Update()
    {
        ButtonCheck();
        heavyTimer();
        HeavySystem();
    }
    private void HeavySystem()
    {
        if (isHeavy)
        {
            heavy();
        }

        if(state.isGround &&isHeavy)
        {
            setIsHeavy(false);
            heavyEnd();
        }
    }
    protected virtual void heavy() { }
    protected virtual void heavyEnd() { }
    private void heavyTimer()
    {
        if (isHeavyPrepare)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                setIsHeavyPrepare(false);
                setIsHeavy(true);
            }
        }
    }
    protected virtual void ToHeavy()
    {
        timer = HeavyPressureTime;
        setIsHeavyPrepare(true);
    }
    protected virtual bool canUseSuperJump() { return false; }
    private void ButtonCheck()
    {
        if(canUseSuperJump())
        {
            if(input.ButtonY)
            {
                if(!buttonTrigger)
                {
                    buttonTrigger = true;

                    ToHeavy();
                    if (skillPower.isWindMax)
                    {
                        jump.OnSuperJump += VFX_superJump_wind;
                        jump.OnSuperJump -= VFX_superJump_fire;

                        SuperJump_wind();
                        skillPower.UseWind();
                        Debug.Log("Wind Super Jump");
                    }
                    else if (skillPower.isFireMax)
                    {
                        jump.OnSuperJump += VFX_superJump_fire;
                        jump.OnSuperJump -= VFX_superJump_wind;

                        SuperJump_fire();
                        skillPower.UseFire();
                        Debug.Log("Fire Super Jump");
                    }
                }
                
            }
            if(!input.ButtonY)
            {
                buttonTrigger = false;
                setIsHeavyPrepare(false);
                setIsHeavy(false);
            }
        }
    }
    
    protected virtual void SuperJump_wind() { }
    protected virtual void SuperJump_fire() { }
    protected void VFX_superJump_wind()
    {
        VFX_SuperJump_Wind.Clear();
        VFX_SuperJump_Wind.Play();
    }
    protected void VFX_superJump_fire()
    {
        VFX_SuperJump_Fire.Clear();
        VFX_SuperJump_Fire.Play();
    }
    private void setIsHeavyPrepare(bool isHeavy)
    {
        isHeavyPrepare = isHeavy;
    }
    private void setIsHeavy(bool isHeavy)
    {
        this.isHeavy = isHeavy;
    }
}
