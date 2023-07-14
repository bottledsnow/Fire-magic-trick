using StarterAssets;
using UnityEngine;
using System.Threading.Tasks;
using Cinemachine;
using MoreMountains.Feedbacks;
using UnityEngine.ProBuilder.MeshOperations;

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
    [SerializeField] private int OutControll_ms;
    [Header("Feedbacks")]
    [SerializeField] private ParticleSystem InAirEffect;
    [SerializeField] private MMF_Player InAirFeedbacks_Start;
    [SerializeField] private MMF_Player InAirFeedbacks_Stop;
    private EnergySystem energySystem;
    private ControllerInput _Input;
    private ThirdPersonController _PlayerControl;
    private FireCheck_Easy fireCheck;
    private ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule;
    private ParticleSystem.EmissionModule emissionModule;
    private Vector3 oldCameraDamping;
    private bool canTeleport;

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
        energySystem = GetComponent<EnergySystem>();
        oldCameraDamping = playerCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping;
        _Input = GameManager.singleton._input;
        _PlayerControl = GetComponent<ThirdPersonController>();
        fireCheck = Camera.main.GetComponent<FireCheck_Easy>();
        velocityOverLifetimeModule = InAirEffect.velocityOverLifetime;
        emissionModule = InAirEffect.emission;
    }
    private void FireTeleport()
    {
        if (fireCheck.isChooseFirePoint && fireCheck.FirePoint != null)
        {
            energySystem.ConsumeFireEnergy(TeleportCost, out canTeleport);
            if (canTeleport)
            {
                fireCheck.isChooseFirePoint = false;
                TeleportFeedback.PlayFeedbacks();
                TranslateSystem();
                fireCheck.AbsorbFire();
                fireCheck.DestroyFirePoint();
            }
            else
            {
                Debug.Log("Not enough Fire Energy");
            }
        }
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
            OutControll_ms = OutControll_ms_normal;
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
                InAirFeedbacks_Start.PlayFeedbacks();
                KeepPressed = true;
                Debug.Log("Button Pressed Start");
                OutControll_ms = OutControll_ms_max;
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
            InAirFeedbacks_Stop.PlayFeedbacks();
            OutControll_ms = OutControll_ms_normal;
        }
    }

    private void ButtonInitialization()
    {
        Pressed = false;
        KeepPressed = false;
        PressedTime = 0;
    }
    #endregion
    #region TranslateSystem
    private void TranslateSystem()
    {
        TranslateStart();
        TranslateWaiting();
    }
    private void TranslateStart()
    {
        SetTeleportCameraDamping();
        _PlayerControl.outControl = true;
        transform.position = fireCheck.FirePoint.position;
    }
    private async void TranslateWaiting()
    {
        float time_ms = 0;
        while (true)
        {
            await Task.Delay(100);
            time_ms += 100;
            velocityOverLifetimeModule.radialMultiplier +=0.5f;
            emissionModule.rateOverTimeMultiplier += 50;
            if (time_ms > OutControll_ms || time_ms > 10000)
            {
                TranslateEnd();
                break;
            }
        }
    }
    private void TranslateEnd()
    {
        velocityOverLifetimeModule.radialMultiplier = 0;
        emissionModule.rateOverTimeMultiplier = 0;
        _PlayerControl.outControl = false;
        revertTeleportCameraDamping();
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
