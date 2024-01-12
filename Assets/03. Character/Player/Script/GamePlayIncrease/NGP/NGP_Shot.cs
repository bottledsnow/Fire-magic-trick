using UnityEngine;

public class NGP_Shot : NGP_Basic_Shot
{
    [Space(20)]
    [Header("Bullet")]
    [SerializeField] private Transform normalCard;
    [SerializeField] private Transform boomCard;
    [SerializeField] private Transform fireCard;
    [SerializeField] private Transform windCard;

    [Header("Shot")]
    [SerializeField] private float shotCoolingTime;

    //Script
    private Shooting_Normal shooting_Normal;
    private NewGamePlay_Combo combo;

    //Variable
    private bool isShot;
    private float timer;
    public enum ShotType
    {
        Normal,
        Boom,
        Fire,
        Wind,
    }
    public ShotType shotType;
    public override void Start()
    {
        base.Start();
        shooting_Normal = GameManager.singleton.ShootingSystem.GetComponent<Shooting_Normal>();
        combo = GetComponent<NewGamePlay_Combo>();
    }
    public override void Update()
    {
        base.Update();
        shotTimer();
    }
    private void shotTimer()
    {
        if (isShot)
        {
            timer += Time.deltaTime;
        }

        if (timer >= shotCoolingTime)
        {
            SetIsShot(false);
            timer = 0;
        }
    }
    public void Normal_Shot()
    {
        if (!isShot)
        {
            Shot(0);
            SetIsShot(true);
        }
        else
        {
            //is Coolling.
        }
    }
    public void Shot()
    {
        Shot(normalCard);
        shooting_Normal.PlayShootFeedbacks();
    }

    public void Shot(float rotate_x)
    {
        Shot(normalCard, rotate_x);
        shooting_Normal.PlayShootFeedbacks();
    }
    public void Shot(float rotate_x, ShotType shotType)
    {
        Transform preferb = ChooseBullet(shotType);
        Shot(preferb, rotate_x);
        shooting_Normal.PlayShootFeedbacks();
    }
    public void Shot(float rotate_x, float rotate_y)
    {
        Shot(normalCard, rotate_x, rotate_y);
        shooting_Normal.PlayShootFeedbacks();
    }
    public void Shot(float rotate_x, float rotate_y, ShotType shotType)
    {
        Transform preferb = ChooseBullet(shotType);
        Shot(preferb, rotate_x, rotate_y);
        shooting_Normal.PlayShootFeedbacks();
    }
    public void Shot(Vector3 positionOffset, float rotate_x, float rotate_y, ShotType shotType)
    {
        Transform preferb = ChooseBullet(shotType);
        Shot(preferb, positionOffset, rotate_x, rotate_y);
        shooting_Normal.PlayShootFeedbacks();
    }
    private void SetIsShot(bool value)
    {
        this.isShot = value;
    }
    private Transform ChooseBullet(ShotType shotType)
    {
        Transform shotPreferb = null;

        switch (shotType)
        {
            case ShotType.Normal:
                shotPreferb = this.normalCard;
                break;

            case ShotType.Boom:
                shotPreferb = boomCard;
                break;

            case ShotType.Fire:
                shotPreferb = fireCard;
                break;

            case ShotType.Wind:
                shotPreferb = windCard;
                break;
        }

        return shotPreferb;
    }
    public void SetShotType(ShotType shotType)
    {
        this.shotType = shotType;
    }
}
