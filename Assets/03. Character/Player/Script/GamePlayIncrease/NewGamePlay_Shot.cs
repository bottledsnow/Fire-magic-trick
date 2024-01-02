using UnityEngine;

public class NewGamePlay_Shot : NewGamePlay_Basic_Shot
{
    [Space(20)]
    public Transform bullet;
    private Shooting_Normal shooting_Normal;


    public override void Start()
    {
        base.Start();
        shooting_Normal = GameManager.singleton.ShootingSystem.GetComponent<Shooting_Normal>();
    }
    public override void Update()
    {
        base.Update();
    }

    public void Shot()
    {
        Shot(bullet);
        shooting_Normal.PlayShootFeedbacks();
    }
    public void Shot(float rotate_x)
    {
        Shot(bullet, rotate_x);
        shooting_Normal.PlayShootFeedbacks();
    }
}
