using StarterAssets;
using UnityEngine;
using System.Threading.Tasks;
using MoreMountains.Feedbacks;

public class NewGamePlay_Basic_Dash : MonoBehaviour
{
    //Script
    private ControllerInput input;
    private CharacterController characterController;
    private ThirdPersonController thirdPersonController;
    private FireDashCollider fireDashCollider;
    private NewGamePlay_Combo combo;

    //Feedbacks
    private MMF_Player Feedbacks_Dash;

    //delegate
    public delegate void PlayerDashHandler();
    public delegate void PlayerDashComboHandler();
    public event PlayerDashHandler OnDash;
    public event PlayerDashComboHandler OnDashCombo;

    protected enum DashType
    {
        DashForward,
        DashBackward,
    }
    protected DashType dashType;
    [Header("Setting")]
    protected float collingTime;
    protected float speed;
    protected float dashDistance;

    private bool isDash;
    private bool isCooling;
    private float dashTime;
    
    protected virtual void Start()
    {
        //Script
        input = GameManager.singleton._input;
        characterController = input.GetComponent<CharacterController>();
        thirdPersonController = input.GetComponent<ThirdPersonController>();
        fireDashCollider = GameManager.singleton.Collider_List.DashCrash.GetComponent<FireDashCollider>();
        combo = GetComponent<NewGamePlay_Combo>();

        //Feedbacks
        Feedbacks_Dash = GameManager.singleton.Feedbacks_List.Dash;

        //Initialize
        DashForwardSetting();
    }
    protected virtual void Update()
    {
        button();
        system();
    }
    private void button()
    {
        if(input.ButtonB)
        {
            if (!isCooling && !isDash)
            {
                SetIsDash(true);

                if (input.LeftStick == new Vector2(0, 0)) SetIsDashType(DashType.DashBackward);
                if (combo.CanUseSkillToContinueCombo() && !combo.isComboDash) DashComboSetting();

                OnDash?.Invoke();
                ToDash();
            }
        }
    }
    private void system()
    {
        if (isDash)
        {
            if(dashType == DashType.DashForward)  Dash(DashType.DashForward);
            if(dashType == DashType.DashBackward) Dash(DashType.DashBackward);
        }
    }
    private async void ToDash()
    {
        CaculateDashTime();
        SetIsDash(true);
        ToCooling();
        await Task.Delay((int)(dashTime*1000));
        SetIsDash(false);
    }
    private async void ToCooling()
    {
        SetIsCooling(true);
        await Task.Delay((int)(collingTime * 1000));
        SetIsCooling(false);
    }
    
    private void CaculateDashTime()
    {
        dashTime = dashDistance / speed;
    }
    protected virtual void Dash(DashType dashType)
    {
        Vector3 direction = Vector3.zero;
        if(dashType == DashType.DashForward) direction = Vector3.forward;
        if (dashType == DashType.DashBackward) direction = Vector3.back;

        Vector3 Dir = Quaternion.Euler(0, thirdPersonController.PlayerRotation, 0) * direction;
        characterController.Move(Dir * speed * Time.deltaTime);
    }
    protected virtual void DashForwardSetting() { }
    protected virtual void DashBackwardSetting() { }
    protected virtual void DashComboSetting() { OnDashCombo?.Invoke(); }
    private void OpenCrash()
    {
        fireDashCollider.SetIsDash(true);
    }
    private void CloseCrash()
    {
        fireDashCollider.SetIsDash(false);
        fireDashCollider.SetIsTriggerDamage(false);
    }
    private void SetIsDash(bool value)
    {
        isDash = value;

        if (isDash)
        {
            Feedbacks_Dash.PlayFeedbacks();
            OpenCrash();
        }else
        {
            SetIsDashType(DashType.DashForward);
            Feedbacks_Dash.StopFeedbacks();
            CloseCrash();
        }
    }
    private void SetIsCooling(bool value)
    {
        isCooling = value;
    }
    private void SetIsDashType(DashType dashType)
    {
        this.dashType = dashType;

        if(dashType == DashType.DashForward)  DashForwardSetting();
        if(dashType == DashType.DashBackward) DashBackwardSetting();
    }
}
