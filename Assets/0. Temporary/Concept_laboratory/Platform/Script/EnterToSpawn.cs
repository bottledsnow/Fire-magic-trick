using UnityEngine;

public class EnterToSpawn : MonoBehaviour
{
    [SerializeField] private BattleArea_Enemyspawn BattleArea_Enemyspawn;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            BattleArea_Enemyspawn.SpawnEnemy();
        }
    }
}
