using UnityEngine;

public class MoneyHitEnemy : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 5f);
    }
    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyDebuff enemyDebuff = other.gameObject.GetComponent<EnemyDebuff>();

            if(enemyDebuff != null)
            {
                enemyDebuff.Hit(EnemyDebuff.DebuffType.Money);
            }
        }
    }
}
