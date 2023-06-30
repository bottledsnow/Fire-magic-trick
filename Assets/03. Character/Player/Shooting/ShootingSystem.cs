using UnityEngine;
using StarterAssets;
using Cinemachine;

public class ShootingSystem : MonoBehaviour
{
    [Header("Shoot Setting")]
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform spawnBulletPosition;

    [Header("Aim Setting")]
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;

    private ControllerInput _Input;
    private ThirdPersonController thirdPersonController;
    private Vector3 mouseWorldPosition = Vector3.zero;
    private void Start()
    {
        _Input = GetComponent<ControllerInput>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }
    private void Update()
    {
        ShootRay();
        TurnToShoot();
    }
    #region Shoot
    private void ShootRay()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
    }
    public void shoot(Transform preferb)
    {
        Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
        Instantiate(preferb, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
    
    private void TurnToShoot()
    {
        if (_Input.RT)
        {
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 50f);
        }
    }
    #endregion
    #region Aim
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
            thirdPersonController.SetRotateOnMove(false);

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
    #endregion
}
