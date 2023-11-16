using UnityEngine;
using System.Threading.Tasks;
using StarterAssets;
using System.Collections;
using MoreMountains.Feedbacks;

public class FireDash : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime_Max;
    [SerializeField] private float CdTime;
    public float CrashForce;
    public float CrashForceUp;
    [Header("Special Dash")]
    [SerializeField] private ParticleSystem DashEffect_Explode_End;
    [SerializeField] private Vector3 Dash_Good;
    [SerializeField] private float Reaction_time;
    [Header("Crash")]
    [SerializeField] FireDashCollider _fireDashCollider;
    [Header("FeedBacks")]
    [SerializeField] private MMF_Player Feedbacks_Dash_Start;
    [SerializeField] private MMF_Player Feedbacks_Dash_End;
    private Vector3 Dash_Normal;
    private float PressedTime = 0;
    private bool Pressed = false;
    private bool KeepPressed;
    private bool dashedButton = false;
    private bool dashOnCD;
    private bool IsDash;
    private ThirdPersonController _playerController;
    private ControllerInput _Input;
    private CharacterController _characterController;
    private EnergySystem _energySystem;
    private FireDashGhost _fireDashGhost;
    private void Start()
    {
        _Input = GameManager.singleton._input;
        _playerController = _Input.GetComponent<ThirdPersonController>();
        _characterController = _Input.GetComponent<CharacterController>();
        _energySystem = _Input.GetComponent<EnergySystem>();
        _fireDashGhost = GetComponent<FireDashGhost>();
        Dash_Normal = DashEffect_Explode_End.gameObject.transform.localScale;
    }
    private void Update()
    {
        ButtonSystem();
    }
    private void ButtonClickEvent()
    {
        if (!IsDash && !dashOnCD && _Input.LeftStick != Vector2.zero)
        {
            useDash();
        }
    }
    private void useDash()
    {
        bool CanUse;
        _energySystem.UseDash(out CanUse);

        if (CanUse)
        {
            IsDash = true;
            OpenCrash();
            SetDashScale(Dash_Normal);
            Dash_Start();
        }
    }
    private void ButtonPressedEndEvent()
    {
        IsDash = false;
        CloseCrash();
        float time_Start = dashTime_Max - Reaction_time;
        float time_End = dashTime_Max + Reaction_time;
        if (time_Start < PressedTime && PressedTime < time_End)
        {
            SetDashScale(Dash_Good);
        }
    }
    private void ButtonReleaseEvent()
    {
        Dash_TriggerLimit();
    }
    #region Dash Button System
    private void ButtonSystem()
    {
        ButtonTrigger();
        ButtonRelease();
    }

    private void ButtonTrigger()
    {
        if (_Input.ButtonB)
        {
            PressedTime += Time.deltaTime;
            ButtonClick();
            ButtonPressedStart();
        }
    }
    private void ButtonClick()
    {
        if (!Pressed)
        {
            Pressed = true;
            ButtonClickEvent();
        }
    }
    private void ButtonPressedStart()
    {
        if (PressedTime > _Input.PressedSensitivity)
        {
            if (!KeepPressed)
            {
                KeepPressed = true;
                //Button Pressed Start
            }
            //Keep Update
        }
    }
    private void ButtonRelease()
    {
        if (!_Input.ButtonB)
        {
            ButtonReleaseEvent();
            ButtonClickOnly();
            ButtonPressedEnd();
            ButtonInitialization();
        }
    }
    private void ButtonClickOnly()
    {
        if (PressedTime < _Input.PressedSensitivity && PressedTime != 0)
        {
            //Button Click Only
        }
    }
    private void ButtonPressedEnd()
    {
        if (PressedTime > _Input.PressedSensitivity)
        {
            //Button Pressed End
            ButtonPressedEndEvent();
        }
    }
    private void ButtonInitialization()
    {
        Pressed = false;
        KeepPressed = false;
        PressedTime = 0;
    }
    #endregion
    #region Dash Systsem
    private void Dash_Start()
    {
        if (!dashedButton)
        {
            if (!dashOnCD)
            {
                dashedButton = true;
                _fireDashGhost.GhostPlay();
                StartCoroutine(DashMove());
                DashCD(CdTime);
                Dash_MaxKeepTime();
                Feedbacks_Dash_Start.PlayFeedbacks();
            }
        }
    }
    private async void Dash_MaxKeepTime()
    {
        await Task.Delay((int)(dashTime_Max * 1000));
        Dash_End();
    }
    
    
    private void Dash_End()
    {
        IsDash = false;
        _fireDashGhost.GhostStop();
        CloseCrash();
        DashEffect_Explode_End.gameObject.transform.localScale = Dash_Normal;
        Feedbacks_Dash_End.PlayFeedbacks();
    }
    private void Dash_TriggerLimit()
    {
        if (!_Input.ButtonB)
        {
            dashedButton = false;
        }
    }
    private async void DashCD(float CDtime)
    {
        dashOnCD = true;
        await Task.Delay((int)(CDtime * 1000));
        dashOnCD = false;
    }
    private void SetDashScale(Vector3 mode)
    {
        DashEffect_Explode_End.gameObject.transform.localScale = mode;
    }
    IEnumerator DashMove()
    {
        float StartTime = Time.time;

        while (IsDash)
        {
            Vector3 Dir = Quaternion.Euler(0, _playerController.PlayerRotation, 0) * Vector3.forward;
            _characterController.Move(Dir * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
    #endregion
    #region Crash
    private void OpenCrash()
    {
        _fireDashCollider.SetIsDash(true);
    }
    private void CloseCrash()
    {
        _fireDashCollider.SetIsDash(false);
    }
    #endregion
}
