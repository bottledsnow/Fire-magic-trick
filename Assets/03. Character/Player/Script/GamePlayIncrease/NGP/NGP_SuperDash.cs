using UnityEngine;

public class NGP_SuperDash : NGP_Basic_SuperDash
{
    private VibrationController vibrationController;
    protected override void Start()
    {
        base.Start();
        vibrationController = GameManager.singleton.GetComponent<VibrationController>();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void OnSuperDashEnd()
    {
        base.OnSuperDashEnd();
    }
    protected override void OnSuperDashHitGround()
    {
        base.OnSuperDashHitGround();
    }
    protected override void OnSuperDashHitKick()
    {
        base.OnSuperDashHitKick();

        combo.UseSuperDashKick();
    }
    protected override void OnSuperDashHitStarKick()
    {
        base.OnSuperDashHitStarKick();

        vibrationController.Vibrate(0.5f, 0.25f);
        combo.UseSuperDashKick();
        superDash.DecreaseSuperDashTimer(3f);
        Debug.Log("Kick Star");
    }
    protected override void OnSuperDashHitStarThrough()
    {
        base.OnSuperDashHitStarThrough();
        vibrationController.Vibrate(0.5f, 0.25f);
    }
    protected override void OnSuperDashHitThrough()
    {
        base.OnSuperDashHitThrough();
    }
    protected override void OnSuperDashStart()
    {
        base.OnSuperDashStart();
    }
}
