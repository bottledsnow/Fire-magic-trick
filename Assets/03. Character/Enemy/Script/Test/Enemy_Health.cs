using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour,IHealth
{
    [SerializeField] private Transform VFX_Soul;
    [SerializeField] private int _health;
    private Rigidbody rb;
    private Collider[] Colliders;
    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Colliders = GetComponentsInChildren<Collider>();
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
        CloseCollider();
        Destroy(gameObject, 1.5f);
    }

    private void CloseCollider()
    {
        foreach (Collider collider in Colliders)
        {
            collider.enabled = false;
        }
    }
}
