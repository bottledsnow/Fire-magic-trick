using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class Shooting_Normal : MonoBehaviour
{
    [SerializeField] private MMF_Player ThrowFeedback;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private float shootCooldown;
    [Header("Pisto")]
    [SerializeField] private bool usePistoMode;
    [Header("Throw Feedbacks")]
    [SerializeField] private bool useThrowFeedbacks;
    [SerializeField] private MMF_Player Throw_A;
    [SerializeField] private MMF_Player Throw_B;
    [SerializeField] private MMF_Player Throw_C;

    private PlayerAnimator _playerAnimator;
    private CrosshairUI _crosshairUI;
    private Shooting _shooting;
    private ControllerInput _Input;
    private bool shooting;
    private bool pisto;
    private int ThrowFeedbacksIndex;
    private void Start()
    {
        _Input = GameManager.singleton._input;
        _crosshairUI = GameManager.singleton.UISystem.GetComponent<CrosshairUI>();
        _shooting = GetComponent<Shooting>();
        _playerAnimator = GameManager.singleton.Player.GetComponent<PlayerAnimator>();
    }
    private void Update()
    {
        ShootingSystem();
        PistoSystem();
    }
    private void ShootingSystem()
    {
        if (_Input.RT && !shooting)
        {
            if(usePistoMode)
            {
                if(!pisto)
                {
                    Shooting();
                    pisto = true;
                }
            }else
            {
                Shooting();
            }
        }
    }
    private void PistoSystem()
    {
        if(usePistoMode)
        {
            if (!_Input.RT)
            {
                pisto = false;
            }
        }
    }
    private void Shooting()
    {
        ThrowFeedbacks();
        _playerAnimator.PlayAnimator("Player@Throw_1");

        _shooting.Shoot_Normal(pfBulletProjectile);
        _crosshairUI.CrosshairShooting();
        ShootCooldown(shootCooldown);
    }
    private async void ShootCooldown(float shootCooldown)
    {
        shooting = true;
        await Task.Delay((int)(shootCooldown * 1000));
        shooting = false;
    }
    private void ThrowFeedbacks()
    {
        ThrowFeedback.PlayFeedbacks();

        if (useThrowFeedbacks)
        {
            if(ThrowFeedbacksIndex == 0)
            {
                Throw_A.PlayFeedbacks();
            }
            if (ThrowFeedbacksIndex == 1)
            {
                Throw_B.PlayFeedbacks();
            }
            if (ThrowFeedbacksIndex == 2)
            {
                Throw_C.PlayFeedbacks();
            }

            if (ThrowFeedbacksIndex == 2)
            {
                ThrowFeedbacksIndex = 0;
            }else
            {
                ThrowFeedbacksIndex++;
            }
            
        }
    }
}
