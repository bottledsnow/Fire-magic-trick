using UnityEngine;
using System.Threading.Tasks;

public class Shooting_Magazing : MonoBehaviour
{
    private ControllerInput _input;
    private EnergySystem _energySystem;
    private MagazingUI _magazingUI;
    [Range(0,14)]
    public int Bullet;

    [SerializeField] private int startBulletNumber;

    private int MaxBullet = 14;
    private bool isReloading = false;

    private void Start()
    {
        _energySystem = GameManager.singleton._playerState.GetComponent<EnergySystem>();
        _magazingUI = GameManager.singleton.UISystem.GetComponent<MagazingUI>();
        _magazingUI.UpdateBulletsNumber(startBulletNumber);
        _input = GameManager.singleton._input;

        Initialization();  
    }
    private void Update()
    {
        UseReload();
    }
    private void Initialization()
    {
        Bullet = startBulletNumber;
    }
    private void UseReload()
    {
        if(_input.ButtonX && !isReloading)
        {
            isReloading = true;
            CheckBulletBumber();
        }
    }
    private void CheckBulletBumber()
    {
        if(Bullet >=14)
        {
            isReloading = false;
            return;
        }else
        {
            CheckEnergy();
        }
    }
    private void CheckEnergy()
    {
        bool CanUse;
        _energySystem.UseReload(out CanUse);

        if (CanUse)
        {
            Reloading();
        }
    }
    public async void Reloading()
    {
        for(int i = 0; i < MaxBullet; i++)
        {
            if (Bullet >= 13)
            {
                Bullet = 14;
                isReloading = false;
                return;
            }
            Bullet += 1;
            _magazingUI.UpdateBulletsNumber(Bullet);
            await Task.Delay(100);
        }
    }
    public void UseBullet()
    {
        if (Bullet < 0)
        {
            return;
        }

        Bullet -= 1;
        _magazingUI.UpdateBulletsNumber(Bullet);
    }
    public void ClearBullet()
    {
        Bullet = 0;
        _magazingUI.UpdateBulletsNumber(Bullet);
    }
}
