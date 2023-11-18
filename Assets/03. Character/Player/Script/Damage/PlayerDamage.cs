using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private DamageType damageType;

    public int damage;

    public enum DamageType
    {
        NormalShoot,
        ChargeShoot,
        FireDash,
        SuperDash,
        Kick
    }
    public void ToDamageEnemy(Collider other)
    {
        IHealth _health = other.gameObject.GetComponent<IHealth>();
        if (_health != null)
        {
            if(other.gameObject != null)
            {
                _health.TakeDamage(damage, damageType);
            }
        }
    }
    public void ToDamageEnemy(Collision Collision)
    {
        IHealth _health = Collision.gameObject.GetComponent<IHealth>();
        if (_health != null)
        {
            if(Collision.gameObject != null)
            {
                _health.TakeDamage(damage, damageType);
            }
        }
    }
}
