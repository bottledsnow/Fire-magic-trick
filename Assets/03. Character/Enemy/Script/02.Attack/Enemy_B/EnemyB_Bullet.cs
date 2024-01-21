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
        Explosion();
    }

    private void Explosion()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 0.5f);
        Destroy(gameObject);
    }
}
