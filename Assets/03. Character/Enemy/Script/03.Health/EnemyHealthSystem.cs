using MoreMountains.Feedbacks;
using UnityEngine;
using System.Threading.Tasks;
using BehaviorDesigner.Runtime;

public class EnemyHealthSystem : MonoBehaviour, IHealth
{
    [SerializeField] private bool isTeachEnemy;
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
    [Header("KickBack")]
    public float kickBackRatio;

    private BehaviorTree bt;
    private ProgressSystem _progress;
    private Transform startPosition;
    private EnemyFireSystem _fireSystem;
    private Vector3 StartPosition;
    private Quaternion StartRotation;
    private float hitTimer;
    private float coolingTimer;
    private bool isCooling;
    private bool isInterval;

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
            Debug.Log("Rebirth");
        }  
    }

    private void Update()
    {
        EnemyCoolingCheck();
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
        Debug.Log("Enemy remain health" + health);
    }
    #endregion
    #region Damage
    public void TakeDamage(int damage , PlayerDamage.DamageType damageType)
    {
        health -= damage;
        hitTimer = Time.time;

        healthFeedback(health);
        bt.SendEvent("Hit");
        
        Debug.Log("Enemy remain health" + health);

        if (_fireSystem != null)
        {
            _fireSystem.FireCheck(damageType);
        }

        if (health <= 0)
        {
            EnemyDie();
        }
        else if (health <= ignitionPoint)
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
            feedbacks_Boom.PlayFeedbacks();
            feedbacks_Fire.StopFeedbacks();
            feedbacks_Shock.StopFeedbacks();
            feedbacks_Steam.StopFeedbacks();
        }
    }
    #endregion
    #region
    private async void EnemyDie()
    {
        await Task.Delay(1500);
        //this.gameObject.SetActive(false);
    }
    private void Initialization()
    {
        this.transform.position = StartPosition;
        this.transform.rotation = StartRotation;
        this.gameObject.SetActive(true);
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
    }   
    private void RebirthScription()
    {
        if(isTeachEnemy==false)
        {
            _progress.OnPlayerDeath += Initialization;
        }
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(Boom)
        {
            feedbacks_FlyBoom.PlayFeedbacks();
        }
    }
}
