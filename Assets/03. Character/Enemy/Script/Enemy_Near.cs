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
    private float normalAttack_FireTimer;

    private enum EnemyState
    {
        Idle, Attack, 
    }

    private EnemyState currentState = EnemyState.Idle;

    private FieldOfView fov;

    void Start()
    {
        fov = this.gameObject.GetComponent<FieldOfView>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        normalAttack_FireTimer = 0;
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
                    currentState = EnemyState.Attack;
                }
                break;

            case EnemyState.Attack:
                // 近戰腳腳攻擊
                if (distanceToPlayer < normalAttack_Range && normalAttack_FireTimer >= normalAttack_Cooldown)
                {
                    normalAttack_FireTimer = 0;
                    StartCoroutine(NormalAttack());
                }
                else
                {
                    transform.LookAt(new Vector3(playerTransform.position.x,transform.position.y,playerTransform.position.z));
                    isMoveToPlayer = true;
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

        normalAttack_FireTimer += 0.2f;
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
    IEnumerator NormalAttack()
    {
        isMoveToPlayer = false;
        yield return new WaitForSeconds(normalAttack_Duration);
    }
}
