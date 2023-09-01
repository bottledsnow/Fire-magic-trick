using StarterAssets;
using UnityEngine;

public class SuperDashKick : MonoBehaviour
{
    [SerializeField] private float leastEffectTime;
    private ThirdPersonController _thirdPersonController;
    private CharacterController _characterController;
    private ControllerInput _input;
    private PlayerState _playerState;
    private SuperDash _superDash;

    private float timer;
    private bool isTimer;
    private bool isInputY;
    private bool isSuperDashKick;

    private void Start()
    {
        _playerState = GameManager.singleton._playerState;
        _input = GameManager.singleton._input;
        _superDash = GetComponent<SuperDash>();
        _thirdPersonController = _playerState.GetComponent<ThirdPersonController>();
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
            isInputY = true;
            isTimer = true;
        }
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
        Debug.Log("Success");
        _thirdPersonController.Jump();
        _playerState.SetGravityToNormal();
    }
    private void kickFail()
    {
        Debug.Log("Fail");
    }
    #endregion
}
