using StarterAssets;
using UnityEngine;
using System.Threading.Tasks;

public class AimSupportSystem : MonoBehaviour
{
    private ThirdPersonController _input;
    [Header("Target")]
    [SerializeField] private GameObject target;
    [Header("AimSupport")]
    [SerializeField] private bool isAimSupport;
    [SerializeField] private float aimSupportTime;
    [Header("offset")]
    [SerializeField] private float delayTime;
    [SerializeField] private float offest_X;
    [SerializeField] private float offest_Y;
    [Header("Smooth Rotate")]
    [SerializeField] private AnimationCurve RotateSpeed_Yaw;
    [SerializeField] private AnimationCurve RotateSpeed_Pitch;
    [SerializeField] private float smoothRotateTime;

    //Smooth Rotate
    private float deltaTime;
    private bool isSmoothRotate;
    private bool isTriggerSmooth;

    //Yaw
    private float StartYaw;
    private float TargetPitch;
    private float variableYaw;

    //Pitch
    private float StartPitch;
    private float TargetYaw;
    private float variablePitch;

    //Timer
    private bool isSmoothTimer;
    private bool isDelay;
    private float smoothTimer;
    private float delayTimer;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float angle_X;
    private float angle_Y;

    //foolproof
    private bool isToClose;

    private void Start()
    {
        _input = GetComponent<ThirdPersonController>();
    }
    private void Update()
    {
        smoothRotateTimer();
        delayTimerSystem();
        ToSloseCheck();
    }
    private void LateUpdate()
    {
        ToLookTargetCheck();
    }
    public async void ToAimSupport(GameObject Target)
    {
        SetIsDelay(true);

        SetTarget(Target);
        SetIsAimSupport(true);
        await Task.Delay((int)(aimSupportTime * 1000));
        SetIsAimSupport(false);
        SetTarget(null);
        SetIsTriggerSmooth(false);
    }
    public async void ToAimSupport_onlySmooth(GameObject Target)
    {
        SetTarget(Target);
        SetIsAimSupport(true);
        await Task.Delay((int)(smoothRotateTime * 1000));
        SetIsAimSupport(false);
        SetTarget(null);
        SetIsTriggerSmooth(false);
    }
    private void ToLookTargetCheck()
    {
        if(isAimSupport)
        {
            ToLookTarget();

            if(isToClose)
            {
                SetIsAimSupport(false);
            }
        }
    }
    private void smoothRotateTimer()
    {
        if(isSmoothTimer)
        {
            smoothTimer += Time.deltaTime;
            deltaTime = smoothTimer / smoothRotateTime;
        }

        if(smoothTimer >= smoothRotateTime)
        {
            SetIsSmoothTimer(false);
            SetIsSmoothRotate(false);
        }
    }
    private void delayTimerSystem()
    {
        if(isDelay)
        {
            delayTimer += Time.deltaTime;

            if(delayTimer >= delayTime)
            {
                SetIsDelay(false);
            }
        }
    }
    private void ToSloseCheck()
    {
        if(this.target != null)
        {
            Vector3 target = this.target.transform.position;
            Vector3 player = this.transform.position;

            Vector3 target_xz = new Vector3(target.x, 0, target.z);
            Vector3 player_xz = new Vector3(player.x, 0, player.z);

            Vector3 distance = target_xz - player_xz;


            Debug.Log(distance.magnitude);

            if (distance.magnitude < 0.5f)
            {
                SetIsSoClose(true);
            }
            else
            {
                SetIsSoClose(false);
            }
        }
    }
    private void ToLookTarget()
    {
        Vector3 Direction = (target.transform.position - this.transform.position).normalized;

        if (isDelay || isToClose)
        {

        }
        else
        {
            if (!isTriggerSmooth)
            {
                //Initialization
                deltaTime = 0;
                smoothTimer = 0;

                //trigger
                SetIsTriggerSmooth(true);
                SetIsSmoothTimer(true);
                SetIsSmoothRotate(true);

                //Start value
                StartYaw = _input.GetCinemachineTargetYaw();
                StartPitch = _input.GetCinemachineTargetPitch();
            }

            if (isSmoothRotate)
            {
                //Yaw
                angle_X = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg;
                TargetYaw = angle_X + offest_X;
                variableYaw = NormalizeAngle(TargetYaw - StartYaw);

                //Pitch
                angle_Y = Mathf.Asin(-Direction.y) * Mathf.Rad2Deg;
                TargetPitch = angle_Y + offest_Y;
                variablePitch = NormalizeAngle(TargetPitch - StartPitch);

                SmoothRotate_Pitch();
                SmoothRotate_Yaw();
            }
            else
            {
                //Yaw
                angle_X = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg;
                _cinemachineTargetYaw = angle_X + offest_X;
                SetCinemachineTargetYaw(_cinemachineTargetYaw);

                //Pitch
                angle_Y = Mathf.Asin(-Direction.y) * Mathf.Rad2Deg;
                _cinemachineTargetPitch = angle_Y + offest_Y;
                SetCinemachineTargetPitch(_cinemachineTargetPitch);
            }
        }
    }
    private void SmoothRotate_Pitch()
    {
        float deltaPitch = RotateSpeed_Pitch.Evaluate(deltaTime) * variablePitch;
        float Pitch = StartPitch + deltaPitch;
        SetCinemachineTargetPitch(Pitch);
    }
    private void SmoothRotate_Yaw()
    {
        float deltaYaw = RotateSpeed_Yaw.Evaluate(deltaTime) * variableYaw;
        float Yaw = StartYaw + deltaYaw;
        SetCinemachineTargetYaw(Yaw);
    }
    public void SetTarget(GameObject Target)
    {
        target = Target;
    }
    private void SetCinemachineTargetPitch(float value)
    {
        _input.SetCinemachineTargetPitch(value);
    }
    private void SetCinemachineTargetYaw(float value)
    {
        _input.SetCinemachineTargetYaw(value);
    }
    private void SetIsAimSupport(bool value)
    {
         isAimSupport = value;
    }
    private void SetIsSmoothTimer(bool value)
    {
        isSmoothTimer = value;
    }
    private void SetIsTriggerSmooth(bool value)
    {
        isTriggerSmooth = value;
    }
    private void SetIsSmoothRotate(bool value)
    {
        isSmoothRotate = value;
    }
    private void SetIsDelay(bool value)
    {
        isDelay = value;

        if (isDelay)
        {
            delayTimer = 0;
        }
    }
    private void SetIsSoClose(bool value)
    {
        isToClose = value;
    }   
    private float NormalizeAngle(float angle)
    {
        while (angle > 180f)
        {
            angle -= 360f;
        }
        while (angle < -180f)
        {
            angle += 360f;
        }
        return angle;
    }
}
