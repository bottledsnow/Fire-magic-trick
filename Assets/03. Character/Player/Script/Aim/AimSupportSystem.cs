using Cinemachine;
using UnityEngine;

public class AimSupportSystem : Basic_AimSupportSystem
{
    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera Normal;
    [SerializeField] private CinemachineVirtualCamera Run;

    //Script 
    private CinemachineFramingTransposer transposer_Normal;
    private CinemachineFramingTransposer transposer_Run;
    private NewGamePlay_Combo combo;

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
        combo = GameManager.singleton.NewGamePlay.GetComponent<NewGamePlay_Combo>();

        //subscribe
        combo.OnUseSkill += NullTarget;

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
                transposer_Normal.m_XDamping = 0;
                transposer_Normal.m_YDamping = 0;
                transposer_Normal.m_ZDamping = 0;
            }
            if (transposer_Run != null)
            {
                transposer_Run.m_XDamping = 0;
                transposer_Run.m_YDamping = 0;
                transposer_Run.m_ZDamping = 0;
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

    public override void ToAimSupport_onlySmooth(GameObject Target)
    {
        base.ToAimSupport_onlySmooth(Target);
    }
    private void NullTarget()
    {
        if(isLocckTarget)
        {
            ToAimSupport(null);
        }
    }
}
