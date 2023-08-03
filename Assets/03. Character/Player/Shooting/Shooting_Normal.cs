using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;

public class Shooting_Normal : MonoBehaviour
{
    [SerializeField] private MMFeedbacks ThrowFeedback;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private float shootCooldown;

    private Shooting _shooting;
    private ControllerInput _Input;
    private bool shooting;

    private void Start()
    {
        _Input = GameManager.singleton._input;
        _shooting = GetComponent<Shooting>();
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
            _shooting.Shoot_Normal(pfBulletProjectile);
            ThrowFeedback.PlayFeedbacks();
        }
    }
    private async void ShootCooldown(float shootCooldown)
    {
        shooting = true;
        await Task.Delay((int)(shootCooldown * 1000));
        shooting = false;
    }
}
