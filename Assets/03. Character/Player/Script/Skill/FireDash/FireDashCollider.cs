using MoreMountains.Feedbacks;
using UnityEngine;

public class FireDashCollider : MonoBehaviour
{
    [SerializeField] private MMF_Player HitFeedbacks;

    //Script
    private Basic_AimSupportSystem _aimSupportSystem;
    private PlayerDamage _playerDamage;
    private VibrationController vibrationController;
    private NGP_Dash dash;

    //value
    private float CrashForce;
    private float CrashForceUp;
    private bool IsDash;
    private bool canTriggerDamage;
    private bool isTriggerDamage;
    private void Start()
    {
        dash = GameManager.singleton.NewGamePlay.GetComponent<NGP_Dash>();
        _aimSupportSystem = GameManager.singleton.Player.GetComponent<Basic_AimSupportSystem>();
        _playerDamage = GetComponent<PlayerDamage>();
        vibrationController = GameManager.singleton.GetComponent<VibrationController>();

        Initialization();
    }
    private void Initialization()
    {
        CrashForce = dash.CrashForce;
        CrashForceUp = dash.CrashForceUp;
    }
    private void OnTriggerEnter(Collider other)
    {
        ToDashHitEnemy(other);
        ToHitGlass(other);
    }
    private void OnTriggerStay(Collider other)
    {
        ToDashHitEnemy(other);
        ToHitGlass(other);
    }
    private void ToDashHitEnemy(Collider other)
    {
        if (IsDash)
        {
            if (canTriggerDamage)
            {
                if (other.CompareTag("Enemy"))
                {
                    vibrationController.Vibrate(0.5f, 0.25f);               

                    canTriggerDamage = false;
                    Vector3 playerposition = transform.parent.transform.position;
                    Vector3 EnemyPosition = other.transform.position;
                    Vector3 direction = transform.parent.transform.forward;
                    Vector3 Enemyup = other.transform.up;

                    if(!isTriggerDamage)
                    {
                        _playerDamage.ToDamageEnemy(other);
                        SetIsTriggerDamage(true);
                    }

                    EnemyHealthSystem enemy = other.GetComponent<EnemyHealthSystem>();
                    enemy.SetAtCrash(true);

                    _aimSupportSystem.ToAimSupport(other.gameObject, _aimSupportSystem.aimSupportTime);
                    HitFeedbacks.PlayFeedbacks();
                    other.GetComponent<Rigidbody>().AddForce(direction * CrashForce + Enemyup * CrashForceUp, ForceMode.Impulse);
                }
            }
        }
    }
    private void ToHitGlass(Collider other)
    {
        if (IsDash)
        {
            if (other.CompareTag("Glass"))
            {
                GlassSystem glass = other.GetComponent<GlassSystem>();
                HitFeedbacks.PlayFeedbacks();
                glass.BrokenCheck_Crash();
            }
        }
    }
    public void SetIsDash(bool value)
    {
        IsDash = value;
        canTriggerDamage = value;
    }
    public void SetIsTriggerDamage(bool active)
    {
        isTriggerDamage = active;
    }
}
