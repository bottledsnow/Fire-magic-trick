using UnityEngine;

public class NewGamePlay_Combo : NewGamePlay_Basic_Combo
{
    private FireDash fireDash;
    private NewGamePlay_ChargeShot chargeShot;

    public delegate void ComboSkillHandler();
    public delegate void ComboShotHandler();
    public event ComboSkillHandler OnUseSkill;
    public event ComboShotHandler OnUseShot;

    protected override void Start()
    {
        base.Start();
        chargeShot = GetComponent<NewGamePlay_ChargeShot>();

        fireDash = GameManager.singleton.EnergySystem.GetComponent<FireDash>();

        fireDash.OnDash += UseSkill;
        chargeShot.OnUseMaxShot += UseShot;
    }
    private void UseSkill()
    {
        OnUseSkill?.Invoke();
        ComboTrigger_Skill();
    }
    private void UseShot()
    {
        OnUseShot?.Invoke();
        ComboTrigger_Shot();
        Debug.Log("Use Shot");
    }
}
