using UnityEngine;

public class NewGamePlay_Shot : NewGamePlay_Basic_Shot
{
    [Space(20)]
    public Transform bullet;
    private Shooting_Normal shooting_Normal;

    [Header("test")]
    public bool isShot;
    public float shotCoolingTime;
    public float timer;

    public override void Start()
    {
        base.Start();
        shooting_Normal = GameManager.singleton.ShootingSystem.GetComponent<Shooting_Normal>();
    }
    public override void Update()
    {
        base.Update();
        shotTimer();
    }
    private void shotTimer()
    {
        if(isShot)
        {
            timer += Time.deltaTime;
        }

        if(timer >= shotCoolingTime)
        {
            SetIsShot(false);
            timer = 0;
        }
    }
    public void Normal_Shot()
    {
        if(!isShot)
        {
            Shot(0);
            SetIsShot(true);
        }else
        {
            //is Coolling.
        }
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
    public void Shot(float rotate_x, float rotate_y)
    {
        Shot(bullet, rotate_x, rotate_y);
        shooting_Normal.PlayShootFeedbacks();
    }
    private void SetIsShot(bool value)
    {
        this.isShot = value;
    }
}
