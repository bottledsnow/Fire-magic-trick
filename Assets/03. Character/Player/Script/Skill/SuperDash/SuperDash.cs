using UnityEngine;
using System.Collections;
using StarterAssets;
using MoreMountains.Feedbacks;

public class SuperDash : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private AnimationCurve superDashIncreaseSpeed;
    [SerializeField] private AnimationCurve superDashReduceSpeed;
    [SerializeField] private float superDashMaxSpeed;
    [SerializeField] private float SuperDashTimeNormal;
    [SerializeField] private float SuperDashTimeFall;
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player FireDashStart;
    [SerializeField] private MMF_Player FireDashHit;
    [SerializeField] private MMF_Player FireDashEnd;

    private SuperDashCameraCheck _superDashCameraCheck;
    private CharacterController _characterController;
    private ControllerInput _input;
    private PlayerAnimator _playerAnimator;
    private PlayerCollider _playerCollider;
    private EnergySystem _energySystem;
    private PlayerState _playerState;
    private GameObject player;


    private Vector3 direction;
    private float superDashSpeed = 0;
    private float superDashTimer = 0;
    private bool isSuperDashThrough;

    [HideInInspector] public bool isSuperDash;

    private void Start()
    {
        _playerState = GameManager.singleton._playerState;
        _input = GameManager.singleton._input;
        _superDashCameraCheck = GetComponent<SuperDashCameraCheck>();
        _characterController = _playerState.GetComponent<CharacterController>();
        _playerCollider = _playerState.GetComponent<PlayerCollider>();
        _playerAnimator = _playerState.GetComponent<PlayerAnimator>();
        _energySystem = _playerState.GetComponent<EnergySystem>();
        player = _playerState.gameObject;
    }
    private void Update()
    {
        GetTarget();
        superDashStartCheck();
        superDash();
        superDashHit();
        superDashThrough();
    }

    private void GetTarget()
    {
        Target = _superDashCameraCheck.Target;
    }
    private void superDashStartCheck()
    {
        if (_input.LB && Target != null)
        {
            if (!isSuperDash && !isSuperDashThrough)
            {
                EnergyCheck();
            }
        }
    }
    private void EnergyCheck()
    {
        bool CanUse;
        _energySystem.UseSuperDash(out CanUse);

        if(CanUse)
        {
            superDashStart();
        }
    }
    
    private void superDashStart()
    {
        isSuperDash = true;
        _playerAnimator.SuperDashStart();
        _playerState.SetGravityToFire();
        FireDashStart.PlayFeedbacks();
        Debug.Log("SuperDash");
    }
    private void superDash()
    {   
        if (isSuperDash)
        {
            LookAtTarget();
            _playerState.OutControl();
            speedIncrease();
            calaulateDirection();
            move();
        }
    }
    private void move()
    {
        _characterController.Move(direction * superDashSpeed * Time.deltaTime);
    }
    private void calaulateDirection()
    {
        direction = calculateDirection(player.transform.position, Target.transform.position).normalized;
    }
    private Vector3 calculateDirection(Vector3 start, Vector3 end)
    {
        return end - start;
    }
    private void speedIncrease()
    {
       superDashTimer = speedTimer(superDashTimer,SuperDashTimeNormal);
       superDashSpeed = superDashIncreaseSpeed.Evaluate(superDashTimer) * superDashMaxSpeed;
    }
    private float speedTimer(float timer,float dashtime)
    {
        if (timer <= 1f)
        {
            timer += Time.deltaTime/ dashtime;
        }else
        {
            timer = 1;
        }
        return timer;
    }
    private void superDashHit()
    {
        if(isSuperDash && _playerCollider.hit !=null)
        {
            if (_playerCollider.hit.collider.tag == "Enemy")
            {
                _playerState.TakeControl();
                isSuperDash = false;
                superDashTimer = 0;
                FireDashHit.PlayFeedbacks();

                if (_playerState.nearGround)
                {
                    superDashStop();
                } else
                {
                    superDashToThrough();
                }
            }
        }
    }
    private void superDashStop()
    {
        _playerState.SetGravityToNormal();
        _playerAnimator.SuperDashEnd();
        FireDashEnd.PlayFeedbacks();
        superDashSpeed = 0;
        Target = null;
    }
    private void superDashToThrough()
    {
        _playerCollider.hit.collider.gameObject.SetActive(false);
        isSuperDashThrough = true;
        superDashSpeed = superDashMaxSpeed;
    }
    private void superDashThrough()
    {
        if (isSuperDashThrough)
        {
            speedReduce();
            move();
        }
        if(_playerState.isGround && isSuperDashThrough)
        {
            isSuperDashThrough = false;
            superDashStop();
        }
    }
    private void speedReduce()
    {
        superDashTimer = speedTimer(superDashTimer, SuperDashTimeFall);
        superDashSpeed = superDashReduceSpeed.Evaluate(superDashTimer) * superDashMaxSpeed;
    }
    private void LookAtTarget()
    {
        _playerState.transform.LookAt(Target.transform);
    }
}
