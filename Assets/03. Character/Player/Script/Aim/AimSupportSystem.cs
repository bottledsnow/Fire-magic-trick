using Cinemachine;
using UnityEngine;

public class AimSupportSystem : Basic_AimSupportSystem
{
    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera Normal;
    [SerializeField] private CinemachineVirtualCamera Run;
    [SerializeField] private float LockDamping = 0.5f;

    //Script 
    private CinemachineFramingTransposer transposer_Normal;
    private CinemachineFramingTransposer transposer_Run;
    private SuperDash superDash;

    //value
    private float Normal_origin_XDamping;
    private float Normal_origin_YDamping;
    private float Normal_origin_ZDamping;
    private float Run_origin_XDamping;
    private float Run_origin_YDamping;
    private float Run_origin_ZDamping;

    protected override void Start()
    {
        base.Start();

        //script
        superDash = GameManager.singleton.EnergySystem.GetComponent<SuperDash>();

        //subscribe
        superDash.OnSuperDashStart += NullTarget;

        if(Normal != null) transposer_Normal = Normal.GetCinemachineComponent<CinemachineFramingTransposer>();
        if(Run != null)    transposer_Run    = Run.   GetCinemachineComponent<CinemachineFramingTransposer>();

        //Get Origin Damping
        if(transposer_Normal != null)
        {
            Normal_origin_XDamping = transposer_Normal.m_XDamping;
            Normal_origin_YDamping = transposer_Normal.m_YDamping;
            Normal_origin_ZDamping = transposer_Normal.m_ZDamping;
        }
        if(transposer_Run != null)
        {
            Run_origin_XDamping = transposer_Run.m_XDamping;
            Run_origin_YDamping = transposer_Run.m_YDamping;
            Run_origin_ZDamping = transposer_Run.m_ZDamping;
        }
    }
    public override void ToAimSupport(GameObject Target)
    {
        base.ToAimSupport(Target);

        if(Target != null)
        {
            
            if (transposer_Normal != null)
            {
                transposer_Normal.m_XDamping = LockDamping;
                transposer_Normal.m_YDamping = LockDamping;
                transposer_Normal.m_ZDamping = LockDamping;
            }
            if (transposer_Run != null)
            {
                transposer_Run.m_XDamping = LockDamping;
                transposer_Run.m_YDamping = LockDamping;
                transposer_Run.m_ZDamping = LockDamping;
            }
        }else
        {
            if(transposer_Normal != null)
            {
                transposer_Normal.m_XDamping = Normal_origin_XDamping;
                transposer_Normal.m_YDamping = Normal_origin_YDamping;
                transposer_Normal.m_ZDamping = Normal_origin_ZDamping;
            }
            if(transposer_Run != null)
            {
                transposer_Run.m_XDamping = Run_origin_XDamping;
                transposer_Run.m_YDamping = Run_origin_YDamping;
                transposer_Run.m_ZDamping = Run_origin_ZDamping;
            }
        }
    }
    private void NullTarget()
    {
        if(isLocckTarget)
        {
            ToAimSupport(null);
        }
    }
}
