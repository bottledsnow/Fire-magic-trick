using UnityEngine;

public class Shooting_Magazing : MonoBehaviour
{
    private MagazingUI _magazingUI;

    [Range(0,14)]
    public int Bullet;
    [SerializeField] private int startBulletNumber;

    private void Start()
    {
        _magazingUI = GameManager.singleton.UISystem.GetComponent<MagazingUI>();
        _magazingUI.UpdateBulletsNumber(startBulletNumber);
    }
    public void Reloading()
    {

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
}
