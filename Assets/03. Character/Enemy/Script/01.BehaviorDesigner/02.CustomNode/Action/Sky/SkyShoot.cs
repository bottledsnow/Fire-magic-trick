using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SkyShoot : Action
{
    public float attackDuaction;
    public float rotateSpeed = 5;
    public SharedGameObject targetObject;
	public Transform shootPoint;
    public Vector3 targetPoint;
    private float timer;
	private LineRenderer lineRenderer;

    public override void OnStart()
    {
        timer = Time.time;
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.enabled = true;
    }

    public override TaskStatus OnUpdate()
    {
		if (Time.time - timer <= attackDuaction - 0.5f)
        {
            Debug.Log("射擊蓄力");
			_LookAtTarget();
			targetPoint = targetObject.Value.transform.position;
			lineRenderer.SetPosition(0, shootPoint.position);
			lineRenderer.SetPosition(1, targetPoint);
        }
        if (Time.time - timer >= attackDuaction)
        {
            Debug.Log("空中射擊");
			lineRenderer.enabled = false;
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