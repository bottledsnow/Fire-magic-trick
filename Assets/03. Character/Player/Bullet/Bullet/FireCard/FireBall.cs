using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("FireBall")]
    [SerializeField] private int damage;
    [SerializeField] private float lifeTime;

    private IHealth health;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            health = other.GetComponent<IHealth>();
            health.TakeDamage(damage,PlayerDamage.DamageType.ChargeShoot);
        }
    }
}
