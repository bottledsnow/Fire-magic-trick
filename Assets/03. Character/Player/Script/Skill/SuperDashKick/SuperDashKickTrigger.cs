using UnityEngine;
using System.Threading.Tasks;

public class SuperDashKickTrigger : MonoBehaviour
{
    [SerializeField] private float TriggerTime;

    //Script
    private Basic_AimSupportSystem _aimSupportSystem;
    private VibrationController vibrationController;
    private PlayerDamage _playerDamage;

    //value
    private float timer;
    private bool isKick;
    private void Awake()
    {
        _playerDamage = GetComponent<PlayerDamage>();
    }
    private void Start()
    {
        _aimSupportSystem = GameManager.singleton.Player.GetComponent<Basic_AimSupportSystem>();
        vibrationController = GameManager.singleton.GetComponent<VibrationController>();
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
                vibrationController.Vibrate(0.5f, 0.25f);
                _playerDamage.ToDamageEnemy(other);
                _aimSupportSystem.ToAimSupport(other.gameObject, _aimSupportSystem.aimSupportTime);
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
