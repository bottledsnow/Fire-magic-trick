using Cinemachine;
using StarterAssets;
using UnityEngine;

public class Move_Our : MonoBehaviour
{
    [Header("Mode")]
    [SerializeField] private bool UseOurMove;
    [Header("Speed")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float slowRunSpeed = 5f;
    [SerializeField] private float fastRunSpeed = 7f;
    [Header("Motion")]
    [SerializeField] private float motion_Move = 1f;
    [SerializeField] private float motion_Run = 1f;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera cameraNormal;
    [SerializeField] private CinemachineVirtualCamera cameraRunFast;

    private ControllerInput _input;
    private ThirdPersonController _thirdPersonController;

    private bool CameraSwitchOn = false;
    private bool Trigger;

    private void Start()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _input = GetComponent<ControllerInput>();

        Initialization();
    }

    private void Initialization()
    {
        if(UseOurMove)
        {
            _thirdPersonController.UseOurMove = true;
        }
    }
    private void Update()
    {
        CameraSwitch();
    }
    private void CameraSwitch()
    {
        if(_input.LSB && !Trigger)
        {
            if(!_input.SprintMode && !CameraSwitchOn)
            {
                SetCameraNormal();
            }
            else
            {
                SetCameraRun();
            }
            Trigger = true;
        }

        if(_input.LeftStick.magnitude < 0.5f)
        {
            SetCameraNormal();
        }

        if(!_input.LSB)
        {
            Trigger = false;
        }
    }
    private void SetCameraNormal()
    {
        cameraNormal.gameObject.SetActive(true);
        cameraRunFast.gameObject.SetActive(false);
        CameraSwitchOn = !CameraSwitchOn;
    }
    private void SetCameraRun()
    {
        cameraNormal.gameObject.SetActive(false);
        cameraRunFast.gameObject.SetActive(true);
        CameraSwitchOn = !CameraSwitchOn;
    }
    public float MoveNew(float _speed,float currentHorizontalSpeed,float targetSpeed,float inputMagnitude,float SpeedChangeRate)
    {
        float StickPower = _input.LeftStick.magnitude;
        if(!_input.SprintMode)
        {
            if (StickPower < 0.5f)
            {
                //move speed
                _speed = moveSpeed * StickPower * 2;
            }
            else
            {
                //slow run speed
                _speed = moveSpeed + (slowRunSpeed-moveSpeed) * StickPower;
            }
        }
        else
        {
            if (StickPower < 0.5f)
            {
                //move speed
                _speed = moveSpeed * StickPower*2;
            }
            else
            {
                // fast run speed
                _speed =slowRunSpeed + (fastRunSpeed- slowRunSpeed)* StickPower;
            }
        }
        _speed = Mathf.Round(_speed * 1000f) / 1000f;

        return _speed;
    }
    public float MoveElseNew(float _speed, float targetSpeed)
    {
        _speed = targetSpeed * _input.LeftStick.magnitude;  
        return _speed;
    }
    public float AnimationBlendNew(float _animationBlend,float _speed)
    {
        float StickPower = _input.LeftStick.magnitude;

        if(_input.SprintMode)
        {
            _animationBlend = fastRunSpeed * StickPower;

        }
        else
        {
            _animationBlend = slowRunSpeed * StickPower;
        }
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        return _animationBlend;
    }
    public float AnimatorMotionNew()
    {
        float StickPower = _input.LeftStick.magnitude;
        float motion = 0;
        if (!_input.SprintMode)
        {
            
            if (0f <StickPower && StickPower < 0.5f)
            {
                //move speed
                motion = motion_Move;
            }
            else
            {
                //slow run speed
                motion = 0.5f + (StickPower/2);
            }
            if (StickPower == 0)
            {
                motion = 1;
            }
        }
        else
        {
            
            if (0f < StickPower &&StickPower < 0.5f)
            {
                //move speed
                motion = 1f;
            }
            else
            {
                // fast run speed
                motion = 0.5f + (StickPower/2);
            }
            if (StickPower == 0)
            {
                motion = 1;
            }
        }
        return motion;
    }
}
