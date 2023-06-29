using UnityEngine;
using Cinemachine;
using StarterAssets;
using System.Threading.Tasks;
using UnityEngine.ProBuilder;

public class ThirdPersonShooterController_Charge : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    [Header("Controller")]
    [SerializeField] private ControllerInput _Input;
    private ThirdPersonController thirdPersonController;

    [Header("Shooting Setting")]
    [SerializeField] private float shootCooldown;
    private bool shooting;
    private bool pistolrate;
    //Charge
    [Header("ChargePreferb")]
    [SerializeField] private Transform pfChargeProjectile;
    [SerializeField] private ParticleSystem ChargePartical;
    private bool triggerParticle;
    private bool Charge;
    private bool CanCharge;
    private float chargePower;

    [SerializeField] private float MaxChargePower;

    //encapsulation
    private Vector3 mouseWorldPosition = Vector3.zero;


    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        TurnToShoot();
        ShootRay();
        Shooting();
        ChargeSystem();
        Aim();
        pistol();
    }
    #region Charge
    private void ChargeSystem()
    {
       ChargeInput();
       ChargePower();
       ChargeRelease();
    }
    private void ChargeRelease()
    {
        if(!_Input.RT)
        {
            if(CanCharge)
            {
                ChargeShoot();
                CanCharge = false;
            }
            Debug.Log("¼¯²»‰ò¶àšâ");
        }
    }
    private void ChargeShoot()
    {
        Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
        Instantiate(pfChargeProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
    private void ChargeInput()
    {
        if (_Input.RT) Charge = true;
        if (!_Input.RT) Charge = false;
    }

    private void ChargePower()
    {
        if(Charge)
        {
            if(!triggerParticle)
            {
                triggerParticle = true;
                ChargePartical.Play();
            }
            chargePower += Time.deltaTime;
            if(chargePower > MaxChargePower) CanCharge = true;
        } else
        {
            chargePower = 0;
            if(triggerParticle)
            {
                ChargePartical.Stop();
                triggerParticle = false;
            }
        }
    }
    #endregion
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
    private void Shooting()
    {
        if (_Input.RT && !shooting && !pistolrate)
        {
            ShootCooldown(shootCooldown);
            pistolrate = true;
            shoot();
        }
    }
    private void shoot()
    {
        Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
        Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
    private void pistol()
    {
        if (!_Input.RT) pistolrate = false;
    }
    private async void ShootCooldown(float shootCooldown)
    {
        shooting = true;
        await Task.Delay((int)(shootCooldown *1000));
        shooting = false;
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
        if (_Input.RB)
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
        if (!_Input.RB)
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
        }
    }
    #endregion
}
