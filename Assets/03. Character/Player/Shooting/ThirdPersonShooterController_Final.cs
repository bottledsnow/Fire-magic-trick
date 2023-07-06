using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;

public class ThirdPersonShooterController_Final : MonoBehaviour
{
    [SerializeField] private MMFeedbacks ThrowFeedback;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private float shootCooldown;

    private ShootingSystem shootingSystem;
    private ControllerInput _Input;
    private bool shooting;
    private bool pistolrate;

    private void Start()
    {
        _Input = GameManager.singleton._input;
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
            shootingSystem.shoot(pfBulletProjectile, EnergySystem.EnergyMode.Shooting);
            ThrowFeedback.PlayFeedbacks();
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
