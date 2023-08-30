using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private ControllerInput _Input;
    private EnergySystem energySystem;
    private Shooting_Check _shooting_check;
    private Shooting_Magazing _magazing;
    [Header("Shoot Setting")]
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private float shootingEnergyCost;
    [SerializeField] private float FireEnergyCost;
    private void Start()
    {
        _Input = GameManager.singleton._input;
        _shooting_check = GetComponent<Shooting_Check>();
        energySystem = _Input.GetComponent<EnergySystem>();
        _magazing = GetComponent<Shooting_Magazing>();
    }
    private void Update()
    {
        TurnToShoot();
    }

    public void shoot(Transform preferb)
    {
        Vector3 aimDir = (_shooting_check.mouseWorldPosition - spawnBulletPosition.position).normalized;
        Instantiate(preferb, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
    public void Shoot_Normal(Transform preferb)
    {
        bool canShoot = false;
        energySystem.CunsumeShootingEnergy(shootingEnergyCost, out canShoot);

        if (canShoot)
        {
            shoot(preferb);
        }
        else
        {
            Debug.Log("not enough shooting energy");
        }
    }
    public void Shoot_Fire(Transform preferb)
    {
        bool canShoot_Fire;

        energySystem.ConsumeFireEnergy(FireEnergyCost, out canShoot_Fire);

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
            Vector3 worldAimTarget = _shooting_check.mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 50f);
        }
    }
}
