using MoreMountains.Feedbacks;
using UnityEngine;

public class FireDashCollider : MonoBehaviour
{
    [SerializeField] private FireDash _fireDash;
    [SerializeField] private MMF_Player HitFeedbacks;
    private float CrashForce;
    private bool IsDash;
    private bool canTriggerDamage;
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
        if (IsDash)
        {
            if(canTriggerDamage)
            {
                if (other.CompareTag("Enemy"))
                {
                    canTriggerDamage = false;
                    Vector3 playerposition = transform.parent.transform.position;
                    Vector3 EnemyPosition = other.transform.position;
                    Vector3 direction = (EnemyPosition - playerposition).normalized;
                    Vector3 Enemyup = other.transform.up;
                    _playerDamage.ToDamageEnemy(other);
                    HitFeedbacks.PlayFeedbacks();
                    other.GetComponent<Rigidbody>().velocity = direction * CrashForce + Enemyup * CrashForceUp;
                }
            }
        }
    }
    public void SetIsDash(bool value)
    {
        IsDash = value;
        canTriggerDamage = value;
    }
}
