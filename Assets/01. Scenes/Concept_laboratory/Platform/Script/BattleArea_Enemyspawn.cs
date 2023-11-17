using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

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
    [Header("spawnPosition")]
    [SerializeField] private Transform Center;
    [SerializeField] private Transform[] spawnPosition;

    public void SpawnEnemy()
    {
        for(int i = 0;i < EnemyNumber_A;i++)
        {
            spawnEnemyA(spawnPosition[randomSpawnPosition()]);
        }
        for(int i = 0;i < EnemyNumber_B;i++)
        {
            spawnEnemyB(spawnPosition[randomSpawnPosition()]);
        }
        for(int i = 0;i < EnemyNumber_C;i++)
        {
            spawnEnemyC(spawnPosition[randomSpawnPosition()]);
        }
    }
    public void spawnEnemyA(Transform position)
    {
        Vector3 Position = newPosition(position);
        Instantiate(Enemy_A, Position, newRotation(Position));
    }
    public void spawnEnemyB(Transform position)
    {
        Vector3 Position = newPosition(position);
        Instantiate(Enemy_B, Position, newRotation(Position));
    }
    public void spawnEnemyC(Transform position)
    {
        Vector3 Position = newPosition(position);
        Instantiate(Enemy_C, Position, newRotation(Position));
    }
    private int randomSpawnPosition()
    {
        int random = UnityEngine.Random.Range(0, spawnPosition.Length);
        return random;
    }
    private float RandomPosition()
    {
          float random = UnityEngine.Random.Range(-2f, 2f);
        return random;
    }
    private Vector3 newPosition(Transform position)
    {
        float x = position.position.x + RandomPosition();
        float y = position.position.y;
        float z = position.position.z + RandomPosition();
        Vector3 newPosition = new Vector3(x, y, z);

        return newPosition;
    }
    private Quaternion newRotation(Vector3 newPosition)
    {
        Vector3 Direction = (Center.position - newPosition).normalized;
        quaternion rotation = quaternion.LookRotation(Direction, Vector3.up);

        return rotation;
    }
}
