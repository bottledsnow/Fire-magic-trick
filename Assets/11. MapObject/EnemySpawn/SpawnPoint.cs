using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class SpawnPoint : MonoBehaviour
{
    [Header("Spawn Point")]
    [SerializeField] private float spawnForce;
    [SerializeField] private float delay;
    [Space(10)]
    [SerializeField] private bool useRandomPosition;
    [SerializeField] private Vector3 min;
    [SerializeField] private Vector3 max;
    [Header("Spawn Object")]
    [SerializeField] private GameObject[] enemys;

    [Header("VFX")]
    [SerializeField] private ParticleSystem vfx_Fog_keep;
    [SerializeField] private ParticleSystem vfx_Fog;

    //Script
    private EnemySpawn enemySpawn;
    private CancellationTokenSource cancellationTokenSource;


    private void Start()
    {
        //Script
        enemySpawn = GetComponentInParent<EnemySpawn>();
        cancellationTokenSource = new CancellationTokenSource();

        //event
        if(enemySpawn != null )
        {
            enemySpawn.OnSpawn += () => spawn_delay();
            enemySpawn.OnDeath += hideEnemy;
        }

        //Init
        hideEnemy();
    }
    public void ResetEnemy()
    {
        hideEnemy();
    }
    private void hideEnemy()
    {
        cancellationTokenSource?.Cancel();
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(false);
        }
    }
    public void Spawn()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i].activeSelf == false)
            {
                spawn();
            }
        }
    }
    private void spawn()
    {
        vfx_Fog_keep.Stop();
        vfx_Fog.Play();

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(true);
            EnemyHealthSystem enemyHealthSystem = enemys[i].GetComponent<EnemyHealthSystem>();
            enemyHealthSystem.giveTargetPlayer();
            Vector3 position = this.transform.position;
            if(useRandomPosition)
            {
                float x = Random.Range(min.x, max.x);
                float y = Random.Range(min.y, max.y);
                float z = Random.Range(min.z, max.z);
                Vector3 offset = new Vector3(x, y, z);
                position += offset;
            }
            enemyHealthSystem.Rebirth(position, this.transform.rotation);
            Rigidbody rb = enemys[i].GetComponent<Rigidbody>();
            rb.AddForce(this.transform.forward * spawnForce, ForceMode.Impulse);
        }
    }
    private async void spawn_delay()
    {
        cancellationTokenSource = new CancellationTokenSource(); 

        if (enemySpawn.isSpawned == true)
        {
            Debug.Log("Spawn Delay");
            return;
        }

        vfx_Fog_keep.Play();

        try
        {
            await Task.Delay((int)(delay * 1000), cancellationTokenSource.Token);
            spawn();
        }
        catch (TaskCanceledException)
        {
            // 在取消的情況下執行一些清理工作，如果需要的話
            vfx_Fog_keep.Stop();
            vfx_Fog.Stop();
            Debug.Log("Spawn operation was canceled.");
        }
    }
}