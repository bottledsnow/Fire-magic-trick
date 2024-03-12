using UnityEngine;

public class MoneyCard : Bullet
{
    private bool isHit = false;
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
        GameObject money = Instantiate(Money, this.transform.position, Quaternion.identity).gameObject;
        Destroy(money, 5);
    }
    protected override void OnHitEnemy()
    {
        base.OnHitEnemy();
    }

    protected override void OnHitSomething()
    {
        base.OnHitSomething();
        if (isHit)  
        {
            Instantiate(Money, this.transform.position, Quaternion.identity);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
