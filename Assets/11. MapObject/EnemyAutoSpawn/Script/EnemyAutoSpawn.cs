using UnityEngine;

public class EnemyAutoSpawn : MonoBehaviour
{
    [Header("Mode")]
    [SerializeField] private bool useY;
    [SerializeField] private bool DirectionToCenter;
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
    Quaternion rotataion;
    private void Awake()
    {
        Enemys = new GameObject[MaxEnemyCount];
        Center = transform.position;
    }
    private void Start()
    {
        SetCanSpawn(true);
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
            if (enemyCount < MaxEnemyCount)
            {
                SpawnEnemy(Center);
                SetCanSpawn(false);
            }
            else
            {
                EnemyRebirthCheck();
            }
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
    
    private void SpawnEnemy(Vector3 posi)
    {
        Vector3 position = newPosition(posi);

        Vector3 Direction = (Center - position).normalized;

        if(DirectionToCenter)
        {
            rotataion = this.transform.rotation;
        }
        else
        {
            rotataion = Quaternion.Euler(Direction);
        }

        Enemys[enemyCount] = Instantiate(Enemy, position, rotataion);

        enemyCount++;
    }
    private void EnemyRebirthCheck()
    {
        for(int i =0;i< Enemys.Length; i++)
        {
            if (Enemys[i].activeSelf == false)
            {
                EnemyRebirth(i);
                return;
            }
        }
    }
    private void EnemyRebirth(int i)
    {
        EnemyHealthSystem health = Enemys[i].GetComponent<EnemyHealthSystem>();

        Vector3 position = newPosition(Center);
        Quaternion rotation = newRotation(Center);

        health.Rebirth(position, rotation);

        SetCanSpawn(false);
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
    private Quaternion newRotation(Vector3 position)
    {
        Vector3 Direction = (Center - position).normalized;
        Quaternion rotataion = Quaternion.Euler(Direction);

        return rotataion;
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
