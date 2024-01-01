using UnityEngine;

public class EnemyShield : MonoBehaviour,IHealth
{
    public int health;
    public int iHealth
    {
        get { return health; }
        set { health = value; }
    }
    [SerializeField] private EnemyHealthSystem _healthSystem;

    private int originHealth;
    private void Awake()
    {
        _healthSystem.OnEnemyDeath += OnEnemyDeath;
    }
    private void Start()
    {
        originHealth = iHealth;
    }
    public void TakeDamage(int damage , PlayerDamage.DamageType damageType)
    {
        if (damageType == PlayerDamage.DamageType.ChargeShoot)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("沒用的，麗莎。");
        }

        if(damageType == PlayerDamage.DamageType.NormalShoot)
        {
            health -= damage;

            if (health <= 0)
            {
                BoxCollider boxCollider = GetComponent<BoxCollider>();
                boxCollider.enabled = false;
                Debug.Log("Shield broken");

            }
        }
    }
    private void OnEnemyDeath()
    {
        this.gameObject.SetActive(true);
        health = originHealth;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        print("護盾");
    }
}
