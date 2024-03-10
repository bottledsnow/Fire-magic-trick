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
    private bool isReloading = false;

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
        if (input.ButtonX && !isReloading)
        {
            UseReload();
        }
    }
    public void UseBullet()
    {
        if (Bullet < 0)
        {
            return;
        }

        Bullet -= 1;
        magazingUI.UpdateBulletsNumber(Bullet);
    }
    public async void Reloading()
    {
        for (int i = 0; i < MaxBullet; i++)
        {
            if (Bullet >= 13)
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
    private void UseReload()
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
            isReloading = false;
            return;
        }
    }
    protected void CheckEnergy()
    {
        if(CanReload())
        {
            SetCanShooting(false);
            Reloading();
        }else
        {
            isReloading = false;
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
        isReloading = value;
    }
}
