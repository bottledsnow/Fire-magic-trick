using MoreMountains.Feedbacks;
using UnityEngine;

public class SuperDashCollider : MonoBehaviour
{
    [SerializeField] private MMF_Player HitFeedbacks;
    private SuperDash _superDash;
    private PlayerDamage _playerDamage;
    private float CrashForce;
    private float CrashForceUp;
    private bool IsSuperDash;
    private bool IsSuperDashKick;
    private bool canTrigger;
    private bool isTriggerDamage;
    public bool EnemyToClose;
    private void Start()
    {
        _superDash = GameManager.singleton.EnergySystem.GetComponent<SuperDash>();
        _playerDamage = GetComponent<PlayerDamage>();
        Initialization();
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
            EnemyToClose = false;
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
                    Debug.Log("SuperDash Damage");
                    canTrigger = false;
                    Vector3 playerposition = transform.parent.transform.position;
                    Vector3 EnemyPosition = other.transform.position;
                    Vector3 direction = (EnemyPosition - playerposition).normalized;
                    Vector3 Enemyup = other.transform.up;
                    HitFeedbacks.PlayFeedbacks();
                    other.GetComponent<Rigidbody>().velocity = direction * CrashForce + Enemyup * CrashForceUp;

                    if(!isTriggerDamage)
                    {
                        _playerDamage.ToDamageEnemy(other);
                        SetIsTriggerDamage(true);
                    }
                }
            }
        }
    }
    private void EnemyStayCheck(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyToClose = true;
            Debug.Log("Comaper");
        }
    }
    public void SetIsSuperDash(bool value)
    {
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
}
