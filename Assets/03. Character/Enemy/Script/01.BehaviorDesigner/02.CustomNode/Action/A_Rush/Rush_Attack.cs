using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Rush_Attack : Action
{
	[Header("JumpForce")]
	[SerializeField] private float upForce;
	[SerializeField] private float forwardForce;

	[Header("GroundCheck")]
	[SerializeField] private float raycastDistance;

	private Rigidbody rb;
	private float timer;
	
	public override void OnStart()
	{
		rb = GetComponent<Rigidbody>();
		timer = Time.time;
		Jump();
	}

	public override TaskStatus OnUpdate()
	{
		if(Time.time-timer>0.2f && isGrounded())
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Running;
	}

	void Jump()
	{
		rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
		rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
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