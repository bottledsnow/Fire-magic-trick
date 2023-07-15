using UnityEngine;
using System.Threading.Tasks;
using StarterAssets;
using System.Collections;
using MoreMountains.Feedbacks;

public class FireDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime_Max;
    [SerializeField] private float CdTime;
    [Header("Special Dash")]
    [SerializeField] private ParticleSystem DashEffect_Explode_End;
    [SerializeField] private Vector3 Dash_Bad;
    [SerializeField] private Vector3 Dash_Normal;
    [SerializeField] private Vector3 Dash_Good;
    [SerializeField] private float Reaction_time;
    public  float PressedTime = 0;
    public  bool Pressed = false;
    public  bool KeepPressed;
    private bool dashedButton = false;
    private bool dashOnCD;
    private bool IsDash;
    private Fire_Teleport _Teleport;
    private ThirdPersonController _playerController;
    private ControllerInput _Input;
    private CharacterController _characterController;
    [Header("FeedBacks")]
    [SerializeField] private MMF_Player Feedbacks_Dash_CD;
    [SerializeField] private MMF_Player Feedbacks_Dash_Start;
    [SerializeField] private MMF_Player Feedbacks_Dash_End;
    private void Start()
    {
        _playerController = GetComponent<ThirdPersonController>();
        _Input = GetComponent<ControllerInput>();
        _characterController = GetComponent<CharacterController>();
        _Teleport = GetComponent<Fire_Teleport>();
    }

    private void Update()
    {
        ButtonSystem();
    }
    #region
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
            SetDashScale(Dash_Normal);
            Pressed = true;
            IsDash = true;
            Dash_Start();
            Debug.Log("Button");
        }
    }
    private void ButtonPressedStart()
    {
        if (PressedTime > _Input.PressedSensitivity)
        {
            if (!KeepPressed)
            {
                KeepPressed = true;
                Debug.Log("Button Pressed Start");
            }
            Debug.Log("Keep Update");
        }
    }
    private void ButtonRelease()
    {
        if (!_Input.ButtonB)
        {
            Dash_TriggerLimit();
            ButtonClickOnly();
            ButtonPressedEnd();
            ButtonInitialization();
        }
    }
    private void ButtonClickOnly()
    {
        if (PressedTime < _Input.PressedSensitivity && PressedTime != 0)
        {
            Debug.Log("Button Click Only");
        }
    }
    private void ButtonPressedEnd()
    {
        if (PressedTime > _Input.PressedSensitivity)
        {
            IsDash = false;
            Debug.Log("Button Pressed End");
            float time_Start = dashTime_Max -Reaction_time;
            float time_End = dashTime_Max + Reaction_time;
            if (time_Start < PressedTime && PressedTime < time_End)
            {
                SetDashScale(Dash_Good);
            }else
            {
                SetDashScale(Dash_Bad);
            }
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
    private async void Dash_MaxKeepTime()
    {
        await Task.Delay((int)(dashTime_Max * 1000));
        Dash_End();
    }
    
    private void Dash_Start()
    {
        if (!dashedButton && _Input.LeftStick != Vector2.zero)
        {
            if (!dashOnCD && !_Teleport.isTeleporting)
            {
                dashedButton = true;
                StartCoroutine(DashMove());
                DashCD(CdTime);
                Dash_MaxKeepTime();
                Feedbacks_Dash_Start.PlayFeedbacks();
            }
        }
    }
    private void Dash_End()
    {
        IsDash = false;
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
        Feedbacks_Dash_CD.PlayFeedbacks();
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
}
