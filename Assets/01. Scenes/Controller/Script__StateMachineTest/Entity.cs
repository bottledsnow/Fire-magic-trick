using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;
    public int facingDirection { get; private set; }
    public Rigidbody rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGO { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;


    private Vector2 velocityWorkSpace;
    public virtual void Start()
    {
        facingDirection = 1;

        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody>();
        anim = aliveGO.GetComponent<Animator>();

        stateMachine =  new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkSpace;
    }
    public virtual bool CheckWall()
    {
        return Physics.Raycast(wallCheck.position,aliveGO.transform.right,entityData.wallCheckDistatnce,entityData.whatIsGround);
    }
    public virtual bool CheckLedge()
    {
        return Physics.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround); 
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right* facingDirection*entityData.wallCheckDistatnce));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
    }
}
