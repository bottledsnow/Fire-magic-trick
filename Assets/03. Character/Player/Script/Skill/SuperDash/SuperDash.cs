using UnityEngine;
using MoreMountains.Feedbacks;
using System.Threading.Tasks;
using UnityEngine.Events;

public class SuperDash : MonoBehaviour
{
    public GameObject Target;
    [SerializeField] private AnimationCurve superDashIncreaseSpeed;
    [SerializeField] private AnimationCurve superDashReduceSpeed;
    [SerializeField] private float superDashMaxSpeed;
    [SerializeField] private float SuperDashTimeNormal;
    [SerializeField] private float SuperDashTimeFall;
    [SerializeField] private float SuperDashCollingTime;
    [Header("Crash")]
    [SerializeField] private SuperDashCollider _superDashCollider;
    public float CrashForce;
    public float CrashForceUp;

    [Header("Feedbacks")]
    [SerializeField] private MMF_Player FireDashStart;
    [SerializeField] private MMF_Player FireDashHit;
    [SerializeField] private MMF_Player FireDashEnd;
    [SerializeField] private MMF_Player FireDashEnd_Interrupt;
    [SerializeField] private MMF_Player Feedbacks_SuperDashCooling;
    [Header("Other")]
    [SerializeField] private GameObject Model;


    private SuperDashCameraCheck _superDashCameraCheck;
    private CharacterController _characterController;
    private SuperDashKickDown _superDashKickDown;
    private ControllerInput _input;
    private PlayerAnimator _playerAnimator;
    private PlayerCollider _playerCollider;
    private SuperDashKick _superDashKick;
    private EnergySystem _energySystem;
    private PlayerState _playerState;
    private GameObject player;


    private Vector3 direction;
    private float superDashSpeed = 0;
    private float superDashTimer = 0;
    private bool isCooling;
    private bool isSuperDashThrough;
    private bool isKick;
    private bool TriggerStart;
    public bool isSuperDash;

    public delegate void SuperDashStartHandler();
    public delegate void SuperDashHitGroundHandler();
    public delegate void SuperDashHitKickHandler();
    public delegate void SuperDashHitThroughHandler();
    public delegate void SuperDashEndHandler();
    public delegate void SuperDashHitStarThroyghHandler();
    public delegate void SuperDashHitStarKickHandler();

    public event SuperDashStartHandler OnSuperDashStart;
    public event SuperDashHitGroundHandler OnSuperDashHitGround;
    public event SuperDashHitKickHandler OnSuperDashHitKick;
    public event SuperDashHitThroughHandler OnSuperDashHitThrough;
    public event SuperDashEndHandler OnSuperDashEnd;
    public event SuperDashHitStarThroyghHandler OnSuperDashHitStarThrough;
    public event SuperDashHitStarKickHandler OnSuperDashHitStarKick;


