using UnityEngine;

public class Enemy_Shawn : MonoBehaviour, IHealth
{
    [Header("State")]
    public bool isIgnite;

    [Header("Health")]
    public int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int ignitionPoint;

    [Header("Cooling")]
    [SerializeField] private float coolingInterval;
    [SerializeField] private float coolingTime;

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
    #region Cooling
    private void Update()
    {
        EnemyCoolingCheck();
    }
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
        Debug.Log("敵人當前血量" + health);
    }
    #endregion
    #region Damage
    public void TakeDamage(int Damage)
    {
        health -= Damage;

        hitTimer = Time.time;

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
}
