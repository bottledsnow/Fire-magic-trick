using UnityEngine;

public class MoneyHitEnemy : MonoBehaviour
{
    private bool isHit = false;
    private void Start()
    {
        Destroy(this.gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isHit)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyDebuff enemyDebuff = other.gameObject.GetComponent<EnemyDebuff>();

                if (enemyDebuff != null)
                {
                    enemyDebuff.Hit(EnemyDebuff.DebuffType.Money);
                    //isHit = true;
                }
            }
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (!isHit)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyDebuff enemyDebuff = other.gameObject.GetComponent<EnemyDebuff>();

                if (enemyDebuff != null)
                {
                    enemyDebuff.Hit(EnemyDebuff.DebuffType.Money);
                    //isHit = true;
                }
            }
        }
    }
}
