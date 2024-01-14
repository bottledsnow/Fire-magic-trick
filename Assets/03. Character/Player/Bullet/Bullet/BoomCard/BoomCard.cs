using UnityEngine;

public class BoomCard : Bullet
{
    [Header("Boom Card")]
    [SerializeField] private Transform boomArea;
    [SerializeField] private Transform fireRetrun;
    protected override void OnHitEnemy()
    {
        base.OnHitEnemy();
        Instantiate(fireRetrun, transform.position, Quaternion.identity);
    }
    protected override void OnHitSomething()
    {
        base.OnHitSomething();
        Instantiate(boomArea, transform.position, Quaternion.identity);
    }
}
