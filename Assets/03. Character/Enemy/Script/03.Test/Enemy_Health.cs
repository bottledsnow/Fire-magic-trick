using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour,IHealth
{
    [SerializeField] private MMFeedbacks HitFeedbacks;
    [SerializeField] private MMFeedbacks DeathFeedbacks;
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;
    [SerializeField] private int ignitionPoint;
    [SerializeField] private float coolingTime;
    [SerializeField] private float coolingInterval;
    private float hitTimer;
    private float coolingTimer;
    private Rigidbody rb;
    private Collider[] Colliders;
    public bool isIgnite;
    public int iHealth
    {
        get { return health; }
        set { health = value; }
    }

    private void Awake()
    {
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

        Debug.Log("Enemy_Test get damage" + Damage);
        Debug.Log("Enemy_Test health" + health);
        
        if(health <= 0)
        {
            EnemyDeath();
            DeathFeedbacks.PlayFeedbacks();
        }
        else if(health <= ignitionPoint)
        {
            EnemyIgnite();
        }
        else
        {
            HitFeedbacks.PlayFeedbacks();
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
    }
    private void EnemyCooling()
    {
        health ++;
        coolingTimer = Time.time;
        Debug.Log("Enemy_Test health" + health);
    }
    private void CloseCollider()
    {
        foreach (Collider collider in Colliders)
        {
            collider.enabled = false;
        }
    }
}
