using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour,IHealth
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int ignitionPoint;
    [SerializeField] private float coolingTime;
    [SerializeField] private float coolingInterval;
    private float hitTimer;
    private float coolingTimer;
    private Rigidbody rb;
    private Collider[] Colliders;
    public bool isIgnite;
    public int health;
    public int iHealth
    {
        get { return health; }
        set { health = value; }
    }

    private void Awake()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody>();
        Colliders = GetComponentsInChildren<Collider>();
    }

    private void Update()
    {
        if(Time.time - hitTimer > coolingTime && Time.time - coolingTimer > coolingInterval && health < maxHealth)
        {
            EnemyCooling();
        }
    }
    public void TakeDamage(int Damage)
    {
        health -= Damage;

        hitTimer = Time.time;

        Debug.Log("敵人當前血量(" + health + "/" + maxHealth + ")");
        
        if(health <= 0)
        {
            EnemyDeath();
        }
        else if(health <= maxHealth - ignitionPoint)
        {
            EnemyIgnite();
        }
    }
    private void EnemyDeath()
    {
        rb.drag = 10;
        CloseCollider();
        Destroy(gameObject, 1.5f);
    }
    private void EnemyIgnite()
    {
        isIgnite = true;
        //引燃時的反饋
    }
    private void EnemyCooling()
    {
        health ++;
        coolingTimer = Time.time;
        Debug.Log("敵人當前血量(" + health + "/" + maxHealth + ")");
        
        if(health >= maxHealth - ignitionPoint)
        {
            isIgnite = false;
        }
    }
    private void CloseCollider()
    {
        foreach (Collider collider in Colliders)
        {
            collider.enabled = false;
        }
    }
}
