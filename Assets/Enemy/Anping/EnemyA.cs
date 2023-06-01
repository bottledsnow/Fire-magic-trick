using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : Enemy
{
    [Header("AttackRange")]
    public float meleeRange = 1f;
    public float laserRange = 5f;
    [Header("AttackStylePrefernceRange")]
    public float meleePreference = 3f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private Transform playerTransform;
    private float lastAttackTime = 0f;
    private int laserFiredTime = 0;
    private Rigidbody rb;
    public float distanceToPlayer;

    private enum EnemyState
    {
        Idle,Melee,Laser
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
                    if(distanceToPlayer > meleePreference)
                    {
                        currentState = EnemyState.Laser;
                    }
                    else
                    {
                        currentState = EnemyState.Melee;
                    }
                }
                break;

            case EnemyState.Laser:
                // 遠程雷射攻擊
                if(distanceToPlayer < laserRange)
                {
                    if (Time.time - lastAttackTime >= attackCooldown)
                    {
                        lastAttackTime = Time.time;
                        LaserShoot();
                    }
                }
                else
                {
                    MoveToPlayer();
                }

                if(laserFiredTime >= 5 || distanceToPlayer < meleePreference)
                {
                    currentState = EnemyState.Melee;
                }
                break;

            case EnemyState.Melee:
                // 近戰腳腳攻擊
                if(distanceToPlayer < meleeRange)
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
        // 向玩家移動
        transform.LookAt(playerTransform);
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed);
    }

    void LaserShoot()
    {
        // 向玩家發射雷射
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 20f;
        Destroy(bullet, 2f);
        laserFiredTime++;
    }

    public void TakeDamage(float damage)
    {
        // 敵人受到傷害
        health -= damage;
        if (health <= 0)
        {
            // 敵人死亡
            Destroy(gameObject);
        }
    }
}
