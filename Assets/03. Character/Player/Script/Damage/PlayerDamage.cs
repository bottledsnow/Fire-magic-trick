using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    private DamageType damageType;

    [SerializeField]
    private int damage;

    public enum DamageType
    {
        NormalShoot,
        ChargeShoot,
        FireDash,
        SuperDash,
        Kick
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            IHealth _health = collision.gameObject.GetComponent<IHealth>();
            if(_health != null)
            {
                _health.TakeDamage(damage,damageType);
            }
        }
    }
}
