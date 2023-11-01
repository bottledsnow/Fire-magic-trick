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
    private ThirdPersonController _thirdPersonController;
    private ControllerInput _input;
    private EnergySystem _energySystem;
    private PlayerState _playerState;
    private SuperDash _superDash;
    private Animator _playerAnimator;

    private float timer;
    private bool isTimer;
    private bool isInputY;
    private bool isCheck;
    private void Start()
    {
        _playerState = GameManager.singleton._playerState;
        _input = GameManager.singleton._input;
        _superDash = GetComponent<SuperDash>();
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
        if(_superDash.isSuperDash && _input.ButtonY)
        {
            EnergyCheck();
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
            kickFail();
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
        KickTriggerCheck();
        //_superdashKickDown.KickDown();
        SuperJump();
        _playerAnimator.SetTrigger("InputY");
        _playerAnimator.SetBool("isSuperDash", false);
        _playerState.SetGravityToNormal();
        SuperDashEnd.PlayFeedbacks();
        isCheck = false;
        _superDash.SetIsKick(false);
        _superDashCollider.SetKick(false);
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
    }
    private void SuperJump()
    {
        float jumpHeigh = _thirdPersonController.JumpHeight;
        _thirdPersonController.JumpHeight = newJumpHeight;
        _thirdPersonController.Jump();
        _thirdPersonController.JumpHeight = jumpHeigh;
    }
    #endregion
}
