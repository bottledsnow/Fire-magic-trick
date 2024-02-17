using MoreMountains.Feedbacks;
using UnityEngine;

public class SuperDashCollider : MonoBehaviour
{
    [SerializeField] private MMF_Player HitFeedbacks;
    
    //Script
    private Basic_AimSupportSystem _aimSupportSystem;
    private VibrationController vibrationController;
    private PlayerDamage _playerDamage;
    private SuperDash _superDash;

    //value
    public bool EnemyToClose;
    private float CrashForce;
    private float CrashForceUp;
    private bool IsSuperDash;
    private bool IsSuperDashKick;
    private bool canTrigger;
    private bool isTriggerDamage;
    private bool isCheckAgain;
    private void Start()
    {
        _superDash = GameManager.singleton.EnergySystem.GetComponent<SuperDash>();
        _playerDamage = GetComponent<PlayerDamage>();
        _aimSupportSystem = GameManager.singleton.Player.GetComponent<Basic_AimSupportSystem>();
        vibrationController = GameManager.singleton.GetComponent<VibrationController>();
        Initialization();
    }
    public void ToCloseCheckAgain()
    {
        SetIsCheckAgain(true);
    }
    private void Initialization()
    {
        CrashForce = _superDash.CrashForce;
        CrashForceUp = _superDash.CrashForceUp;
    }
    private void OnTriggerEnter(Collider other)
    {
        ToSuperHitEnemy(other);
    }
    private void OnTriggerStay(Collider other)
    {
        ToSuperHitEnemy(other);
        EnemyStayCheck(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            SetEnemyToClose(false);
        }
    }
    private void ToSuperHitEnemy(Collider other)
    {
        if (IsSuperDash && !IsSuperDashKick)
        {
            if (other.CompareTag("Enemy"))
            {
                if (canTrigger)
                {
                    canTrigger = false;

                    vibrationController.Vibrate(0.5f, 0.25f);

                    if (!isTriggerDamage)
                    {
                        _playerDamage.ToDamageEnemy(other);
                        SetIsTriggerDamage(true);
                    }

                    //Caculate Direction
                    Vector3 playerposition = transform.parent.transform.position;
                    Vector3 EnemyPosition = other.transform.position;
                    Vector3 direction = (EnemyPosition - playerposition).normalized;
                    Vector3 Enemyup = other.transform.up;

                    //atCrash
                    //_aimSupportSystem.ToAimSupport(other.gameObject);
                    HitFeedbacks.PlayFeedbacks();
                    other.GetComponent<Rigidbody>().velocity = direction * CrashForce + Enemyup * CrashForceUp;

                    
                }
            }
        }
    }
    private void EnemyStayCheck(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            SetEnemyToClose(true);
        }
        if(isCheckAgain)
        {
            if(other.CompareTag("Enemy"))
            {
                SetEnemyToClose(true);
            }else
            {
                SetEnemyToClose(false);
            }
        }
    }
    public void SetIsSuperDash(bool value)
    {
        if(value)
        {
            SetIsTriggerDamage(false);
        }

        IsSuperDash = value;
        canTrigger = value;
    }
    public void SetKick(bool value)
    {
        IsSuperDashKick = value;
    }
    public void SetEnemyToClose(bool Value)
    {
        EnemyToClose = Value;
    }
    public void SetIsTriggerDamage(bool active)
    {
        isTriggerDamage = active;
    }
    public void SetIsCheckAgain(bool active)
    {
        isCheckAgain = active;
    }
}
