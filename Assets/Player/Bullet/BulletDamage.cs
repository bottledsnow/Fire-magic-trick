using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField]
    private int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Enemy")
        {
            IHealth health = collision.gameObject.GetComponent<IHealth>();
            health.TakeDamage(damage);
        }
    }
}
