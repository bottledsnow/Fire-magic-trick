using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Near : MonoBehaviour, IHealth
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private float moveSpeed;
    [Header("Attack")]
    public float attackRange = 1f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;

    private Transform playerTransform;
    private Rigidbody rb;
    private float distanceToPlayer;

    private enum EnemyState
    {
        Idle, Melee
    }

    private EnemyState currentState = EnemyState.Idle;

    private FieldOfView fov;

    void Start()
    {
        fov = this.gameObject.GetComponent<FieldOfView>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        switch (currentState)
        {
            case EnemyState.Idle:
                // 閒置狀態，等待發現玩家，根據距離選擇攻擊方式
                if (fov.canSeePlayer)
                {
                    currentState = EnemyState.Melee;
                }
                break;

            case EnemyState.Melee:
                // 近戰腳腳攻擊
                if (distanceToPlayer < attackRange)
                {
                    transform.LookAt(playerTransform);

                    // 近戰攻擊邏輯運算
                    print("腳腳怪使出近戰攻擊!");
                }
                else
                {
                    MoveToPlayer();
                }
                break;
        }
    }

    void MoveToPlayer()
    {
        transform.LookAt(playerTransform);
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed);
    }

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }
    public void TakeDamage(int Damage)
    {
        _health -= Damage;
        Debug.Log("Enemy_Test get damage " + Damage);
        Debug.Log("Enemy_Test health " + _health);
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
