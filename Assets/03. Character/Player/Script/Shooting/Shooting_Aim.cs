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

    public bool isAiming;


    private void Start()
    {
        _Input = GameManager.singleton._input;
        thirdPersonController = _Input.GetComponent<ThirdPersonController>();
        _playerState = GameManager.singleton.Player.GetComponent<PlayerState>();
    }

    private void Update()
    {
        aimCheck();
    }
    private void aimCheck()
    {
        if(_Input.LT)
        {
            if(!isAiming)
            {
                SetisAiming(true);
                aimOpen();
                Debug.Log("Aim Open");
            }
        }
        else
        {
            if(isAiming)
            {
                SetisAiming(false);
                aimClose();
                Debug.Log("Aim Close");
            }
        }
    }
    private void aimOpen()
    {
        //Initialization
        aimVirtualCamera.gameObject.SetActive(true);
        thirdPersonController.SetSensitivity(aimSensitivity_x, aimSensitivity_y);

        //Calculate Aim Direction
        Vector3 worldAimTarget = mouseWorldPosition;
        worldAimTarget.y = transform.position.y;
        Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(aimDirection, transform.up);

        //set
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 20f);
    }
    private void aimClose()
    {
        aimVirtualCamera.gameObject.SetActive(false);
        thirdPersonController.SetSensitivity(normalSensitivity_x, normalSensitivity_y);
        thirdPersonController.SetRotateOnMove(true);
    }
    private void SetisAiming(bool value)
    {
        isAiming = value;
    }
}
