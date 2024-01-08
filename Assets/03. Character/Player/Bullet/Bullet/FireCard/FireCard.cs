using UnityEngine;

public class FireCard : Bullet
{
    [Header("FireCard")]
    [SerializeField] private Transform fireBall;
    [SerializeField] private float moveTime;
    [SerializeField] private float ThroughDistance;

    //Script
    private SuperDash superDash;

    //variable
    private float timer;

    protected override void Start()
    {
        base.Start();

        superDash = GameManager.singleton.EnergySystem.GetComponent<SuperDash>();
    }
    protected override void Update()
    {
        base.Update();

        timerSystem();
        DistanceCheck();
    }
    private void DistanceCheck()
    {
        Vector3 Player = GameManager.singleton.Player.transform.position;
        Vector3 FireCard = transform.position;
        float distance = (Player - FireCard).magnitude;

        if(distance < ThroughDistance)
        {
            if(superDash.isSuperDash)
            {
                superDash.ToThroughEnemy();
            }
        }
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
