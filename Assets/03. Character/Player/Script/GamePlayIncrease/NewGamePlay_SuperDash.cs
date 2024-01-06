using UnityEngine;

public class NewGamePlay_SuperDash : NewGamePlay_Basic_SuperDash
{
    protected override void Start()
    {
        base.Start();
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

        combo.SetComboShotType(NewGamePlay_Combo.ComboShotType.FireCard);
        Debug.Log("Kick Enemy");
    }

    protected override void OnSuperDashHitStarKick()
    {
        base.OnSuperDashHitStarKick();

        combo.SetComboShotType(NewGamePlay_Combo.ComboShotType.FireCard_Fast);
        Debug.Log("Kick Star");
    }

    protected override void OnSuperDashHitStarThrough()
    {
        base.OnSuperDashHitStarThrough();
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
