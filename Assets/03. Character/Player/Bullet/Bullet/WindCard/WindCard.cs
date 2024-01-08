using UnityEngine;

public class WindCard : Bullet
{
    [Header("WindCard")]
    [SerializeField] private Transform WindCardReturn;
    protected override void Start()
    {
        base.Start();

        //Setting
        useTriggerEnter = true;
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    protected override void OnHitEnemy()
    {
        base.OnHitEnemy();
        Instantiate(WindCardReturn, transform.position, Quaternion.identity);
    }

    protected override void OnHitSomething()
    {
        base.OnHitSomething();
    }
}
