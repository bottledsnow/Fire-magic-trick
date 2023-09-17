using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RangedShoot_Shoot : Action
{
	public GameObject bulletPrefab;
    public Transform firePoint;
	public float bulletSpeed = 20;
	public SharedGameObject targetObject;

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
				Vector3 direction = (targetObject.Value.transform.position - transform.position).normalized;
                bulletRigidbody.AddForce(direction * bulletSpeed, ForceMode.Impulse);
            }
        }
		return TaskStatus.Success;
	}
}