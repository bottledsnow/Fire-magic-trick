using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Test : MonoBehaviour,IHealth
{
    [SerializeField] private Transform VFX_Soul;
    [SerializeField] private int _health;
    private Rigidbody rb;
    private Collider boxCollider;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<Collider>();
    }
    public void TakeDamage(int Damage)
    {
        _health -= Damage;
        Debug.Log("Enemy_Test get damage" + Damage);
        Debug.Log("Enemy_Test health" + _health);

        if(_health <=0)
        {
            EnemyDeath();
        }
    }
    private void EnemyDeath()
    {
        Instantiate(VFX_Soul,transform.position,VFX_Soul.rotation);
        rb.drag = 10;
        boxCollider.enabled = false;
        Destroy(gameObject, 1.5f);
    }
}
