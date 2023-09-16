using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Shooting_Magazing _shooting_magazing;
    private Shooting_Check _shooting_check;
    [Header("Shoot Setting")]
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private float shootingEnergyCost;
    [SerializeField] private float FireEnergyCost;
    private void Start()
    {
        _shooting_check = GetComponent<Shooting_Check>();
        _shooting_magazing = GetComponent<Shooting_Magazing>();
    }
    public void shoot(Transform preferb)
    {
        Vector3 aimDir = (_shooting_check.mouseWorldPosition - spawnBulletPosition.position).normalized;
        Instantiate(preferb, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
        _shooting_magazing.UseBullet();
    }
    public void Shoot_Normal(Transform preferb)
    {
        shoot(preferb);
    }
}
