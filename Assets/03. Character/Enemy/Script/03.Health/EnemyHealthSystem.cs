using MoreMountains.Feedbacks;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class EnemyHealthSystem : MonoBehaviour, IHealth
{
    

    [SerializeField] private bool isTeachEnemy;
    [SerializeField] private bool isRebirthHide;
    [Header("State")]
    public bool isIgnite;
    public bool isHurt;
    public bool isSteam;
    public bool isFire;
    public bool isShock;
    public bool Boom;

    [Header("Health")]
    [SerializeField] private int StartHealth;
    public int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int ignitionPoint;

    [Header("Cooling")]
    [SerializeField] private float coolingInterval;
    [SerializeField] private float coolingTime;

    [Header("Feedbacks")]
    [SerializeField] private EyeColorController _eye;
    [SerializeField] private MMF_Player feedbacks_Steam;
    [SerializeField] private MMF_Player feedbacks_Fire;
    [SerializeField] private MMF_Player feedbacks_Shock;
    [SerializeField] private MMF_Player feedbacks_Boom;
    [SerializeField] private MMF_Player feedbacks_FlyBoom;
    [SerializeField] private GameObject VFX_Death;

    [Header("KickBack")]
    public float kickBackRatio;
    public bool kickBackGuard = false;

    [Header("Spread Area")]
    [SerializeField] private GameObject spreadArea;

    [Header("AtCrash")]
    public bool atCrash;
    [SerializeField] private float atCrashTime =3;

    //Script
    private BehaviorTree bt;
    private ProgressSystem _progress;
    private Transform startPosition;
    private EnemyFireSystem _fireSystem;
    private Vector3 StartPosition;
    private Quaternion StartRotation;

    //event
    public delegate void ToPlayEnemyHit();
    public event ToPlayEnemyHit OnEnemyHit;
    public delegate void EnemyDeath();
    public event EnemyDeath OnEnemyDeath;

    //variable
    private float atCrashTimer;
    private float hitTimer;
    private float coolingTimer;
    private bool isCooling;
    private bool isInterval;
    private bool isTriggerDeath;

    public int iHealth
    {
        get { return health; }
        set { health = value; }
    }

    private void Awake()
    {
        _fireSystem = GetComponent<EnemyFireSystem>();
    }
    private void Start()
    {
        health = maxHealth;
        _progress = GameManager.singleton.GetComponent<ProgressSystem>();
        bt = GetComponent<BehaviorTree>();
        startPosition = this.transform;
        StartPosition = this.transform.position;
        StartRotation = this.transform.rotation;
        if (isTeachEnemy == false)
        {
            RebirthScription();
        }  
    }

    private void Update()
    {
        EnemyCoolingCheck();
        atCrashTimerSystem();
    }
    public void giveTargetPlayer()
    {
        GameObject player = GameManager.singleton.Player.gameObject;
        EnemyAggroSystem enemyAggroSystem = GetComponent<EnemyAggroSystem>();
        if (enemyAggroSystem != null) enemyAggroSystem.GiveAggroTarget(player);
    }
    private void atCrashTimerSystem()
    {
        if(atCrash)
        {
            atCrashTimer += Time.deltaTime;
        }

        if(atCrashTimer>atCrashTime)
        {
            SetAtCrash(false);
            atCrashTimer = 0;
        }
    }
    #region Cooling
    private void EnemyCoolingCheck()
    {
        isCooling = Time.time - hitTimer > coolingTime ? true : false;
        isInterval = Time.time - coolingTimer > coolingInterval ? true : false;

        if (health < maxHealth && isCooling && isInterval)
        {
            EnemyCooling();
        }
    }
    private void EnemyCooling()
    {
        health++;
        coolingTimer = Time.time;
        healthFeedback(health);
    }
    #endregion
    #region Damage
    public void TakeDamage(int damage , PlayerDamage.DamageType damageType)
    {
        health -= damage;
        hitTimer = Time.time;

        healthFeedback(health);

        if(!kickBackGuard)
        {
            if (damageType == PlayerDamage.DamageType.NormalShoot || damageType == PlayerDamage.DamageType.ChargeShoot)
            {
                bt.SendEvent("HitByPlayer");
            }
        }

        OnEnemyHit?.Invoke();

        if (_fireSystem != null)
        {
            _fireSystem.FireCheck(damageType);
        }

        if (health <= ignitionPoint)
        {
            EnemyIgnite();
        }
    }
    private void EnemyIgnite()
    {
        isIgnite = true;
    }
    #endregion
    #region Feedback
    private void healthFeedback(int health)
    {
        if(health == 6)
        {
            _eye.SetYellow();

            if(isHurt)
            {
                isHurt = false;
            }
        }
        if (health == 5) 
        {
            isHurt = true;

            _eye.SetOrange();

            if(isSteam)
            {
                isSteam = false;
                feedbacks_Steam.StopFeedbacks();
            }
        }
        if (health == 4)
        {
            isSteam = true;

            _eye.SetRed();
            feedbacks_Steam.PlayFeedbacks();
        }
        if (health == 3)
        {
            _eye.SetPurple();

            if(!isSteam)
            {
                isSteam = true;
                feedbacks_Steam.PlayFeedbacks();
            }

            if(isFire)
            {
                isFire = false;
                feedbacks_Steam.PlayFeedbacks();
                feedbacks_Fire.StopFeedbacks();
            }
        }
        if (health == 2)
        {
            isFire = true;

            feedbacks_Steam.StopFeedbacks();
            feedbacks_Fire.PlayFeedbacks();

            if(isShock)
            {
                isShock = false;
                feedbacks_Shock.StopFeedbacks();
            }
        }
        if (health == 1)
        {
            isShock = true;

            if(this.transform.gameObject !=null)
            {
                feedbacks_Shock.PlayFeedbacks();
            }

            if (!isFire)
            {
                isFire = true;
                feedbacks_Fire.PlayFeedbacks();
            }
        }
        if (health == 0 || health<0)
        {
            if(!isTriggerDeath)
            {
                SetIsTriggerDeath(true);
                
                feedbacks_Boom.PlayFeedbacks();
                BoomBody();
                feedbacks_Fire.StopFeedbacks();
                feedbacks_Shock.StopFeedbacks();
                feedbacks_Steam.StopFeedbacks();
                OnEnemyDeath?.Invoke();
            }
        }
    }
    private void BoomBody()
    {
        if (VFX_Death == null) return;
        Vector3 offset = new Vector3(0, 1, 0);
        GameObject VFX = Instantiate(VFX_Death, this.transform.position+ offset, Quaternion.identity);
        Destroy(VFX, 3.5f);
    }
    #endregion
    #region
    public void Rebirth(Vector3 position, Quaternion rotation)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
        Initialization();
        return;
    }
    private void RebirthSelf()
    {
        if(this.transform.gameObject !=  null)
        {
            Rebirth(StartPosition, StartRotation);
        }
    }
    private void Initialization()
    {
        EnemyAggroSystem aggroSystem = GetComponent<EnemyAggroSystem>();
        if (aggroSystem != null)
        {
            aggroSystem.CleanAggroTarget();
        }

        if(!isRebirthHide)
        {
            this.gameObject.SetActive(true);
        }
        isIgnite = false;
        isHurt = false;
        isSteam = false;
        isFire = false;
        isShock = false;
        Boom = false;
        _eye.SetYellow();
        feedbacks_Steam.StopFeedbacks();
        feedbacks_Fire.StopFeedbacks();
        feedbacks_Shock.StopFeedbacks();
        feedbacks_Boom.StopFeedbacks();
        feedbacks_FlyBoom.StopFeedbacks();
        health = StartHealth;
        SetIsTriggerDeath(false);
    }
    
    private void RebirthScription()
    {
        if(isTeachEnemy==false)
        {
            _progress.OnPlayerDeath += RebirthSelf;
        }
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(Boom)
        {
            InstantiateSpreadArea();
            feedbacks_FlyBoom.PlayFeedbacks();
            bt.enabled = false;
        }
    }
    private void InstantiateSpreadArea()
    {
        GameObject spreadObj = Instantiate(spreadArea, this.transform.position, Quaternion.identity);
        Destroy(spreadObj, 1.5f);
    }
    public void SetAtCrash(bool active)
    {
        atCrash = active;
        
        Debug.Log("At Crash�G" + active+"Time:" + Time.time);
    }
    public void EnemyDeathRightNow()
    {
        feedbacks_Boom.PlayFeedbacks();
        feedbacks_Fire.StopFeedbacks();
        feedbacks_Shock.StopFeedbacks();
        feedbacks_Steam.StopFeedbacks();
        OnEnemyDeath?.Invoke();
    }
    private void SetIsTriggerDeath(bool value)
    {
        isTriggerDeath = value;
    }
}
