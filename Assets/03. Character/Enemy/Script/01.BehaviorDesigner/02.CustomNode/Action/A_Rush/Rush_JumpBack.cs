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

    private Rigidbody rb;

    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();  
    }

    public override TaskStatus OnUpdate()
    {
        JampBack();
        return TaskStatus.Success;
    }

    private void JampBack()
    {
        rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
		rb.AddForce(-transform.forward * backForce, ForceMode.Impulse);
    }
}