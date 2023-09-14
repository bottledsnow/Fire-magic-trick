using UnityEngine;

public class Shooting : MonoBehaviour
{
    private ControllerInput _Input;
    private Shooting_Check _shooting_check;
    [Header("Shoot Setting")]
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private float shootingEnergyCost;
    [SerializeField] private float FireEnergyCost;
    private void Start()
    {
        _Input = GameManager.singleton._input;
        _shooting_check = GetComponent<Shooting_Check>();
    }
    public void shoot(Transform preferb)
    {
        Vector3 aimDir = (_shooting_check.mouseWorldPosition - spawnBulletPosition.position).normalized;
        Instantiate(preferb, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
    }
    public void Shoot_Normal(Transform preferb)
    {
        shoot(preferb);
    }
}
