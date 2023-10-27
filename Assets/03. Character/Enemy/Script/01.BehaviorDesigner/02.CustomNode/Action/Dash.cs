using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Dash : Action
{
    private Rigidbody rb;
    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override TaskStatus OnUpdate()
    {
        float randomDirection = Random.Range(0, 2) == 0 ? -1.0f : 1.0f;

        rb.AddForce(Vector3.right * 30 * randomDirection, ForceMode.Impulse);
        return TaskStatus.Success;
    }
}