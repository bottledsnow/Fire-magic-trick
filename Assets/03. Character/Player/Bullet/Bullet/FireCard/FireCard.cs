using UnityEngine;

public class FireCard : Bullet
{
    [Header("FireCard")]
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private float moveTime;
    [SerializeField] private float ThroughDistance;

    //variable
    private float timer;

    private bool isTriggered;

    protected override void Start()
    {
        base.Start();

        isTriggered = false;
    }
    protected override void Update()
    {
        base.Update();

        TimerSystem();
    }

    private void TimerSystem()
    {
        timer += Time.deltaTime;

        if (timer>moveTime)
        {
            ToStop();
        }
    }

    protected override void OnHitEnemy()
    {
        base.OnHitEnemy();
    }

    protected override void OnHitSomething()
    {
        base.OnHitSomething();

        if (!isTriggered)
        {
            isTriggered = true;
            ObjectPoolManager.SpawnObject(fireBallPrefab, transform.position, Quaternion.identity);
        }

    }
    private void ToStop()
    {
        speed = 0;
    }
}
