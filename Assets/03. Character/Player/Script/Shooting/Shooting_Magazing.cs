using UnityEngine;
using System.Threading.Tasks;

public class Shooting_Magazing : MonoBehaviour
{
    //Script
    protected Shooting_Normal shooting_Normal;
    protected ControllerInput input;
    protected EnergySystem energySystem;
    protected MagazingUI magazingUI;
    protected Shooting shooting;

    //variable
    [Range(0,14)]
    [SerializeField] private int startBulletNumber;
    public int Bullet;
    private int MaxBullet = 14;
    private bool isReload = false;

    protected virtual void Awake()
    {
        shooting_Normal = GetComponent<Shooting_Normal>();
        shooting = GetComponent<Shooting>();
    }
    protected virtual void Start()
    {
        energySystem = GameManager.singleton._playerState.GetComponent<EnergySystem>();
        magazingUI = GameManager.singleton.UISystem.GetComponent<MagazingUI>();
        input = GameManager.singleton._input;

        Initialization();  
    }
    protected virtual void Update()
    {
        if (input.ButtonX && !isReload)
        {
            UseReload();
        }
    }
    public void UseReload()
    {
        Reload();
    }
    public void UseBullet()
    {
        if (isReload) return;

        if (Bullet > 0)
        {
            Bullet -= 1;
            magazingUI.UpdateBulletsNumber(Bullet);
        }else
        if (Bullet <= 0)
        {
            Reload();
            return;
        }
    }
    public void UseBullet(int number)
    {
        if(Bullet<=0) UseBullet();

        for(int i=0; i < number; i++)
        {
            if(Bullet >0)
            {
                UseBullet();
            }
        }
    }
    public async void ReloadSystem()
    {
        for (int i = 0; i < MaxBullet; i++)
        {
            if (Bullet > 13)
            {
                Bullet = 14;
                SetCanShooting(true);
                SetIsReload(false);
                return;
            }
            Bullet += 1;
            magazingUI.UpdateBulletsNumber(Bullet);
            await Task.Delay(100);
        }
        Bullet = 14;
        SetCanShooting(true);
        SetIsReload(false);
    }
    public void ClearBullet()
    {
        Bullet = 0;
        magazingUI.UpdateBulletsNumber(Bullet);
    }
    private void Initialization()
    {
        Bullet = startBulletNumber;
        magazingUI.UpdateBulletsNumber(startBulletNumber);
    }
    private void Reload()
    {
        SetIsReload(true);
        CheckBulletBumber();
    }
    private void CheckBulletBumber()
    {
        if(Bullet < 14)
        {
            CheckEnergy();
        }else
        {
            //don't need reload.
            isReload = false;
            return;
        }
    }
    protected void CheckEnergy()
    {
        if(CanReload())
        {
            SetCanShooting(false);
            ReloadSystem();
        }else
        {
            isReload = false;
            //no energy;
        }
    }
    protected virtual bool CanReload() { return true; }
    private void SetCanShooting(bool value)
    {
        shooting.enabled = value;
        shooting_Normal.enabled = value;
    }
    protected void SetIsReload(bool value)
    {
        isReload = value;
    }
}
