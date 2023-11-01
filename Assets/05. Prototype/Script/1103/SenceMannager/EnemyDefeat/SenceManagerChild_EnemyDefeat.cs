using UnityEngine;

public class SenceManagerChild_EnemyDefeat : MonoBehaviour
{
    [SerializeField] private SenceManager _senceManager;
    private GameObject Enemy;
    private bool EnemyAlive;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(!EnemyAlive)
            {
                EnemyAlive = true;
                Enemy = other.gameObject;
            }
        }
    }

    private void Update()
    {
        EnemyDeath();
    }
    private void EnemyDeath()
    {
        if(Enemy.active==false && EnemyAlive)
        {
            EnemyAlive = false;
            _senceManager.GlassBroken();
        }
    }
}
