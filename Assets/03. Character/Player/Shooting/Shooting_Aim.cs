using UnityEngine;
using Cinemachine;
using StarterAssets;

public class Shooting_Aim : MonoBehaviour
{
    private ControllerInput _Input;
    private ThirdPersonController thirdPersonController;
    private Vector3 mouseWorldPosition = Vector3.zero;

    [Header("Aim Setting")]
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;


    private void Start()
    {
        _Input = GameManager.singleton._input;
        thirdPersonController = _Input.GetComponent<ThirdPersonController>();
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
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
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
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
        }
    }
}
