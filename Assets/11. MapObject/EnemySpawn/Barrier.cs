using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] private GameObject[] barriers;
    private EnemySpawn enemySpawn;

    private void Start()
    {
        //Script
        enemySpawn = GetComponentInParent<EnemySpawn>();

        //Event
        enemySpawn.OnSpawn += openBarrier;
        enemySpawn.OnDeath += closeBarrier;
        enemySpawn.OnClear += closeBarrier;

        //Function
        getBarrier();
        closeBarrier();
    }
    private void openBarrier()
    {
        for (int i = 0; i < this.barriers.Length; i++)
        {
            this.barriers[i].SetActive(true);
        }
    }
    private void closeBarrier()
    {
        for (int i = 0; i < this.barriers.Length; i++)
        {
            this.barriers[i].SetActive(false);
        }
    }
    private void getBarrier()
    {
        Transform parentTransform = transform;
        barriers = new GameObject[parentTransform.childCount];

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            barriers[i] = parentTransform.GetChild(i).gameObject;
        }
    }
}
