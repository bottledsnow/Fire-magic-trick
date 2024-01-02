using UnityEngine;

public class NewGamePlay_Basic_Shot : MonoBehaviour
{
    private Shooting_Check _shooting_check;
    [Header("Shoot Setting")]
    [SerializeField] private Transform spawnBulletPositionOrigin;
    [SerializeField] private Transform spawnBulletPosition;
    public virtual void Start()
    {
        _shooting_check = GameManager.singleton.ShootingSystem.GetComponent<Shooting_Check>();
    }
    public virtual void Update()
    {
        
    }
    public void Shot(Transform preferb)
    {
        spawnBulletPositionToNew();

        Vector3 aimDir = (_shooting_check.mouseWorldPosition - spawnBulletPosition.position).normalized;
    }
    public void Shot(Transform preferb, float rotate_Yaxis)
    {
        spawnBulletPositionToNew();

        Vector3 aimDir = (_shooting_check.mouseWorldPosition - spawnBulletPosition.position).normalized;
        Vector3 rotate = new Vector3(0, rotate_Yaxis, 0);
        Vector3 newDir = Quaternion.Euler(rotate) * aimDir;
        Instantiate(preferb, spawnBulletPosition.position, Quaternion.LookRotation(newDir, Vector3.up));
    }
    private void spawnBulletPositionToNew()
    {
        Vector3 Direction = (_shooting_check.mouseWorldPosition - spawnBulletPositionOrigin.position).normalized;
        spawnBulletPositionOrigin.transform.forward = new Vector3(Direction.x, 0, Direction.z);
    }
}
