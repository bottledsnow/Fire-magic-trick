using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Near_Health : MonoBehaviour,IHealth
{
    private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }
    public void TakeDamage(int Damage)
    {
        _health -= Damage;
        Debug.Log("Enemy_Near get damage " + Damage);
        Debug.Log("Enemy_Near health " + _health);
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
