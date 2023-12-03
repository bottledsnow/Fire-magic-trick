using UnityEngine;

public class BrokenBoomArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyHealthSystem enemyHealthSystem = other.GetComponent<EnemyHealthSystem>();
            enemyHealthSystem.TakeDamage(2, PlayerDamage.DamageType.SuperDash);
        }
    }
}
