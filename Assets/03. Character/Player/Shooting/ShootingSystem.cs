using UnityEngine;
using StarterAssets;
using Cinemachine;
using MoreMountains.Feedbacks;

public class ShootingSystem : MonoBehaviour
{
    [Header("Shooting System Component")]
    [SerializeField] private ThirdPersonShooterController_Final _Shooting;
    [SerializeField] private ThirdPersonShooterController_Charge _ShootingCharge;
    [Header("Shoot Setting")]
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private float shootingEnergyCost;
    [SerializeField] private float FireEnergyCost;
    [SerializeField] private float MaxShootDistance;

    [Header("Aim Setting")]
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;

    private ControllerInput _Input;
    private ThirdPersonController thirdPersonController;
    private EnergySystem energySystem;
    private Vector3 mouseWorldPosition = Vector3.zero;
    private void Start()
    {
        _Input = GetComponent<ControllerInput>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        energySystem = GetComponent<EnergySystem>();
    }
    private void Update()
    {
        Aim();
        ShootRay();
        TurnToShoot();
    }
    #region ShootEnergy
    
    #endregion
    #region Shoot
    private void ShootRay()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        RaycastHit hit;
        bool raycastHit = Physics.Raycast(ray, out hit, 999f, aimColliderLayerMask);

        if (raycastHit)
        {
            debugTransform.position = hit.point;
            mouseWorldPosition = hit.point;
        }
        else
        {
            // 射?未命中時，根據射?方向和距離設置 debugTransform 的位置
            debugTransform.position = ray.origin + ray.direction * MaxShootDistance;
            mouseWorldPosition = debugTransform.position;
        }
    }
    public void shoot(Transform preferb)
    {
        Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
        Instantiate(preferb, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
    public void Shoot_Normal(Transform preferb)
    {
        bool canShoot = false;
        energySystem.CunsumeShootingEnergy(shootingEnergyCost,out canShoot);

        if(canShoot)
        {
            shoot(preferb);
        }else
        {
            Debug.Log("not enough shooting energy");
        }
    }
    public void Shoot_Fire(Transform preferb)
    {
        bool canShoot_Fire;

        energySystem.ConsumeFireEnergy(FireEnergyCost,out canShoot_Fire);

        if (canShoot_Fire)
        {
            shoot(preferb);
            Debug.Log("PlayerShootFire");
        }
        else
        {
            Debug.Log("not enough Fire energy");
        }
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
