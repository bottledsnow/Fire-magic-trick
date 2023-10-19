using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RushAttack : Action
{
	[Header("JumpForce")]
	[SerializeField] private float upForce;
	[SerializeField] private float forwardForce;

	private Rigidbody rb;
	
	public override void OnStart()
	{
		rb = GetComponent<Rigidbody>();
	}

	public override TaskStatus OnUpdate()
	{
		Jump();
		return TaskStatus.Success;
	}

	void Jump()
	{
		rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
		rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
	}
}