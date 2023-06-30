using UnityEngine;
using Cinemachine;
using StarterAssets;
using System.Threading.Tasks;
using UnityEngine.ProBuilder;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private float shootCooldown;
    [SerializeField] private Transform pfBulletProjectile;
    private bool shooting;
    private bool pistolrate;
    private ControllerInput _Input;
    private ShootingSystem shootingSystem;

    private void Start()
    {
        _Input = GetComponent<ControllerInput>();
        shootingSystem = GetComponent<ShootingSystem>();
    }

    private void Update()
    {
        Shooting();
        pistol();
    }
    private void Shooting()
    {
        if (_Input.RT && !shooting && !pistolrate)
        {
            ShootCooldown(shootCooldown);
            pistolrate = true;
            shootingSystem.shoot(pfBulletProjectile);
        }
    }
    private void pistol()
    {
        if (!_Input.RT) pistolrate = false;
    }
    private async void ShootCooldown(float shootCooldown)
    {
        shooting = true;
        await Task.Delay((int)(shootCooldown * 1000));
        shooting = false;
    }
}
