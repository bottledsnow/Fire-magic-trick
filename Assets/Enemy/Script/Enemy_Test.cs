using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Test : MonoBehaviour,IHealth
{
    [SerializeField]
    private int _health;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }
    public void TakeDamage(int Damage)
    {
        _health -= Damage;
        Debug.Log("Enemy_Test get damage�G" + Damage);
        Debug.Log("Enemy_Test health�G" + _health);
    }
}
