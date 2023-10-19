using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MeleeSmash : Action
{
	[Header("SharedVariable")]
	[SerializeField] private SharedGameObject targetObject;
	[Header("MovementDuaction")]
	[SerializeField] private float attackDuaction;
	[Header("Rotate")]
	[SerializeField] private float rotateSpeed = 1;

	private float timer;

	public override void OnStart()
	{
		timer = Time.time;
	}

	public override TaskStatus OnUpdate()
	{
		_LookAtTarget();
		if(Time.time - timer >= attackDuaction)
		{
			Debug.Log("進戰斬擊");
			return TaskStatus.Success;
		}
		return TaskStatus.Running;
	}

	void _LookAtTarget()
	{
		Quaternion rotation = Quaternion.LookRotation(new Vector3(targetObject.Value.transform.position.x, transform.position.y, targetObject.Value.transform.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
	}
}