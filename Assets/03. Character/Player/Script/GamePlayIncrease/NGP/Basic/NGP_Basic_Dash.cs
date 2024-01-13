using MoreMountains.Feedbacks;
using StarterAssets;
using UnityEngine;
using System.Threading.Tasks;

public class NGP_Basic_Dash : MonoBehaviour
{
    //Script
    protected PlayerState playerState;
    private ThirdPersonController thirdPersonController;
    private CharacterController characterController;
    private FireDashCollider fireDashCollider;
    private ControllerInput input;

    //Feedbacks
    private MMF_Player Feedbacks_Dash;

    //delegate
    public delegate void DashDelegateHandler();
    public event DashDelegateHandler OnDash;

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
    protected float coolingTimer;

    //value
    private bool isDash;
    private bool isCooling;
    private bool isButton;
    private float dashTime;

    protected virtual void Start()
    {
        //Script
        input = GameManager.singleton._input;
        characterController = input.GetComponent<CharacterController>();
        thirdPersonController = input.GetComponent<ThirdPersonController>();
        fireDashCollider = GameManager.singleton.Collider_List.DashCrash.GetComponent<FireDashCollider>();
        playerState = GameManager.singleton.Player.GetComponent<PlayerState>();

        //Feedbacks
        Feedbacks_Dash = GameManager.singleton.Feedbacks_List.Dash;
    }
    protected virtual void Update()
    {
        button();
        system();
    }
    private void button()
    {
        if (input.ButtonB && !isButton)
        {
            SetIsButton(true);

            if (!isCooling && !isDash)
            {
                if (WindButton()) SetIsDashType(DashType.DashBackward);
                if (FireButton()) SetIsDashType(DashType.DashForward);
                if (CanCombo()) DashComboSetting();

                SetIsDash(true);
                ToDash();
            }
        }
        else
        {
            if (!input.ButtonB && isButton)
            {
                SetIsButton(false);
            }
        }
    }
    protected virtual bool WindButton() { return input.LeftStick == new Vector2(0, 0); }
    protected virtual bool FireButton() { return input.LeftStick != new Vector2(0, 0); }
    protected virtual bool CanCombo() { return false; }
    protected virtual void DashForwardSetting() { }
    protected virtual void DashBackwardSetting() { }
    protected virtual void DashComboSetting() { }
    private void Dash(DashType dashType)
    {
        Vector3 direction = Vector3.zero;
        Vector3 dir = Vector3.zero;

        if (dashType == DashType.DashForward)
        {
            direction = Vector3.forward;
            dir = Quaternion.Euler(0, thirdPersonController.PlayerRotation, 0) * direction;
        }
        if (dashType == DashType.DashBackward)
        {
            direction = Camera.main.transform.forward * -1;
            dir = new Vector3(direction.x, 0, direction.z).normalized;
            playerState.TurnToNewDirection(-dir);
        }
        characterController.Move(dir * speed * Time.deltaTime);
    }
    protected virtual void DashStop() { }
    private void system()
    {
        if (isDash)
        {
            if (dashType == DashType.DashForward) Dash(DashType.DashForward);
            if (dashType == DashType.DashBackward) Dash(DashType.DashBackward);
        }

        if (isCooling)
        {
            coolingTimer -= Time.deltaTime;
        }

        if (coolingTimer <= 0)
        {
            SetIsCooling(false);
            coolingTimer = collingTime;
        }
    }
    private async void ToDash()
    {
        CaculateDashTime();
        SetIsDash(true);
        SetIsCooling(true);
        OnDash?.Invoke();
        await Task.Delay((int)(dashTime * 1000));
        SetIsDash(false);
    }
    private void CaculateDashTime()
    {
        dashTime = dashDistance / speed;
    }
    public void DecreaseDashCooling(float value)
    {
        coolingTimer -= value;
    }
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
        }
        else
        {

            Feedbacks_Dash.StopFeedbacks();
            DashStop();
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

        if (dashType == DashType.DashForward) DashForwardSetting();
        if (dashType == DashType.DashBackward) DashBackwardSetting();
    }
    private void SetIsButton(bool value)
    {
        isButton = value;
    }
}
