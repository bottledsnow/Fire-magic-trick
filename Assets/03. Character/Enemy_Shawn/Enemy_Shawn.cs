using MoreMountains.Feedbacks;
using UnityEngine;
public class Enemy_Shawn : MonoBehaviour, IHealth
{
    [Header("State")]
    public bool isIgnite;
    public bool isHurt;
    public bool isSteam;
    public bool isFire;
    public bool isShock;
    public bool Boom;

    [Header("Health")]
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


    private Collider[] Colliders;
    private Rigidbody rb;
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
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        Colliders = GetComponentsInChildren<Collider>();
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
        Debug.Log("敵人當前血量" + health);
    }
    #endregion
    #region Damage
    public void TakeDamage(int Damage)
    {
        health -= Damage;

        hitTimer = Time.time;

        healthFeedback(health);
        Debug.Log("敵人受到傷害" + Damage);
        Debug.Log("敵人當前血量" + health);

        if (health <= 0)
        {
            EnemyDeath();
        }
        else if (health <= ignitionPoint)
        {
            EnemyIgnite();
        }
    }
    private void EnemyDeath()
    {
        rb.drag = 10;
        CloseCollider();
        Destroy(gameObject, 1.5f);
    }
    private void EnemyIgnite()
    {
        isIgnite = true;
        //引燃時的反饋
    }
    private void CloseCollider()
    {
        foreach (Collider collider in Colliders)
        {
            collider.enabled = false;
        }
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

            feedbacks_Shock.PlayFeedbacks();
        }
        if (health == 0)
        {
            feedbacks_Boom.PlayFeedbacks();
            feedbacks_Fire.StopFeedbacks();
            feedbacks_Shock.StopFeedbacks();
            feedbacks_Steam.StopFeedbacks();
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
