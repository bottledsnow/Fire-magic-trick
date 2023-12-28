using UnityEngine;
using System.Threading.Tasks;

public class SuperDashKickTrigger : MonoBehaviour
{
    [SerializeField] private float TriggerTime;
    private PlayerDamage _playerDamage;
    private AimSupportSystem _aimSupportSystem;

    private float timer;
    private bool isKick;
    private void Awake()
    {
        _playerDamage = GetComponent<PlayerDamage>();
    }
    private void Start()
    {
        _aimSupportSystem = GameManager.singleton.Player.GetComponent<AimSupportSystem>();
    }
    private void Update()
    {
        KickTimer();
    }
    private void OnTriggerStay(Collider other)
    {
        if (isKick)
        {
            if(other.CompareTag("Enemy"))
            {
                _playerDamage.ToDamageEnemy(other);
                _aimSupportSystem.ToAimSupport(other.gameObject);
                isKick = false;
            }
        }
    }
    public void SetTriggerKickCollider(bool active)
    {
        isKick = true;
    }
    private void KickTimer()
    {
        if(isKick)
        {
            timer += Time.deltaTime;
        }
        if(timer > TriggerTime)
        {
            isKick = false;
            timer = 0;
        }
    }
}
