using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Near : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [Header("NormalAttack")]
    public float normalAttack_Range = 1f;
    public float normalAttack_Damage = 10f;
    public float normalAttack_Cooldown = 3f;
    public float normalAttack_Duration = 1.5f;

    private Transform playerTransform;
    private Rigidbody rb;
    private float distanceToPlayer;
    private bool isMoveToPlayer;
    private int playerDetection;
    private float normalAttack_FireTimer;
    private float normalAttack_DurationTimer;

    private enum EnemyState
    {
        Idle, Chase, Attack, 
    }

    private EnemyState currentState = EnemyState.Idle;

    private FieldOfView fov;

    void Start()
    {
        fov = this.gameObject.GetComponent<FieldOfView>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        normalAttack_FireTimer = 0;
        normalAttack_DurationTimer = 0;
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        SpeedControl();

        switch (currentState)
        {
            case EnemyState.Idle:
                // 閒置狀態，等待發現玩家
                isMoveToPlayer = false;
                if (fov.canSeePlayer)
                {   
                    if(distanceToPlayer > normalAttack_Range)
                    {
                        currentState = EnemyState.Chase;
                    }
                    else
                    {
                        currentState = EnemyState.Attack;
                    }
                }
                break;

            case EnemyState.Chase:
                //追逐玩家
                isMoveToPlayer = true;
                if(distanceToPlayer < normalAttack_Range)
                {
                    currentState = EnemyState.Attack;
                }
                if(!fov.canSeePlayer)
                {
                    currentState = EnemyState.Idle;
                    normalAttack_DurationTimer = Time.time + normalAttack_Duration;
                }
                break;

            case EnemyState.Attack:
                // 近戰攻擊
                isMoveToPlayer = false;
                if(Time.time >= normalAttack_FireTimer)
                {
                    normalAttack_FireTimer = Time.time + normalAttack_Cooldown;
                    NormalAttack();
                }
                if(distanceToPlayer > normalAttack_Range && Time.time > normalAttack_DurationTimer)
                {   
                    currentState = EnemyState.Chase;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        if(isMoveToPlayer)
        {
            MoveToPlayer();
        }
    }

    void MoveToPlayer()
    {
        transform.LookAt(new Vector3(playerTransform.position.x,transform.position.y,playerTransform.position.z));
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed);
    }
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    void NormalAttack()
    {
        print("普通攻擊");
    }
}