    private void Start()
    {
        _superDashKick = GameManager.singleton.EnergySystem.GetComponent<SuperDashKick>();
        _playerState = GameManager.singleton._playerState;
        _input = GameManager.singleton._input;
        _superDashCameraCheck = GetComponent<SuperDashCameraCheck>();
        _superDashKickDown = GetComponent<SuperDashKickDown>();
        _characterController = _playerState.GetComponent<CharacterController>();
        _playerCollider = GameManager.singleton.Player.GetComponent<PlayerCollider>();
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
        IsSuperDashModelCheck();
    }
    private async void IsSuperDashModelCheck()
    {
        await Task.Delay(300);
        if(isSuperDash)
        {
            if(Model.activeSelf == true)
            {
                Model.SetActive(false);
            }
        }
    }
    private void Initialization()
    {
        isSuperDashThrough = false;
        isSuperDash = false;
        isKick = false;
        _superDashCollider.SetIsSuperDash(false);
        _playerState.TakeControl();
        _superDashCollider.SetEnemyToClose(false);
        SetTriggerStart(false);
        _playerCollider.ClearColliderTarget();
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
                if(_superDashCollider.EnemyToClose)
                {
                    Debug.Log("Enemy To Close");
                }else
                {
                    if(!TriggerStart)
                    {
                        if(!isCooling)
                        {
                            EnergyCheck();
                        }
                    }
                }
            }
        }
    }
    private void EnergyCheck()
    {
        bool CanUse;
        _energySystem.UseSuperDash(out CanUse);

        if(CanUse)
        {
            SuperDashColling();
            superDashStart();
            OnSuperDashStart?.Invoke();
        }
    }
    private async void SuperDashColling()
    {
        SetIsCooling(true);
        Feedbacks_SuperDashCooling.PlayFeedbacks();
        await Task.Delay((int)(SuperDashCollingTime*1000));
        Feedbacks_SuperDashCooling.StopFeedbacks();
        SetIsCooling(false);
    }
    
    private void superDashStart()
    {
        isSuperDash = true;
        _superDashCollider.SetIsSuperDash(true);
        FireDashStart.PlayFeedbacks();
        superDashInterrupt();

        SetTriggerStart(true);
        _superDashKickDown.GetTarget(Target);
        _playerAnimator.SuperDashStart();
        _playerState.SetGravityToFire();
    }
    private void superDash()
    {   
        if (isSuperDash)
        {
            if(Target!=null)
            {
                LookAtTarget();
                _playerState.OutControl();
                speedIncrease();
                calaulateDirection();
                move();
            }
            else
            {
                Debug.Log("EnemyDissapear");
                EnemyDissapear();
            }
        }
    }
    private void LookAtTarget()
    {
        _playerState.transform.LookAt(Target.transform);
    }
    private void speedIncrease()
    {
        superDashTimer = speedTimer(superDashTimer, SuperDashTimeNormal);
        superDashSpeed = superDashIncreaseSpeed.Evaluate(superDashTimer) * superDashMaxSpeed;
    }
    private void calaulateDirection()
    {
        direction = calculateDirection(player.transform.position, Target.transform.position).normalized;
    }
    private void move()
    {
        _characterController.Move(direction * superDashSpeed * Time.deltaTime);
    }
    
    private Vector3 calculateDirection(Vector3 start, Vector3 end)
    {
        return end - start;
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
                    HitGroundEnemy();
                    OnSuperDashHitGround?.Invoke();
                    Debug.Log("Hit Ground Enemy");
                } else
                {
                    if(_superDashKick.timerCheck(isKick))
                    {
                        HitToKickDown();
                        OnSuperDashHitKick?.Invoke();
                        Debug.Log("Kick Enemy SuperDash");
                    }else
                    {
                        superDashToThrough();
                        OnSuperDashHitThrough?.Invoke();
                        Debug.Log("Through Enemy");
                    }
                }
            }else
            if(_playerCollider.hit.collider.CompareTag("FirePoint"))
            {
                FirePoint point = _playerCollider.hit.collider.GetComponent<FirePoint>();
                point.ToUseFirePoint();

                _playerState.TakeControl();
                isSuperDash = false;
                superDashTimer = 0;
                FireDashHit.PlayFeedbacks();

                if (_playerState.nearGround)
                {
                    superDashStop();
                }
                else
                {
                    if (_superDashKick.timerCheck(isKick))
                    {
                        superDashStop();
                        OnSuperDashHitStarKick?.Invoke();
                        Debug.Log("Kick Star SuperDash");
                    }
                    else
                    {
                        superDashToThrough(point);
                        OnSuperDashHitStarThrough?.Invoke();
                    }
                }
            }
        }
    }
    private void HitGroundEnemy()
    {
        superDashStop();
    }
    private void HitToKickDown()
    {
        Debug.Log("KickDown");
        superDashStop();
        _superDashKickDown.KickDown();
    }
    private void superDashStop()
    {
        if(isSuperDash)
        {
            Debug.Log("Stop But IsFire");
        }else
        {
            _superDashCollider.SetIsSuperDash(false);
            _playerState.SetGravityToNormal();
            _playerAnimator.SuperDashEnd();
            FireDashEnd.PlayFeedbacks();
            superDashSpeed = 0;
            Target = null;
            _superDashKickDown.NullTarget();
            SetTriggerStart(false);
            Initialization();
        }
    }
    private void superDashInterruptStop()
    {
        if(isSuperDash)
        {
            _superDashCollider.SetIsSuperDash(false);
            _playerState.SetGravityToNormal();
            _playerState.ResetVerticalvelocity();
            _playerAnimator.SuperDashEnd();
            FireDashEnd_Interrupt.PlayFeedbacks();
            superDashSpeed = 0;
            Target = null;
            _superDashKickDown.NullTarget();
            SetTriggerStart(false);
            Initialization();
        }
    }
    private void superDashToThrough()
    {
        EnemyHealthSystem enemyHealthSystem = _playerCollider.hit.collider.GetComponent<EnemyHealthSystem>();
        enemyHealthSystem.EnemyDeathRightNow();

        _playerCollider.hit.collider.gameObject.SetActive(false);
        isSuperDashThrough = true;
        superDashSpeed = superDashMaxSpeed;
    }
    private void superDashToThrough(FirePoint point)
    {
        //point.ToUseFirePoint();
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
    private async  void superDashInterrupt()
    {
        await Task.Delay(3000);
        superDashInterruptStop();
    }
    public void SetIsKick(bool value)
    {
        isKick = value;
    }
    public void EnemyDissapear()
    {
        if(Target ==null)
        {
            superDashStop();
        }
    }
    private void SetTriggerStart(bool active)
    {
        TriggerStart = active;
    }
    private void SetIsCooling(bool value)
    {
        isCooling = value;
    }
}
