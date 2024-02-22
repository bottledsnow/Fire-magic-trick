using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Rush_JumpBack : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;

    [Header("JumpForce")]
    [SerializeField] private float upForce;
    [SerializeField] private float backForce;

    [Header("GroundCheck")]
    [SerializeField] private float raycastDistance = 0.3f;

    private Rigidbody rb;
    private float timer;
    EnemyAggroSystem enemyAggroSystem;

    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();
        timer = Time.time;
        JampBack();
        enemyAggroSystem = GetComponent<EnemyAggroSystem>();
        enemyAggroSystem.StopReducingController(true);
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - timer > 0.2f && isGrounded())
        {
            rb.velocity = Vector3.zero;
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    private void JampBack()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        rb.AddForce(-transform.forward * backForce, ForceMode.Impulse);
    }

    bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void OnEnd()
    {
        enemyAggroSystem.StopReducingController(false);
    }
}