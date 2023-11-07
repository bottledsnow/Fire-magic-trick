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
        if (IsSuperDash && !IsSuperDashKick)
        {
            if (other.CompareTag("Enemy"))
            {
                if(canTrigger)
                {
                    Debug.Log("SuperDash Damage");
                    canTrigger = false;
                    Vector3 playerposition = transform.parent.transform.position;
                    Vector3 EnemyPosition = other.transform.position;
                    Vector3 direction = (EnemyPosition - playerposition).normalized;
                    Vector3 Enemyup = other.transform.up;
                    HitFeedbacks.PlayFeedbacks();
                    other.GetComponent<Rigidbody>().velocity = direction * CrashForce + Enemyup * CrashForceUp;
                    _playerDamage.ToDamageEnemy(other);
                }
            }
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
}
