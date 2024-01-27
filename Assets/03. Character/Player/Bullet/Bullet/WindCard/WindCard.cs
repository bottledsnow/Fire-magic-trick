using UnityEngine;

public class WindCard : Bullet
{
    [Header("WindCard")]
    [SerializeField] private Transform WindCardReturn;

    //Script
    private TrackSystem trackSystem;
    protected override void Start()
    {
        base.Start();

        //Script
        trackSystem = GetComponent<TrackSystem>();

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

        if(trackSystem != null)
        {
            trackSystem.enabled = false;
        }
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
