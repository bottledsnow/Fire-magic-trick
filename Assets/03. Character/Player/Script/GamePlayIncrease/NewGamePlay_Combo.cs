using UnityEngine;
using System.Threading.Tasks;

public class NewGamePlay_Combo : NewGamePlay_Basic_Combo
{
    [Header("Combo Colling")]
    [SerializeField] private float comboShotCollingTime =0.5f;
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

    //VFX
    private ParticleSystem VFX_UseSkill;
    private ParticleSystem VFX_UseShot;

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
        WindCard,
    }

    protected override void Start()
    {
        base.Start();

        //Script
        kick = GameManager.singleton.EnergySystem.GetComponent<SuperDashKick>();
        chargeShot = GetComponent<NewGamePlay_ChargeShot>();
        dash = GetComponent<NewGamePlay_Dash>();

        //VFX
        VFX_UseSkill = GameManager.singleton.VFX_List.VFX_UseSkill;
        VFX_UseShot = GameManager.singleton.VFX_List.VFX_UseShot;

        //Subscribe
        dash.OnDash += UseSkill;
        kick.OnKick += UseSkill;
        chargeShot.OnUseMaxShot += UseShot;
        chargeShot.OnUseFireCard += UseShot;

        //Subscribe Combo
        dash.OnDashCombo += UseComboDash;
        chargeShot.OnUseScatterShotCombo += UseComboScatterShot;
        chargeShot.OnUseTripleShotCombo += UseComboTripleShot;

        //Subscribe Card
        chargeShot.OnUseFireCard += UseComboFireCard;
        chargeShot.OnUseWindCard += UseComboWindCard;
        chargeShot.OnUseBoomCard += UseComboBoomCard;
    }
    private void UseSkill()
    {
        OnUseSkill?.Invoke();
        ComboTrigger_Skill();
        VFX_UseSkill.gameObject.SetActive(true);
        VFX_UseShot.gameObject.SetActive(false);
    }
    private void UseShot()
    {
        OnUseShot?.Invoke();
        ComboTrigger_Shot();
        VFX_UseShot.gameObject.SetActive(true);
        VFX_UseSkill.gameObject.SetActive(false);
    }
    protected override void ComboEnd(ComboType comboType)
    {
        base.ComboEnd(comboType);

        VFX_UseSkill.gameObject.SetActive(false);
        VFX_UseShot.gameObject.SetActive(false);
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
        dash.DecreaseDashCooling(dash.ShotToDecreaseCoolingTime);
        await Task.Delay((int)(comboShotCollingTime * 1000));
        SetIsComboScatterShot(false);
    }
    private async void UseComboTripleShot()
    {
        SetIsComboTripleShot(true);
        Debug.Log("UseComboTripleShot");
        dash.DecreaseDashCooling(dash.ShotToDecreaseCoolingTime);
        await Task.Delay((int)(comboShotCollingTime * 1000));
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
