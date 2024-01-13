using UnityEngine;

public class NGP_Basic_Combo : MonoBehaviour
{
    //Script
    protected NGP_Dash dash;

    //variable
    public bool CanComboShot
    {
        get { return canComboShot; }
    }
    public bool CanComboSkill
    {
        get { return canComboSkill; }
    }
    [Header("Combo")]
    [SerializeField] private bool canComboShot;
    [SerializeField] private bool canComboSkill;

    [Header("ComboTime")]
    [SerializeField] private float dashComboTime;
    private float timer_DashCombo;
    private bool isTimer_DashCombo;

    protected virtual void Start() 
    {
        //Script
        dash = GameManager.singleton.NewGamePlay.GetComponent<NGP_Dash>();

        //Initialize
        setCanComboShot(false);
        setCanComboSkill(false);

        //Subscribe
        dash.OnDash += UseDash;
    }
    protected virtual void Update() 
    {
        dashComboTimer();
    }
    public void UseComboShot() { setCanComboShot(false); }
    public void UseComboSkill() { setCanComboSkill(false); }
    public virtual void UseDash() 
    {
        setCanComboShot(true);
    }
    public virtual void UseSuperDash() { setCanComboShot(true); }
    private void dashComboTimer()
    {
        if(isTimer_DashCombo)
            timer_DashCombo -= Time.deltaTime;

        if (timer_DashCombo < 0)
            setIsTimer_DashCombo(false);
    }
    private void setCanComboShot(bool value) { canComboShot = value; }
    private void setCanComboSkill(bool value) { canComboSkill = value; }
    private void setIsTimer_DashCombo(bool value) { isTimer_DashCombo = value; }
}
