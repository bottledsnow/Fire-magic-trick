using UnityEngine;

public class FireCard : Bullet
{
    [Header("FireCard")]
    [SerializeField] private Transform fireBall;
    [SerializeField] private float moveTime;

    //variable
    private float timer;

    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Update()
    {
        base.Update();

        timerSystem();
    }
    private void timerSystem()
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
        Instantiate(fireBall, transform.position, Quaternion.identity);
    }
    private void ToStop()
    {
        speed = 0;
    }
}
