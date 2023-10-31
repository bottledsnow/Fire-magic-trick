using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour,IHealth
{
    public int health;
    public int iHealth
    {
        get { return health; }
        set { health = value; }
    }

    public void TakeDamage(int damage , PlayerDamage.DamageType damageType)
    {
        if (damageType == PlayerDamage.DamageType.ChargeShoot)
        {
            Destroy(gameObject);
        }
        else
        {
            
        }
    }
}
