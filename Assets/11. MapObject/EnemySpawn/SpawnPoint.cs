using UnityEngine;
using System.Threading.Tasks;
public class SpawnPoint : MonoBehaviour
{
    [Header("Spawn Point")]
    [SerializeField] private float spawnForce;
    [SerializeField] private float delay;

    [Header("Spawn Object")]
    [SerializeField] private GameObject[] enemys;

    //Script
    private EnemySpawn enemySpawn;

    [Header("VFX")]
    [SerializeField] private ParticleSystem vfx_Fog_keep;
    [SerializeField] private ParticleSystem vfx_Fog;

    private void Start()
    {
        //Script
        enemySpawn = GetComponentInParent<EnemySpawn>();

        //event
        enemySpawn.OnSpawn += spawn_delay;
        enemySpawn.OnDeath += hideEnemy;
    }
    private void hideEnemy()
    {
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
        if(enemySpawn.isSpawned == true) { return; }

        vfx_Fog_keep.Play();
        await Task.Delay((int)(delay * 1000));
        spawn();
    }
}
