using UnityEngine;

public class BattleArea_Enemyspawn : MonoBehaviour
{
    [SerializeField] private bool useSuperMode;
    [Header("Enter Fight")]
    [SerializeField] private Transform Enemy_A;
    [SerializeField] private int EnemyNumber_A;
    [SerializeField] private Transform Enemy_B;
    [SerializeField] private int EnemyNumber_B;
    [SerializeField] private Transform Enemy_C;
    [SerializeField] private int EnemyNumber_C;

    public void spawnEnemyA(Transform position)
    {
        Instantiate(Enemy_A, position.position, Quaternion.identity);
    }
    public void spawnEnemyB(Transform position)
    {
        Instantiate(Enemy_B, position.position, Quaternion.identity);
    }
    public void spawnEnemyC(Transform position)
    {
        Instantiate(Enemy_C, position.position, Quaternion.identity);
    }

}
