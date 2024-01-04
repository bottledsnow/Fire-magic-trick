using UnityEditor.U2D;
using UnityEngine;

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
    [SerializeField] private float dahsComboCooling;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void DashForwardSetting()
    {
        base.DashForwardSetting();

        speed = forwardDashSpeed;
        dashDistance = forwardDashDistance;
        collingTime = dashCooling;
    }
    protected override void DashBackwardSetting()
    {
        base.DashBackwardSetting();

        speed = backwardDashSpeed;
        dashDistance = backwardDashDistance;
    }
    protected override void DashComboSetting()
    {
        base.DashComboSetting();

        speed = dashComboSpeed;
        dashDistance = dashComboDistance;
        collingTime = dahsComboCooling;
    }
}
