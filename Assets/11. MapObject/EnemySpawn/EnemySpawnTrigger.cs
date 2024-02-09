using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    private EnemySpawn enemySpawn;

    private void Start()
    {
        enemySpawn = GetComponentInParent<EnemySpawn>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemySpawn.ToSpawn();
        }
    }
}
