using MoreMountains.Feedbacks;
using UnityEngine;

public class NGP_Basic_UI : MonoBehaviour
{
    //Script
    [SerializeField] protected Animator animator_State;
    [SerializeField] protected Animator animator_UpRe;

    //Feedbacks
    protected MMF_Player UI_State;

    //VFX
    private ParticleSystem VFX_UI_State_Fire;
    private ParticleSystem VFX_UI_State_Wind;

    private ParticleSystem.MainModule VFX_main_StateFire;
    private ParticleSystem.MainModule VFX_main_StateWind;

    protected virtual void Start()
    {
        UI_State = GameManager.singleton.Feedbacks_List.GetComponent<Feedbacks_List>().UI_State;

        //VFX
        VFX_UI_State_Fire = GameManager.singleton.VFX_List.VFX_UI_State_Fire;
        VFX_main_StateFire = VFX_UI_State_Fire.main;
        VFX_UI_State_Wind = GameManager.singleton.VFX_List.VFX_UI_State_Wind;
        VFX_main_StateWind = VFX_UI_State_Wind.main;

        //Set
        VFX_main_StateFire.maxParticles = 0;
        VFX_main_StateWind.maxParticles = 0;
    }
    protected virtual void Update()
    {
        
    }
    public void ToNone()
    {
        //Animator
        animator_State.SetBool("isNone", true);
        animator_State.SetBool("isFire", false);
        animator_State.SetBool("isWind", false);

        //VFX
        VFX_UI_State_Fire.Stop();
        VFX_UI_State_Fire.Clear();
        VFX_UI_State_Wind.Stop();
        VFX_UI_State_Wind.Clear();
    }
    public void ToFire()
    {
        //Animator
        animator_State.SetBool("isNone", false);
        animator_State.SetBool("isFire", true);
        animator_State.SetBool("isWind", false);

        //Feedbacks
        UI_State.PlayFeedbacks();

        //VFX
        VFX_UI_State_Fire.Clear();
        VFX_UI_State_Wind.Clear();
        VFX_UI_State_Fire.Play();
        VFX_UI_State_Wind.Stop();
    }
    
    public void ToWind()
    {
        //Animator
        animator_State.SetBool("isNone", false);
        animator_State.SetBool("isFire", false);
        animator_State.SetBool("isWind", true);

        //Feedbacks
        UI_State.PlayFeedbacks();

        //VFX
        VFX_UI_State_Fire.Clear();
        VFX_UI_State_Wind.Clear();
        VFX_UI_State_Fire.Stop();
        VFX_UI_State_Wind.Play();
    }
    public void ToUpdateFire(int power)
    {
        VFX_UI_State_Fire.Clear();
        VFX_main_StateFire.maxParticles = power;
        VFX_UI_State_Fire.Play();
    }
    public void ToUpdateWind(int power)
    {
        VFX_UI_State_Wind.Clear();
        VFX_main_StateWind.maxParticles = power;
        VFX_UI_State_Wind.Play();
    }
    public void ToUp()
    {
        animator_UpRe.SetBool("isUp", true);
        animator_UpRe.SetBool("isRe", false);
    }
    public void ToRe()
    {
        animator_UpRe.SetBool("isUp", false);
        animator_UpRe.SetBool("isRe", true);
    }
}
