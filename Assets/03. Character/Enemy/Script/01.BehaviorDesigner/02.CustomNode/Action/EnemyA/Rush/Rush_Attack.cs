using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Rush_Attack : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedTransform behaviorObject;

    [Header("JumpForce")]
    [SerializeField] private float upForce;
    [SerializeField] private float forwardForce;

    [Header("GroundCheck")]
    [SerializeField] private float raycastDistance = 0.3f;

    private GameObject rushCollider;
    private Rigidbody rb;
    private float timer;
    NavMeshAgent navMeshAgent;

    public override void OnStart()
    {
        if(behaviorObject.Value != null)
        {
            rushCollider = behaviorObject.Value.Find("RushCollider").gameObject;
        }
        rb = GetComponent<Rigidbody>();
        timer = Time.time;
        navMeshAgent = GetComponent<NavMeshAgent>();
        Jump();
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - timer > 0.2f && isGrounded())
        {
			ColliderController(false);
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
        rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);

		ColliderController(true);
    }

	private void ColliderController(bool isActive)
	{
        if(rushCollider !=null)
        {
            rushCollider.SetActive(isActive);
        }
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
}