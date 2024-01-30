using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB_Bullet : MonoBehaviour
{
    [Header("DestoryAfterTime")]
    [SerializeField] private float lifeTime;

    [Header("ExplosionPrefab")]
    [SerializeField] private GameObject explosionPrefab;

    [Header("TargetMask")]
    [SerializeField] private LayerMask ignoredLayer;

    [Header("CoordinatedAttack")]
    [SerializeField] float nearbyRange;
    [SerializeField] LayerMask enemyMask;

    private float timer;

    void Awake()
    {
        timer = Time.time;
    }

    void Update()
    {
        if (Time.time - timer > lifeTime)
        {
            Explosion();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            //CallCoordinatedAttack();
        }
        Explosion();
    }

    private void Explosion()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 0.5f);
        Destroy(gameObject);
    }

    void CallCoordinatedAttack()
    {
        foreach(GameObject enemy in NearbyEnemy())
        {
            enemy.GetComponent<EnemyTeamSystem>().CoordinatedAttack();
        }
        
    }

    GameObject[] NearbyEnemy()
    {
        Collider[] i = Physics.OverlapSphere(transform.position, nearbyRange, enemyMask);
        List<GameObject> nearbyEnemy = new List<GameObject>();

        foreach (Collider collider in i)
        {
            EnemyAggroSystem aggroSystem = collider.GetComponent<EnemyAggroSystem>();

            if (aggroSystem != null && collider != GetComponent<Collider>())
            {
                nearbyEnemy.Add(collider.gameObject);
            }
        }

        return nearbyEnemy.ToArray();
    }
}
