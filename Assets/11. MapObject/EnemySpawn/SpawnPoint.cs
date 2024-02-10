using UnityEngine;
using System.Threading.Tasks;
using System.Threading;

public class SpawnPoint : MonoBehaviour
{
    [Header("Spawn Point")]
    [SerializeField] private float spawnForce;
    [SerializeField] private float delay;

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
        enemySpawn.OnSpawn += () => spawn_delay();
        enemySpawn.OnDeath += hideEnemy;

        //Init
        hideEnemy();
    }
    private void hideEnemy()
    {
        cancellationTokenSource?.Cancel();
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(false);
            Debug.Log(enemys[i].name + " is hidden");
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
            enemyHealthSystem.Rebirth(this.transform.position, this.transform.rotation);
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