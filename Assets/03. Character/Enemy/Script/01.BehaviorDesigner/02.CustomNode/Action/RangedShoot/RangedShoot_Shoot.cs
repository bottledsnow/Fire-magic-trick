using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RangedShoot_Shoot : Action
{
	[Header("SharedVariable")]
	[SerializeField] private SharedGameObject targetObject;
	[Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 20;
	[Header("Player")]
    [SerializeField] private float playerHeight = 0.3f;
    

    public override void OnStart()
    {

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