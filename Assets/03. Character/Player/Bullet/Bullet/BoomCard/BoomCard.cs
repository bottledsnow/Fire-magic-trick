using UnityEngine;

public class BoomCard : Bullet
{
    [Header("Boom Card")]
    [SerializeField] private Transform boomArea;
    protected override void OnHitEnemy()
    {
        base.OnHitEnemy();
    }
    protected override void OnHitSomething()
    {
        base.OnHitSomething();
        Instantiate(boomArea, transform.position, Quaternion.identity);
    }
}
