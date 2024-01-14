using UnityEngine;

public class NGP_Basic_Combo : MonoBehaviour
{
    [Header("Combo")]
    [SerializeField] private bool canComboShot;
    [SerializeField] private bool canComboDash;

    [Header("ComboTime")]
    [SerializeField] private float dashComboTime;

    //variable
    public bool CanComboShot
    {
        get { return canComboShot; }
    }
    public bool CanComboDash
    {
        get { return canComboDash; }
    }
    public bool CanSuperDashShot
    {
        get { return canSuperDashShot; }
    }
    protected float timer_DashCombo;
    private bool isTimer_DashCombo;
    private bool canSuperDashShot;
    private float canCSTimer;
    private float canComboShotTime = 2.5f;

    //Script
    protected NGP_Dash dash;
    protected NGP_ChargeShot chargeShot;

    protected virtual void Start() 
    {
        //Script
        dash = GameManager.singleton.NewGamePlay.GetComponent<NGP_Dash>();
        chargeShot = GameManager.singleton.NewGamePlay.GetComponent<NGP_ChargeShot>();

        //Initialize
        setCanComboShot(false);
        setCanComboDash(false);

        //Subscribe
        dash.OnDash += UseDash;
        chargeShot.OnChargeMaxShot += UseMaxChargeShot;
        dash.OnDashCombo += UseComboDash;
    }
    protected virtual void Update() 
    {
        dashComboTimer();
        canComboShotTimer();
    }
    public void UseComboShot() { setCanComboShot(false); }
    public void UseComboDash() 
    { 
        setCanComboDash(false); 
        setIsTimer_DashCombo(true);
    }
    public virtual void UseDash() { setCanComboShot(true); }
    public virtual void UseMaxChargeShot() 
    { 
        if(!isTimer_DashCombo)
        {
            setCanComboDash(true);
            timer_DashCombo = dashComboTime;
            dash.CoolingStopRightNow();
        }
    }
    public virtual void UseSuperDashKick() 
    {
        setCanSuperDashShot(true);
        setCanComboShot(true);
    }
    private void dashComboTimer()
    {
        if(isTimer_DashCombo)
            timer_DashCombo -= Time.deltaTime;

        if (timer_DashCombo < 0)
            setIsTimer_DashCombo(false);
    }
    private void canComboShotTimer()
    {
        if (canComboShot)
        {
            canCSTimer -= Time.deltaTime;
        }

        if (canCSTimer < 0)
        {
            setCanComboShot(false);
        }
    }
    private void setCanComboShot(bool value)
    {
        canComboShot = value;
        if (canComboShot)
        {
            canCSTimer = canComboShotTime;
        }else
        {
            setCanSuperDashShot(false);
        }
    }
    private void setCanComboDash(bool value) { canComboDash = value; }
    private void setIsTimer_DashCombo(bool value) { isTimer_DashCombo = value; }
    private void setCanSuperDashShot(bool value) { canSuperDashShot = value; }
}
