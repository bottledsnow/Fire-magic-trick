using MoreMountains.Feedbacks;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Shooting_Magazing _shooting_magazing;
    private Shooting_Check _shooting_check;
    [Header("Shoot Setting")]
    [SerializeField] private Transform spawnBulletPositionOrigin;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private float shootingEnergyCost;
    [SerializeField] private float FireEnergyCost;
    [Header("Feedbacks")]
    [SerializeField] private MMF_Player feedbacks_NoBullet;
    private void Start()
    {
        _shooting_check = GetComponent<Shooting_Check>();
        _shooting_magazing = GetComponent<Shooting_Magazing>();
    }
    public void shoot(Transform preferb)
    {
        spawnBulletPositionToNew();

        if (_shooting_magazing.enabled == true)
        {
            if (_shooting_magazing.Bullet <= 0)
            {
                feedbacks_NoBullet.PlayFeedbacks();
                return;
            }

            _shooting_magazing.UseBullet();
        }

        Vector3 aimDir = (_shooting_check.mouseWorldPosition - spawnBulletPosition.position).normalized;
        Instantiate(preferb, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));

        
    }
    public void Shoot_Normal(Transform preferb)
    {
        shoot(preferb);
    }
    private void spawnBulletPositionToNew()
    {
        Vector3 Direction = (_shooting_check.mouseWorldPosition - spawnBulletPositionOrigin.position).normalized;
        spawnBulletPositionOrigin.transform.forward = new Vector3(Direction.x, 0, Direction.z);
    }
}
