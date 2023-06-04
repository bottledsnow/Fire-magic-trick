using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Far_Health : MonoBehaviour,IHealth
{
    [SerializeField] private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }
    public void TakeDamage(int Damage)
    {
        _health -= Damage;
        Debug.Log("Enemy_Far get damage " + Damage);
        Debug.Log("Enemy_Far health " + _health);
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
