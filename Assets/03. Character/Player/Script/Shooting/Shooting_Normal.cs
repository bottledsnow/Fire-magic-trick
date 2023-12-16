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
    private Shooting_Magazing _shooting_magazing;
    private ControllerInput _Input;
    private PlayerState _playerState;
    private CrosshairUI _crosshairUI;
    private Shooting _shooting;
    private bool shooting;
    private bool pisto;
    private int ThrowFeedbacksIndex;
    private void Start()
    {
        _Input = GameManager.singleton._input;
        _crosshairUI = GameManager.singleton.UISystem.GetComponent<CrosshairUI>();
        _playerAnimator = GameManager.singleton.Player.GetComponent<PlayerAnimator>();
        _playerState = GameManager.singleton.Player.GetComponent<PlayerState>();
        _shooting = GetComponent<Shooting>();
        _shooting_magazing = GetComponent<Shooting_Magazing>();
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
        ShootCooldown(shootCooldown);
        _shooting.Shoot_Normal(pfBulletProjectile);

        if (_shooting_magazing.Bullet <= 0 )
        {
            return;
        }

        ThrowFeedbacks();
        _playerAnimator.PlayAnimator("Player@Throw_1");
        _crosshairUI.CrosshairShooting();
        _playerState.TurnToAimDirection(10f);
        _playerState.TurnToAimDirection(10f);
        _playerState.TurnToAimDirection(10f);
        _playerState.TurnToAimDirection(10f);
        _playerState.TurnToAimDirection(10f);
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
