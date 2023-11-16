using UnityEngine;

public class EnemyFireSystem : MonoBehaviour
{
    [SerializeField] private GameObject TrackTarget;
    [SerializeField] private GameObject DashFire;
    [Header("Setting")]
    [SerializeField] private float spreadTimer;


    private EnemyHealthSystem _health;

    private float timerNumber;
    private bool isSpread;
    private bool isTimer;

    private void Awake()
    {
        _health = GetComponent<EnemyHealthSystem>();
    }
    private void Update()
    {
        TrackTargetSystem();
        SpreadTimerSystem();
    }
    public void FireCheck(PlayerDamage.DamageType damageType)
    {
        if(damageType == PlayerDamage.DamageType.FireDash)
        {
            SetIsSpread(true);
            SetDashFire(true);
            SetTrackTarget(true);
            SetIsTimer(true);
            SetTimerNumber(spreadTimer);
        }
    }
    private void TrackTargetSystem()
    {
        if (!isSpread)
        {
            if (_health.isFire)
            {
                SetTrackTarget(true);
            }
            else
            {
                SetTrackTarget(false);
            }
        }
    }
    private void SpreadTimerSystem()
    {
        if(isTimer)
        {
            timerNumber -= Time.deltaTime;
        }

        if(timerNumber <=0 && isTimer)
        {
            SetIsTimer(false);
            SetTimerNumber(0);
            SetIsSpread(false);
            SetDashFire(false);
        }
    }
    private void SetIsTimer(bool active)
    {
        isTimer = active;
    }
    private void SetTimerNumber(float numbber)
    {
        timerNumber = numbber;
    }
    private void SetDashFire(bool active)
    {
        DashFire.SetActive(active);
    }
    private void SetIsSpread(bool active)
    {
        isSpread = active;
    }
    private void SetTrackTarget(bool active)
    {
        TrackTarget.SetActive(active);
    }
}
