using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RangedShoot_Shoot : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedTransform behaviorObject;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    
    [SerializeField] private float bulletSpeed = 20;

    [Header("Player")]
    [SerializeField] private float playerHeight = 0.3f;

    private Transform firePoint;


    public override void OnStart()
    {
        firePoint = behaviorObject.Value.Find("FirePoint");
    }

    public override TaskStatus OnUpdate()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Object.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                Vector3 direction = (new Vector3(0, playerHeight, 0) + targetObject.Value.transform.position - firePoint.position).normalized;
                bulletRigidbody.AddForce(direction * bulletSpeed, ForceMode.Impulse);               
            }
        }
        return TaskStatus.Success;
    }
}