using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private DamageType damageType;

    public int damage;

    //interface
    IHitNotifier[] hitNotifier;
    ITriggerNotifier[] triggerNotifier;

    private void Start()
    {
        hitNotifier = GetComponents<IHitNotifier>();
        triggerNotifier = GetComponents<ITriggerNotifier>();

        for (int i = 0; i < hitNotifier.Length; i++)
        {
            if (hitNotifier[i] != null)
            {
                hitNotifier[i].OnHit += ToDamageEnemy;
            }
        }

        for (int i = 0; i < triggerNotifier.Length; i++)
        {
            if (triggerNotifier[i] != null)
            {
                triggerNotifier[i].OnTrigger += ToDamageEnemy;
            }
        }
    }

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
