using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Playables;

public class NewGamePlay_Dash : NewGamePlay_Basic_Dash
{
    [Header("Setting")]
    [SerializeField] private float dashCooling;

    [Header("Normal Dash")]
    [SerializeField] private float forwardDashSpeed;
    [SerializeField] private float forwardDashDistance;

    [Header("Backward Dash")]
    [SerializeField] private float backwardDashSpeed;
    [SerializeField] private float backwardDashDistance;


    [Header("Dash Combo")]
    [SerializeField] private float dashComboSpeed;
    [SerializeField] private float dashComboDistance;
    [SerializeField] private float dahsComboCoolingDecrease;

    //feedbacks
    private MMF_Player Feedback_DashBack;

    //public 
    public float ShotToDecreaseCoolingTime = 0.1f;
    protected override void Start()
    {
        base.Start();

        //Script
        playerState = GameManager.singleton.Player.GetComponent<PlayerState>();

        //feedbacks
        Feedback_DashBack = GameManager.singleton.Feedbacks_List.DashBack;

    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void DashForwardSetting()
    {
        base.DashForwardSetting();

        //combo
        combo.SetComboShotType(NewGamePlay_Combo.ComboShotType.ScatterShot);

        //variable
        coolingTimer= dashCooling;
        speed = forwardDashSpeed;
        dashDistance = forwardDashDistance;
    }
    protected override void DashBackwardSetting()
    {
        base.DashBackwardSetting();

        //Combo
        combo.SetComboShotType(NewGamePlay_Combo.ComboShotType.WindCard);

        //feedbacks
        Feedback_DashBack.PlayFeedbacks();

        //Gravity
        playerState.SetGravity(0);
        playerState.SetVerticalVelocity(0);

        //variable
        coolingTimer = dashCooling;
        speed = backwardDashSpeed;
        dashDistance = backwardDashDistance;
    }
    protected override void DashComboSetting()
    {
        base.DashComboSetting();

        speed = dashComboSpeed;
        dashDistance = dashComboDistance;
        coolingTimer -= dahsComboCoolingDecrease;
    }
    protected override void DashStop()
    {
        base.DashStop();

        //feedbacks
        Feedback_DashBack.StopFeedbacks();

        //Gravity
        playerState.SetGravityToNormal();

        //variable
        collingTime = dashCooling;
    }
}
