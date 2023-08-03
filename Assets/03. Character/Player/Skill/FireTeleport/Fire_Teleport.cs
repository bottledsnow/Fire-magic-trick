using StarterAssets;
using UnityEngine;
using System.Threading.Tasks;
using Cinemachine;
using MoreMountains.Feedbacks;

public class Fire_Teleport : MonoBehaviour
{
    public int TeleportDamage;
    [SerializeField] MMFeedbacks TeleportFeedback;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float TeleportCost;
    [SerializeField] private float TeleporCameraDamping;
    [Header("OutControll")]
    [SerializeField] private int OutControll_ms_normal;
    [SerializeField] private int OutControll_ms_max;
    private int OutControll_ms;
    [Header("Feedbacks")]
    [SerializeField] private SpeedCameraParticle speedCameraParticle;
    [SerializeField] private ParticleSystem InAirEffect;
    [SerializeField] private MMF_Player InAirFeedbacks_Start;
    [SerializeField] private MMF_Player InAirFeedbacks_Stop;
    private EnergySystem _energySystem;
    private CameraSystem _cameraSystem;
    private ControllerInput _Input;
    private ThirdPersonController _PlayerControl;
    private FireCheck _fireCheck;
    private ParticleSystem.EmissionModule emissionModule;
    private Vector3 oldCameraDamping;
    private bool canTeleport;
    private Transform player;

    [HideInInspector] public bool isTeleporting;
    //Button System
    private float PressedTime = 0;
    private bool Pressed = false;
    private bool KeepPressed;

    private void Update()
    {
        ButtonSystem();
    }
    private void Start()
    {
        _Input = GameManager.singleton._input;
        _energySystem = _Input.GetComponent<EnergySystem>();
        _cameraSystem = _Input.GetComponent<CameraSystem>();
        _PlayerControl = _Input.GetComponent<ThirdPersonController>();
        _fireCheck = Camera.main.GetComponent<FireCheck>();
        player = _Input.transform;
        oldCameraDamping = playerCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping;
        emissionModule = InAirEffect.emission;
    }
    
    #region Button System
    private void ButtonSystem()
    {
        ButtonTrigger();
        ButtonRelease();
    }

    private void ButtonTrigger()
    {
        if (_Input.LB)
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
            FireTeleport();
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
                FireInAir();
            }
            Debug.Log("Keep Update");
        }
    }
    private void ButtonRelease()
    {
        if (!_Input.LB)
        {
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
            OutControll_ms = OutControll_ms_normal;
        }
    }
    private void ButtonPressedEnd()
    {
        if (PressedTime > _Input.PressedSensitivity)
        {
            Debug.Log("Button Pressed End");
            PlayerJump();
            InAirFeedbacks_Stop.PlayFeedbacks();
            OutControll_ms = OutControll_ms_normal;
        }
    }

    private void ButtonInitialization()
    {
        isTeleporting = false;
        Pressed = false;
        KeepPressed = false;
        PressedTime = 0;
    }
    #endregion
    private void FireTeleport()
    {
        if (_fireCheck.isChoosePoint && _fireCheck.FirePoint != null)
        {
            _energySystem.ConsumeFireEnergy(TeleportCost, out canTeleport);
            if (canTeleport)
            {
                SetOutControll(OutControll_ms_normal);
                SetSpeedParticle();
                isTeleporting = true;
                _fireCheck.isChoosePoint = false;
                TranslateSystem();
                ToDamageAround();
                _fireCheck.AbsorbFire();
                _fireCheck.DestroyFirePoint();
            }
            else
            {
                Debug.Log("Not enough Fire Energy");
            }
        }
    }
    private void SetOutControll(int ms)
    {
        OutControll_ms = ms;
    }
    private void SetSpeedParticle()
    {
        speedCameraParticle.OpenParticle();
        speedCameraParticle.CloseParticle(500);
    }
    private void ToDamageAround()
    {
        TeleportFeedback.PlayFeedbacks();
    }
    private void FireInAir()
    {
        if (isTeleporting)
        {
            InAirFeedbacks_Start.PlayFeedbacks();
            Debug.Log("Button Pressed Start");
            OutControll_ms = OutControll_ms_max;
        }
    }
    private void PlayerJump()
    {
        Debug.Log("Player Jump");
        _PlayerControl.Jump();
    }
    #region TranslateSystem
    private void TranslateSystem()
    {
        TranslateStart();
        TranslateWaiting();
    }
    private void TranslateStart()
    {
        //_cameraSystem.ToTeleportCamera();
        SetTeleportCameraDamping();
        _PlayerControl.outControl = true;
        player.transform.position = _fireCheck.FirePoint.position;
    }
    private async void TranslateWaiting()
    {
        float time_ms = 0;
        while (true)
        {
            await Task.Delay(100);
            time_ms += 100;
            emissionModule.rateOverTimeMultiplier += 100;
            if (time_ms > OutControll_ms || time_ms > 10000)
            {
                TranslateEnd();
                break;
            }
        }
    }
    private void TranslateEnd()
    {
        revertTeleportCameraDamping();
        emissionModule.rateOverTimeMultiplier = 0;
        _PlayerControl.outControl = false;
    }
    private void SetTeleportCameraDamping()
    {
        Vector3 newCameraDamping = new Vector3(TeleporCameraDamping, TeleporCameraDamping, TeleporCameraDamping);
        playerCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping = newCameraDamping;
    }
    private void revertTeleportCameraDamping()
    {
        playerCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping = oldCameraDamping;
    }
    #endregion
}
