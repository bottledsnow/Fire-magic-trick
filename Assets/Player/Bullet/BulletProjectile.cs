using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitBlue;
    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        float speed = 10f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BulletTarget>() != null)
        {
            //Hit Target
            Instantiate(vfxHitBlue, transform.position, Quaternion.identity);
        }
        else
        {
            //Hit Something else
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
