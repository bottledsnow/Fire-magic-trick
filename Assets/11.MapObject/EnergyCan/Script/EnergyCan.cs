using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCan : MonoBehaviour ,IHealth
{
    public int health;
    public int iHealth
    {
        get { return health; }
        set { health = value; }
    }

    public void TakeDamage(int damage , PlayerDamage.DamageType damageType)
    {
        health -= damage;

        if(health <= 0)
        {
            Broke();
        }
    }

    void Broke()
    {
        print("能量擊破");
        Destroy(gameObject);
    }
}
