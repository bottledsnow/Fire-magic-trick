using UnityEngine;

public class NGP_CameraSystem : NGP_Basic_CameraSystem
{
    [Header("CameraLookPlayerForward")]
    [SerializeField] private GameObject Target;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void ClearTarget()
    {
        TurnCameraToTarget(null);
    }
    protected override void LockToTarget()
    {
        TurnCameraToTarget(superDashCameraCheck.Target);
    }
    protected override void LookTarget()
    {
        if (aimSupport.target != null)
        {
            if (superDashCameraCheck.Target == null)
            {
                setIsLookTarget(false);
                ClearTarget();
            }

            state.TurnToAimDirection();
        }
    }
    protected override void LookForward()
    {
        TrunCameraToPlayerForward();
    }
    private void TurnCameraToTarget(GameObject Target)
    {
        aimSupport.ToAimSupport(Target);
    }
    protected void TrunCameraToPlayerForward() { aimSupport.ToAimSupport_onlySmooth(Target); }
}

