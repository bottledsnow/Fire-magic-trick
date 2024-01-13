using UnityEngine;

public class NewGamePlay_Basic_Combo : MonoBehaviour
{
    private ParticleSystem VFX_ComboCharge;
    private ParticleSystem VFX_CanCombo_Shot;
    private ParticleSystem VFX_CanCombo_Skill;
    private ParticleSystem VFX_Foot_L;
    private ParticleSystem VFX_Foot_R;

    protected bool isCombo_Shot;
    protected bool isCombo_Skill;
    protected float comboTimer;

    protected enum ComboType
    {
        Shot,
        Skill
    }
    protected ComboType comboType;
    [Header("Basic Setting")]
    [SerializeField] private float ComboTime;
    
    protected virtual void Start()
    {
        VFX_ComboCharge = GameManager.singleton.VFX_List.VFX_ChargePower;
        VFX_CanCombo_Shot = GameManager.singleton.VFX_List.VFX_WindState;
        VFX_Foot_L = GameManager.singleton.VFX_List.VFX_Foot_L;
        VFX_Foot_R = GameManager.singleton.VFX_List.VFX_Foot_R;
    }
    protected virtual void Update()
    {
        ComboTimer();
    }
    private void ComboTimer()
    {
        if (isCombo_Shot || isCombo_Skill)
        {
            comboTimer += Time.deltaTime;

            if(comboTimer > ComboTime)
            {
                SetIsCombo_Shot(false);
                SetIsCombo_Skill(false);
            }
        }
    }
    protected void ComboTrigger_Shot()
    {
        SetIsCombo_Shot(true);
        if (isCombo_Shot) comboTimer = 0;
    }
    protected void ComboTrigger_Skill()
    {
        SetIsCombo_Skill(true);
        if ( isCombo_Skill) comboTimer = 0;
    }
    public void CanUseShotToContinueCombo(out bool canUse)
    {
        canUse = isCombo_Skill;

        if (canUse)
        {
            SetIsCombo_Skill(false);
        }
    }
    public bool CanUseShotToContinueCombo()
    {
        bool CanUse = isCombo_Skill;

        if(isCombo_Skill)
        {
            SetIsCombo_Skill(false);
        }

        return CanUse;
    }
    public void CanUseSkillToContinueCombo(out bool canUse)
    {
        canUse = isCombo_Shot;

        if (canUse)
        {
            SetIsCombo_Shot(false);
        }
    }
    
    public bool CanUseSkillToContinueCombo()
    {
        bool canUse = isCombo_Shot;

        if (isCombo_Shot)
        {
            SetIsCombo_Shot(false);
        }

        return canUse;
    }
    public virtual void ComboChargeStart()
    {
        VFX_ComboCharge.Play();
    }
    public virtual void ComboChargeStop()
    {
        VFX_ComboCharge.Stop();
    }
    protected virtual void ComboStart(ComboType comboType)
    {
        if (comboType == ComboType.Shot) VFX_CanCombo_Shot.Play();
        if (comboType == ComboType.Skill)
        {
            //VFX_CanCombo_Skill.Play();
            VFX_Foot_L.Play();
            VFX_Foot_R.Play();
        }
    }
    protected virtual void ComboEnd(ComboType comboType)
    {
        if (comboType == ComboType.Shot) VFX_CanCombo_Shot.Stop();
        if (comboType == ComboType.Skill)
        { 
            //VFX_CanCombo_Skill.Stop();
            VFX_Foot_L.Stop();
            VFX_Foot_R.Stop();
        } 
    }
    protected void SetIsCombo_Shot(bool value)
    {
        isCombo_Shot = value;

        if(isCombo_Shot)
        {
            ComboStart(ComboType.Shot);
            comboTimer = 0;
        }
        else
        {
            ComboEnd(ComboType.Shot);
        }
    }
    protected void SetIsCombo_Skill(bool value)
    {
        isCombo_Skill = value;

        if (isCombo_Skill)
        {
            ComboStart(ComboType.Skill);
            comboTimer = 0;
        }
        else
        {
            ComboEnd(ComboType.Skill);
        }
    }
}
