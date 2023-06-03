using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Far_Shawn : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [Header("Attack")]
    public float attackRange = 1f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private Transform playerTransform;
    private Rigidbody rb;
    private float distanceToPlayer;
    private float lastAttackTime;

    private enum EnemyState
    {
        Idle, Attack
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
                // 閒置狀態，等待發現玩家
                if (fov.canSeePlayer)
                {
                    currentState = EnemyState.Attack;
                }
                break;

            case EnemyState.Attack:
                // 遠程雷射攻擊
                if(distanceToPlayer < attackRange)
                {
                    transform.LookAt(playerTransform);
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
                break;
        }
    }

    void MoveToPlayer()
    {
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
    }

    
}
