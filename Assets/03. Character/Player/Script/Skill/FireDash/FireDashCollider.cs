using MoreMountains.Feedbacks;
using UnityEngine;

public class FireDashCollider : MonoBehaviour
{
    [SerializeField] private FireDash _fireDash;
    [SerializeField] private MMF_Player HitFeedbacks;
    private float CrashForce;
    private bool IsDash;
    private bool canTriggerDamage;
    private bool isTriggerDamage;
    private float CrashForceUp;
    private PlayerDamage _playerDamage;
    private void Start()
    {
        _fireDash = GameManager.singleton.EnergySystem.GetComponent<FireDash>();
        _playerDamage = GetComponent<PlayerDamage>();
        Initialization();
    }
    private void Initialization()
    {
        CrashForce = _fireDash.CrashForce;
        CrashForceUp = _fireDash.CrashForceUp;
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
