using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGP_SuperDash : NGP_Basic_SuperDash
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

        combo.UseSuperDashKick();
        Debug.Log("Kick Enemy");
    }
    protected override void OnSuperDashHitStarKick()
    {
        base.OnSuperDashHitStarKick();

        combo.UseSuperDashKick();
        superDash.DecreaseSuperDashTimer(3f);
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
