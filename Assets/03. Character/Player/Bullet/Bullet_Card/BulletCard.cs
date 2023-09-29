using UnityEngine;

public class BulletCard : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            IHealth _health = collision.gameObject.GetComponent<IHealth>();
            if(_health != null)
            {
                _health.TakeDamage(1);
            }
        }
    }
}
