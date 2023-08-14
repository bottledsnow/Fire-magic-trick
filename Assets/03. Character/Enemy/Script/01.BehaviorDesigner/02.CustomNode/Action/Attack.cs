using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MeleeSmash : Action
{
	public float attackDuaction;
	public float rotateSpeed = 1;
    public SharedTransform targetTransform;
	public float timer;

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
		Quaternion rotation = Quaternion.LookRotation(new Vector3(targetTransform.Value.position.x, transform.position.y, targetTransform.Value.position.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
	}
}