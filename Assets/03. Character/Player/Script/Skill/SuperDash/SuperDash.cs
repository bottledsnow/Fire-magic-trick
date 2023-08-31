using UnityEngine;
using System.Collections;

public class SuperDash : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    [SerializeField] private AnimationCurve superDashIncreaseSpeed;
    [SerializeField] private float superDashMaxSpeed;
    [SerializeField] private float SuperDashTime;
    private SuperDashCameraCheck _superDashCameraCheck;
    private CharacterController _characterController;
    private ControllerInput _input;
    private PlayerCollider _playerCollider;
    private PlayerState _playerState;
    private GameObject player;

    private float superDashSpeed = 0;
    private float superDashTimer = 0;
    private bool isSuperDash;

    private void Start()
    {
        _playerState = GameManager.singleton._playerState;
        _input = GameManager.singleton._input;
        _superDashCameraCheck = GetComponent<SuperDashCameraCheck>();
        _characterController = _playerState.GetComponent<CharacterController>();
        _playerCollider = _playerState.GetComponent<PlayerCollider>();
        player = _playerState.gameObject;
    }
    private void Update()
    {
        GetTarget();
        superDashStart();
        superDash();
        superDashStop();
    }
    
    private void GetTarget()
    {
        Target = _superDashCameraCheck.Target;
    }
    private void superDashStart()
    {
        if (_input.LB)
        {
            Debug.Log("LB");
            if(!isSuperDash)
            {
                Debug.Log("!isSuperDash");
                if(Target!=null)
                {
                    isSuperDash = true;
                    Debug.Log("SuperDash");
                }
            }
        }
        /*
        if(_input.RB && !isSuperDash && Target != null)
        {
            isSuperDash = true;
            Debug.Log("SuperDash");
        }
        */
    }
    private void superDash()
    {   
        if (isSuperDash)
        {   
            _playerState.OutControl();
            speedIncrease();
            move();
        }
    }

    private void move()
    {
        Vector3 Direction = CalculateDirection(player.transform.position, Target.transform.position).normalized;
        _characterController.Move(Direction * superDashSpeed * Time.deltaTime);
    }
    private Vector3 CalculateDirection(Vector3 start, Vector3 end)
    {
        return end - start;
    }
    private void speedIncrease()
    {
       superDashTimer = speedTimer(superDashTimer);
        superDashSpeed = superDashIncreaseSpeed.Evaluate(superDashTimer) * superDashMaxSpeed;
    }
    private float speedTimer(float timer)
    {
        if (timer <= 1f)
        {
            timer += Time.deltaTime/SuperDashTime;
        }else
        {
            timer = 1;
        }
        return timer;
    }
    private void superDashStop()
    {
        if( isSuperDash)
        {
            if (_playerCollider.hit.collider.tag == "Enemy")
            {
                _playerState.TakeControl();
                isSuperDash = false;
                superDashSpeed = 0;
                superDashTimer = 0;
                Target = null;
            }
        }
    }
}
