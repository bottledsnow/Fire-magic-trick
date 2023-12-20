using UnityEngine;

public class EnemyB_BulletHitChild : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyHealthSystem healthSystem = other.gameObject.GetComponent<EnemyHealthSystem>();
            healthSystem.TakeDamage(0,PlayerDamage.DamageType.SuperDash);
        }
    }
}
