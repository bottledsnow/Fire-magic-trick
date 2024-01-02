using UnityEngine;
using System.Threading.Tasks;

public class NewGamePlay_Basic_Combo : MonoBehaviour
{
    private ParticleSystem VFX_ComboCharge;
    private ParticleSystem VFX_CanCombo;

    protected bool isCombo;
    protected float comboTimer;
    [Header("Basic Setting")]
    [SerializeField] private float ComboTime;
    
    protected virtual void Start()
    {
        VFX_ComboCharge = GameManager.singleton.VFX_List.VFX_ComboCharge;
        VFX_CanCombo = GameManager.singleton.VFX_List.VFX_CanCombo;
    }
    protected virtual void Update()
    {
        if(isCombo)
        {
            comboTimer += Time.deltaTime;
        }

        if(isCombo && comboTimer > ComboTime)
        {
            SetIsCombo(false);
            ComboEnd();
        }
    }
    public void ComboTrigger()
    {
        if (!isCombo) SetIsCombo(true);
        if ( isCombo) comboTimer = 0;
    }
    public void CanUseCombo(out bool canUse)
    {
        canUse = isCombo;

        if (canUse)
        {
            Debug.Log("Combo Comtinu");
            SetIsCombo(false);
        }
        else
        {
            Debug.Log("No or used");
        }
    }
    public virtual void ComboChargeStart()
    {
        VFX_ComboCharge.Play();
    }
    public virtual void ComboChargeStop()
    {
        VFX_ComboCharge.Stop();
    }
    protected virtual void ComboStart()
    {
        VFX_CanCombo.Play();
    }
    protected virtual void ComboEnd()
    {
        VFX_CanCombo.Stop();
    }
    protected void SetIsCombo(bool value)
    {
        isCombo = value;

        if(isCombo)
        {
            ComboStart();
        }else
        {
            ComboEnd();
        }
    }
}
