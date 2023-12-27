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
    private float smoothTimer;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float angle_X;
    private float angle_Y;

    private void Start()
    {
        _input = GetComponent<ThirdPersonController>();
    }
    private void Update()
    {
        smoothRotateTimer();
    }
    private void LateUpdate()
    {
        ToLookTargetCheck();
    }
    public async void ToAimSupport(GameObject Target)
    {
        SetTarget(Target);
        SetIsAimSupport(true);
        await Task.Delay((int)(aimSupportTime*1000));
        SetIsAimSupport(false);
        SetTarget(null);
        SetIsTriggerSmooth(false);
    }
    private void ToLookTargetCheck()
    {
        if(isAimSupport)
        {
            ToLookTarget();
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
    private void ToLookTarget()
    {
        Vector3 Direction = (target.transform.position - this.transform.position).normalized;

        if (!isTriggerSmooth)
        {
            //Initialization
            deltaTime = 0;
            smoothTimer = 0;

            //trigger
            SetIsTriggerSmooth(true);
            SetIsSmoothTimer(true);
            SetIsSmoothRotate(true);

            //Yaw
            StartYaw = _input.GetCinemachineTargetYaw();
            angle_X = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg;
            TargetYaw = angle_X + offest_X;
            variableYaw = TargetYaw - StartYaw;
            Debug.Log(variableYaw+":Y");

            //Pitch
            StartPitch = _input.GetCinemachineTargetPitch();
            angle_Y = Mathf.Asin(Direction.y) * Mathf.Rad2Deg;
            TargetPitch = angle_Y + offest_Y;
            variablePitch = TargetPitch - StartPitch;
            Debug.Log(variablePitch+":P");
        }

        if(isSmoothRotate)
        {
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
            angle_Y = Mathf.Asin(Direction.y) * Mathf.Rad2Deg;
            _cinemachineTargetPitch = angle_Y + offest_Y;
            SetCinemachineTargetPitch(_cinemachineTargetPitch);
        }
    }
    private void SmoothRotate_Pitch()
    {
        float deltaPitch = RotateSpeed_Pitch.Evaluate(deltaTime) * variablePitch;
        float Pitch = StartPitch + deltaPitch;
        SetCinemachineTargetPitch(Pitch);
    }private void SmoothRotate_Yaw()
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
    private void SetLockControllerCamera(bool value)
    {
          _input.LockCameraPosition = value;
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
}
