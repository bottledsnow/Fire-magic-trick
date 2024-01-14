using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class NGP_Basic_SuperJump : MonoBehaviour
{
    [Header("Super Jump")]
    [SerializeField] protected float SuperJumpHeight = 7f;

    //Script
    protected PlayerJump jump;
    protected NGP_SkillPower skillPower;
    protected ControllerInput input;

    //vfx
    private ParticleSystem VFX_SuperJump_Wind;
    private ParticleSystem VFX_SuperJump_Fire;

    protected virtual void Start()
    {
        skillPower = GameManager.singleton.NewGamePlay.GetComponent<NGP_SkillPower>();
        input = GameManager.singleton._input;
        jump = GameManager.singleton.Player.GetComponent<PlayerJump>();

        //vfx
        VFX_SuperJump_Wind = GameManager.singleton.VFX_List.VFX_SuperJump_Wind;
        VFX_SuperJump_Fire = GameManager.singleton.VFX_List.VFX_SuperJump_Fire;
    }
    protected virtual void Update()
    {
        ButtonCheck();
    }
    protected virtual bool canUseSuperJump() { return false; }
    private void ButtonCheck()
    {
        if(canUseSuperJump())
        {
            if(input.ButtonY)
            {
                if(skillPower.isWindMax)
                {
                    jump.OnSuperJump += VFX_superJump_wind;
                    jump.OnSuperJump -= VFX_superJump_fire;

                    SuperJump_wind();
                    skillPower.UseWind();
                }
                else if (skillPower.isFireMax)
                {
                    jump.OnSuperJump += VFX_superJump_fire;
                    jump.OnSuperJump -= VFX_superJump_wind;

                    SuperJump_fire();
                    skillPower.UseFire();
                }
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
}
