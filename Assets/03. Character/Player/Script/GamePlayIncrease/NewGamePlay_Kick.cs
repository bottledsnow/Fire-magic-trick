using UnityEngine;

public class NewGamePlay_Kick : NewGamePlay_Basic_Kick
{
    [Header("kick")]
    [SerializeField] private float bulletKeepTime;

    //Script
    private BulletTime bulletTime;

    protected override void Start()
    {
        base.Start();

        bulletTime =  GameManager.singleton.GetComponent<BulletTime>();
    }
    protected override void Onkick()
    {
        base.Onkick();
        bulletTime.BulletTime_Slow(bulletKeepTime);
    }
}
