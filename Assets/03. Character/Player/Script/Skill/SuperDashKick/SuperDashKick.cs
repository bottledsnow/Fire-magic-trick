using MoreMountains.Feedbacks;
using StarterAssets;
using UnityEngine;

public class SuperDashKick : MonoBehaviour
{
    [SerializeField] private float leastEffectTime;
    [SerializeField] private float newJumpHeight;
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player SuperDashEnd;

    private ThirdPersonController _thirdPersonController;
    private SuperDashKickDown _superdashKickDown;
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
        _superdashKickDown = GetComponent<SuperDashKickDown>();
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
    #endregion
    #region Kick
    private void kickSuccess()
    {
        _superdashKickDown.KickDown();
        SuperJump();
        _playerAnimator.SetTrigger("InputY");
        _playerAnimator.SetBool("isSuperDash", false);
        _playerState.SetGravityToNormal();
        SuperDashEnd.PlayFeedbacks();
        isCheck = false;
    }
    private void kickFail()
    {
        Debug.Log("Fail");
        isCheck = false;
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
