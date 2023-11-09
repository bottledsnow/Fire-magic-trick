using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class Shooting_Normal : MonoBehaviour
{
    [SerializeField] private MMFeedbacks ThrowFeedback;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private float shootCooldown;
    [Header("Throw Feedbacks")]
    [SerializeField] private bool useThrowFeedbacks;
    [SerializeField] private Transform ThrowParticle;

    private CrosshairUI _crosshairUI;
    private Shooting _shooting;
    private ControllerInput _Input;
    private bool shooting;
    private void Start()
    {
        _Input = GameManager.singleton._input;
        _crosshairUI = GameManager.singleton.UISystem.GetComponent<CrosshairUI>();
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
            ThrowFeedbacksRotation();
            ThrowFeedback.PlayFeedbacks();
            _shooting.Shoot_Normal(pfBulletProjectile);
            _crosshairUI.CrosshairShooting();
            ShootCooldown(shootCooldown);
        }
    }
    private async void ShootCooldown(float shootCooldown)
    {
        shooting = true;
        await Task.Delay((int)(shootCooldown * 1000));
        shooting = false;
    }
    private void ThrowFeedbacksRotation()
    {
        if(useThrowFeedbacks)
        {
            
        }
    }
}
