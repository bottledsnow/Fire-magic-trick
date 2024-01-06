using UnityEngine;
using System.Threading.Tasks;
using System.ComponentModel;

public class NewGamePlay_Combo : NewGamePlay_Basic_Combo
{
    [Header("Combo Colling")]
    [SerializeField] private float comboCollingTime;
    [SerializeField] private float comboCardCollingTime;

    [Header("ShotCombo")]
    public ComboShotType comboShotType;

    [Header("Combo")]
    public bool isComboScatterShot;
    public bool isComboTripleShot;
    public bool isComboDash;
    public bool isComboSuperDash;
    public bool isComboKick;
    public bool isComboFloat;

    [Header("Combo Card")]
    public bool isUseFireCard;
    public bool isUseWindCard;
    public bool isUseBoomCard;

    //Script
    private NewGamePlay_Dash dash;
    private NewGamePlay_ChargeShot chargeShot;
    private SuperDashKick kick;

    //delegate
    public delegate void ComboSkillHandler();
    public delegate void ComboShotHandler();
    public event ComboSkillHandler OnUseSkill;
    public event ComboShotHandler OnUseShot;

    public enum ComboShotType
    {
        defaultShot,
        TripleShot,
        ScatterShot,
        FireCard,
        FireCard_Fast,
    }

    protected override void Start()
    {
        base.Start();

        //Script
        chargeShot = GetComponent<NewGamePlay_ChargeShot>();
        dash = GetComponent<NewGamePlay_Dash>();
        kick = GameManager.singleton.EnergySystem.GetComponent<SuperDashKick>();

        //Subscribe
        dash.OnDash += UseSkill;
        kick.OnKick += UseSkill;
        chargeShot.OnUseMaxShot += UseShot;

        //Subscribe Combo
        dash.OnDashCombo += UseComboDash;

        //Subscribe Card
        chargeShot.OnUseFireCard += UseComboFireCard;
        chargeShot.OnUseWindCard += UseComboWindCard;
        chargeShot.OnUseBoomCard += UseComboBoomCard;
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
    }
    public void SetComboShotType(ComboShotType comboShotType)
    {
        this.comboShotType = comboShotType;
    }
    #region Use Combo
    private async void UseComboDash()
    {
        SetIsComboDash(true);
        await Task.Delay((int)(comboCollingTime*1000));
        SetIsComboDash(false);
    }
    private async void UseComboSuperDash()
    {
        SetIsComboSuperDash(true);
        await Task.Delay((int)(comboCollingTime * 1000));
        SetIsComboSuperDash(false);
    }
    private async void UseComboKick()
    {
        SetIsComboKick(true);
        await Task.Delay((int)(comboCollingTime * 1000));
        SetIsComboKick(false);
    }
    private async void UseComboFloat()
    {
        SetIsComboFloat(true);
        await Task.Delay((int)(comboCollingTime * 1000));
        SetIsComboFloat(false);
    }
    private async void UseComboScatterShot()
    {
        SetIsComboScatterShot(true);
        await Task.Delay((int)(comboCollingTime * 1000));
        SetIsComboScatterShot(false);
    }
    private async void UseComboTripleShot()
    {
        SetIsComboTripleShot(true);
        await Task.Delay((int)(comboCollingTime * 1000));
        SetIsComboTripleShot(false);
    }
    private async void UseComboFireCard()
    {
        SetIsUseFireCard(true);
        await Task.Delay((int)(comboCardCollingTime * 1000));
        SetIsUseFireCard(false);
    }
    private async void UseComboWindCard()
    {
        SetIsUseWindCard(true);
        await Task.Delay((int)(comboCardCollingTime * 1000));
        SetIsUseWindCard(false);
    }
    private async void UseComboBoomCard()
    {
        SetIsUseBoomCard(true);
        await Task.Delay((int)(comboCardCollingTime * 1000));
        SetIsUseBoomCard(false);
    }
    #endregion
    #region Set
    private void SetIsComboDash(bool value)
    {
        isComboDash = value;
    }
    private void SetIsComboSuperDash(bool value)
    {
        isComboSuperDash = value;
    }
    private void SetIsComboKick(bool value)
    {
        isComboKick = value;
    }
    private void SetIsComboFloat(bool value)
    {
        isComboFloat = value;
    }
    private void SetIsComboScatterShot(bool value)
    {
        isComboScatterShot = value;
    }
    private void SetIsComboTripleShot(bool value)
    {
        isComboTripleShot = value;
    }
    private void SetIsUseBoomCard(bool value)
    {
        isUseBoomCard = value;
    }
    private void SetIsUseFireCard(bool value)
    {
        isUseFireCard = value;
    }
    private void SetIsUseWindCard(bool value)
    {
        isUseWindCard = value;
    }
    #endregion
}
