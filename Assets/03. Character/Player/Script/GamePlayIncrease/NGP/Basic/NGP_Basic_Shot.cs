using UnityEngine;

public class NGP_Basic_Shot : MonoBehaviour
{
    [Header("Shoot Setting")]
    [SerializeField] private Transform spawnBulletPositionOrigin;
    [SerializeField] private Transform spawnBulletPosition;

    //Script
    private Shooting_Check _shooting_check;

    public virtual void Start()
    {
        _shooting_check = GameManager.singleton.ShootingSystem.GetComponent<Shooting_Check>();
    }
    public virtual void Update() { }
    public void Shot(Transform preferb)
    {
        Shot(preferb, 0);
    }
    public GameObject Shot_Gameobj(Transform preferb, float rotate_Yaxis)
    {
        spawnBulletPositionToNew();

        Vector3 aimDir = (_shooting_check.mouseWorldPosition - spawnBulletPosition.position).normalized;
        Vector3 rotate = new Vector3(0, rotate_Yaxis, 0);
        Vector3 newDir = Quaternion.Euler(rotate) * aimDir;
        Transform bullet = Instantiate(preferb, spawnBulletPosition.position, Quaternion.LookRotation(newDir, Vector3.up));

        return bullet.gameObject;
    }
    public void Shot(Transform preferb, float rotate_Yaxis)
    {
        spawnBulletPositionToNew();

        Vector3 aimDir = (_shooting_check.mouseWorldPosition - spawnBulletPosition.position).normalized;
        Vector3 rotate = new Vector3(0, rotate_Yaxis, 0);
        Vector3 newDir = Quaternion.Euler(rotate) * aimDir;
        Instantiate(preferb, spawnBulletPosition.position, Quaternion.LookRotation(newDir, Vector3.up));
    }
    public void Shot(Transform preferb, float rotate_Xaxis, float rotate_Yaxis)
    {
        spawnBulletPositionToNew();

        Vector3 aimDir = (_shooting_check.mouseWorldPosition - spawnBulletPosition.position).normalized;
        Vector3 rotate = new Vector3(rotate_Xaxis, rotate_Yaxis, 0);
        Vector3 newDir = Quaternion.Euler(rotate) * aimDir;
        Instantiate(preferb, spawnBulletPosition.position, Quaternion.LookRotation(newDir, Vector3.up));
    }
    public void Shot(Transform preferb, Vector3 positionOffset, float rotate_Xaxis, float rotate_Yaxis)
    {
        spawnBulletPositionToNew();

        Vector3 aimDir = (_shooting_check.mouseWorldPosition - spawnBulletPosition.position).normalized;
        Vector3 rotate = new Vector3(rotate_Xaxis, rotate_Yaxis, 0);
        Vector3 newDir = Quaternion.Euler(rotate) * aimDir;
        Instantiate(preferb, spawnBulletPosition.position + positionOffset, Quaternion.LookRotation(newDir, Vector3.up));
    }
    private void spawnBulletPositionToNew()
    {
        Vector3 Direction = (_shooting_check.mouseWorldPosition - spawnBulletPositionOrigin.position).normalized;
        spawnBulletPositionOrigin.transform.forward = new Vector3(Direction.x, 0, Direction.z);
    }
}
