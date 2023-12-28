using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Camera_Normal;
    [SerializeField] private CinemachineVirtualCamera Camera_Run;
    [SerializeField] private CinemachineVirtualCamera Camera_Aim;
    [SerializeField] private CinemachineVirtualCamera Camera_Death;

    [Header("CameraLookPlayerForward")]
    [SerializeField] private GameObject Target;

    private AimSupportSystem _aimSupportSystem;
    private ControllerInput _input;
    private bool isTriggerButton;

    private void Start()
    {
        _input = GameManager.singleton.Player.GetComponent<ControllerInput>();
        _aimSupportSystem =GameManager.singleton.Player.GetComponent<AimSupportSystem>();
    }

    private void Update()
    {
        ButtonSystem();
    }
    private void ButtonSystem()
    {
        if(!isTriggerButton)
        {
            if (_input.RSB)
            {
                SetIsTriggerButton(true);
                TrunCameraToPlayerForward();
            }
        }

        if(isTriggerButton)
        {
            if (!_input.RSB)
            {
                SetIsTriggerButton(false);
            }
        }
    }
    private void TrunCameraToPlayerForward()
    {
        _aimSupportSystem.ToAimSupport_onlySmooth(Target);
    }

    public void useCamera_Death()
    {
        Camera_Death.gameObject.SetActive(true);
        Camera_Aim.gameObject.SetActive(false);
        Camera_Run.gameObject.SetActive(false);
        Camera_Normal.gameObject.SetActive(false);
    }
    private void SetIsTriggerButton(bool active)
    {
        isTriggerButton = active;
    }
}
