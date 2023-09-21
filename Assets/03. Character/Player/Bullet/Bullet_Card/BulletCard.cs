using UnityEngine;

public class BulletCard : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy_Health _health = collision.gameObject.GetComponent<Enemy_Health>();
            if(_health != null)
            {
                _health.TakeDamage(1);
            }
        }
    }
}
