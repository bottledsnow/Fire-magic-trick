using MoreMountains.Feedbacks;
using UnityEngine;

public class EnemyAutoSpawn : MonoBehaviour
{
    [Header("Mode")]
    [SerializeField] private bool useY;
    [Header("Setting")]
    [SerializeField] private GameObject Enemy;
    [SerializeField] private int MaxEnemyCount;
    [SerializeField] private float spawnRange = 10;
    [SerializeField] private float spawnTime;
    [Header("Debug")]
    [SerializeField] private Transform debugBox;

    private GameObject[] Enemys;
    private Vector3 Center;
    private float timer;
    private bool canSpawn;
    private int enemyCount;

    private void Awake()
    {
        Enemys = new GameObject[MaxEnemyCount];
        Center = transform.position;
    }
    private void Update()
    {
        spawnEnemySystem();
        spawnEnemyTimerSystme();
    }
    private void spawnEnemySystem()
    {
        if(canSpawn)
        {
            SpawnEnemy(Center);
            SetCanSpawn(false);
        }
    }
    private void spawnEnemyTimerSystme()
    {
        if(!canSpawn)
        {
            timer += Time.deltaTime;
        }

        if(timer >= spawnTime)
        {
            timer = 0;
            SetCanSpawn(true);
        }
    }
    
    private void SpawnEnemy(Vector3 position)
    {
        Vector3 Direction = (Center - position).normalized;
        Quaternion rotataion = Quaternion.Euler(Direction);

        if(enemyCount < MaxEnemyCount)
        {
            Enemys[enemyCount] = Instantiate(Enemy,newPosition(position),rotataion);
            enemyCount++;
        }
    }
    
    private Vector3 newPosition(Vector3 position)
    {
        float x = position.x + Random.Range(-spawnRange, spawnRange);
        float y1 = position.y + Random.Range(-spawnRange, spawnRange);
        float y2 = position.y;
        float z = position.z + Random.Range(-spawnRange, spawnRange);

        if(useY)
        {
            Vector3 newPosition = new Vector3(x, y1, z);
            return newPosition;
        }
        else
        {
            Vector3 newPosition = new Vector3(x, y2, z);
            return newPosition;
        }
    }
    private void SetCanSpawn(bool active)
    {
        canSpawn = active;
    }
    private void OnValidate()
    {
        if(useY)
        {
            Vector3 scale = new Vector3(spawnRange * 2, spawnRange * 2, spawnRange * 2);
            debugBox.localScale = scale;
        }
        else
        {
            Vector3 scale = new Vector3(spawnRange * 2, 1, spawnRange * 2);
            debugBox.localScale = scale;
        }
    }
}
