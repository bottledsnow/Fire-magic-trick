using MoreMountains.Feedbacks;
using StarterAssets;
using UnityEngine;
using System.Threading.Tasks;

public class SuperDashKick : MonoBehaviour
{
    [SerializeField] private float leastEffectTime;
    [SerializeField] private float newJumpHeight;
    [Header("Layermask")]
    [SerializeField] private LayerMask NormalLeyer;
    [SerializeField] private LayerMask KickLayer;
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player SuperDashEnd;
    [Header("Collider")]
    [SerializeField] private SuperDashCollider _superDashCollider;
    [SerializeField] private SuperDashKickTrigger _superDashKickTrigger;

    //Script
    private ThirdPersonController _thirdPersonController;
    private ControllerInput _input;
    private EnergySystem _energySystem;
    private PlayerState _playerState;
    private SuperDash _superDash;
    private FireFloat _fireFloat;
    private Animator _playerAnimator;

    //delegate
    public delegate void OnKickHandler();
    public event OnKickHandler OnKick;

    //Variable
    private float timer;
    private bool isTimer;
    private bool isInputY;
    private bool isCheck;
    private void Awake()
    {
        _playerState = GameManager.singleton._playerState;
        _input = GameManager.singleton._input;
        _superDash = GetComponent<SuperDash>();
        _fireFloat = GetComponent<FireFloat>();
        _thirdPersonController = _playerState.GetComponent<ThirdPersonController>();
        _playerAnimator = _playerState.GetComponent<Animator>();
        _energySystem = _playerState.GetComponent<EnergySystem>();
    }
    private void Update()
    {
        GetButtonInput();
        effectTimer();
    }
    #region KickCheck
    private void GetButtonInput()
    {
        if(_superDash.isSuperDash)
        {
            if (_input.ButtonY ||_input.ButtonA)
            {
                EnergyCheck();
            }
        }
    }
    private void EnergyCheck()
    {
        if(!isCheck)
        {
            isCheck = true;
            bool CanUse = false;
            _energySystem.UseKick(out CanUse);

            if (CanUse)
            {
                KickStart();
            }
        }
    }
    private void KickStart()
    {
        isInputY = true;
        isTimer = true;
        _superDashCollider.SetKick(true);
        _superDash.SetIsKick(true);
    }
    private void effectTimer()
    {
        if (isTimer)
        {
            timer += Time.deltaTime;
        }
        if(!_superDash.isSuperDash && isInputY)
        {
            timerCheck();
            timer = 0;
            isInputY = false;
            isTimer = false;
        }
    }
    
    private void timerCheck()
    {
        if (timer < leastEffectTime)
        {
            kickSuccess();
        } else
        {
            kickSuccess();
            //kickFail();
        }
    }
    public bool timerCheck(bool kickSuccess)
    {
        if(!isInputY)
        {
            return false;
        }else
        {
            if (timer < leastEffectTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    #endregion
    #region Kick
    private void kickSuccess()
    {
        OnKick?.Invoke();
        KickTriggerCheck();
        SuperJump();
        _playerAnimator.SetTrigger("InputY");
        _playerAnimator.SetBool("isSuperDash", false);
        _playerState.SetGravityToNormal();
        SuperDashEnd.PlayFeedbacks();
        isCheck = false;
        _superDash.SetIsKick(false);
        _superDashCollider.SetKick(false);
        _superDashCollider.SetIsSuperDash(false);
        _superDashKickTrigger.SetTriggerKickCollider(true);
    }
    private async void KickTriggerCheck()
    {
        _playerState.gameObject.layer = 16;
        await Task.Delay(500);
        _playerState.gameObject.layer = 6;
    }
    private void kickFail()
    {
        Debug.Log("Fail");
        isCheck = false;
        _superDash.SetIsKick(false);
        _superDashCollider.SetKick(false);
        _superDashCollider.SetIsSuperDash(false);
    }
    private void SuperJump()
    {
        _thirdPersonController.ResetJump();
        _fireFloat.ResetFloatingTrigger();

        float jumpHeigh = _thirdPersonController.JumpHeight;
        _thirdPersonController.JumpHeight = newJumpHeight;
        _thirdPersonController.Jump();
        _thirdPersonController.JumpHeight = jumpHeigh;
    }
    #endregion
}
