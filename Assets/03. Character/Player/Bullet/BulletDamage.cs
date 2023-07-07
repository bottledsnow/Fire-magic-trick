using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Enemy")
        {
            MMFeedbacks EnemyFeedback = collision.gameObject.GetComponent<MMFeedbacks>();
            EnemyFeedback?.PlayFeedbacks();
            IHealth health = collision.gameObject.GetComponent<IHealth>();
            health.TakeDamage(damage);
        }
    }
}
