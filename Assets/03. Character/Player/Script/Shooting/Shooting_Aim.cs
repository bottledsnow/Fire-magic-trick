using UnityEngine;
using Cinemachine;
using StarterAssets;

public class Shooting_Aim : MonoBehaviour
{
    private ControllerInput _Input;
    private ThirdPersonController thirdPersonController;
    private PlayerState _playerState;
    private Vector3 mouseWorldPosition = Vector3.zero;

    [Header("Aim Setting")]
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity_x;
    [SerializeField] private float normalSensitivity_y;
    [SerializeField] private float aimSensitivity_x;
    [SerializeField] private float aimSensitivity_y;



    private void Start()
    {
        _Input = GameManager.singleton._input;
        thirdPersonController = _Input.GetComponent<ThirdPersonController>();
        _playerState = GameManager.singleton.Player.GetComponent<PlayerState>();
    }

    private void Update()
    {
        Aim();
    }
    private void Aim()
    {
        AimTrigger();
        AimClose();
    }
    private void AimTrigger()
    {
        if (_Input.LT)
        {
            _playerState.TurnToAimDirection();
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity_x,aimSensitivity_y);
            //thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(aimDirection, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 20f);
        }
    }
    private void AimClose()
    {
        if (!_Input.LT) 
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity_x,normalSensitivity_y);
            thirdPersonController.SetRotateOnMove(true);
        }
    }
}
