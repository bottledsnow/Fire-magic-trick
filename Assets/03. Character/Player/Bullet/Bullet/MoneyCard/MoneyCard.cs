using UnityEngine;

public class MoneyCard : Bullet
{
    [SerializeField] private Transform Money;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    private void OnDestroy()
    {
        Instantiate(Money, this.transform.position, Quaternion.identity);
    }
    protected override void OnHitEnemy()
    {
        base.OnHitEnemy();
    }

    protected override void OnHitSomething()
    {
        base.OnHitSomething();
        Instantiate(Money, this.transform.position, Quaternion.identity);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
