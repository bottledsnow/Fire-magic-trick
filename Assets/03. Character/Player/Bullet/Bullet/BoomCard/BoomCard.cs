using UnityEngine;

public class BoomCard : Bullet
{
    [Header("Boom Card")]
    [SerializeField] private GameObject boomArea;
    [SerializeField] private GameObject fireRetrun;
    protected override void OnHitEnemy()
    {
        base.OnHitEnemy();

        // ObjectPoolManager.SpawnObject(fireRetrun, transform.position, Quaternion.identity);
    }
    protected override void OnHitSomething()
    {
        base.OnHitSomething();

        ObjectPoolManager.SpawnObject(boomArea, transform.position, Quaternion.identity);
    }
}
