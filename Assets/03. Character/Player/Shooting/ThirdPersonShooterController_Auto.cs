using UnityEngine;
using Cinemachine;
using StarterAssets;
using System.Threading.Tasks;
using UnityEngine.ProBuilder;

public class ThirdPersonShooterController_Auto : MonoBehaviour
{
    [SerializeField] private float shootCooldown;
    [SerializeField] private Transform pfBulletProjectile;
    private ShootingSystem shootingSystem;
    private ControllerInput _Input;
    private bool shooting;
    private void Start()
    {
        _Input = GetComponent<ControllerInput>();
        shootingSystem = GetComponent<ShootingSystem>();
    }
    private void Update()
    {
        Shooting();
    }
    private void Shooting()
    {
        if (_Input.RT && !shooting)
        {
            ShootCooldown(shootCooldown);
            shootingSystem.shoot(pfBulletProjectile);
        }
    }
    private async void ShootCooldown(float shootCooldown)
    {
        shooting = true;
        await Task.Delay((int)(shootCooldown *1000));
        shooting = false;
    }
}
