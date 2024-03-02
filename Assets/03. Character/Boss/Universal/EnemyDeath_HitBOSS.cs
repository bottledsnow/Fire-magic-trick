using UnityEngine;

public class EnemyDeath_HitBOSS : MonoBehaviour
{
    private EnemyHealthSystem health;
    [SerializeField] private Boss boss;
    [SerializeField] private int Damage;

    private void Awake()
    {
        if(boss != null)
        {
            health = GetComponent<EnemyHealthSystem>();
        }
    }

    private void Start()
    {
        if(boss!=null)
        {
            health.OnEnemyDeath += Hit;
        }
    }
    private void Hit()
    {
        if(boss!=null)
        {
            boss.TakeDamage(Damage, PlayerDamage.DamageType.NormalShoot);
        }
    }
}
