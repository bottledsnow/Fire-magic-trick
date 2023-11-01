using MoreMountains.Feedbacks;
using UnityEngine;

public class SuperDashCollider : MonoBehaviour
{
    [SerializeField] private MMF_Player HitFeedbacks;
    private SuperDash _superDash;
    private SuperDashKick _superDashKick;
    private PlayerDamage _playerDamage;
    private float CrashForce;
    private float CrashForceUp;
    private bool IsSuperDash;
    private bool SuperDashHit;
    private bool IsSuperDashKick;
    private void Start()
    {
        _superDash = GameManager.singleton.EnergySystem.GetComponent<SuperDash>();
        _superDashKick = GameManager.singleton.EnergySystem.GetComponent <SuperDashKick>();
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
                Debug.Log("Hit");
                SuperDashHit = true;
                Vector3 playerposition = transform.parent.transform.position;
                Vector3 EnemyPosition = other.transform.position;
                Vector3 direction = (EnemyPosition - playerposition).normalized;
                Vector3 Enemyup = other.transform.up;
                HitFeedbacks.PlayFeedbacks();
                other.GetComponent<Rigidbody>().velocity = direction * CrashForce + Enemyup * CrashForceUp;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(SuperDashHit && !IsSuperDashKick)
        {
            SuperDashHit = false;
            _playerDamage.ToDamageEnemy(other);
        }
    }   
    public void SetIsSuperDash(bool value)
    {
        IsSuperDash = value;
    }
    public void SetKick(bool value)
    {
        IsSuperDashKick = value;
    }
}
