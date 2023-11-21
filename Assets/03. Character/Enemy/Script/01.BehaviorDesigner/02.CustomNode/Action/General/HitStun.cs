using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HitStun : Action
{
    [Header("StunTime")]
    [SerializeField] private float minStunTime = 0.2f;

    [Header("GroundCheck")]
    [SerializeField] private float raycastDistance = 0.3f;

    private float timer;

    public override void OnStart()
    {
        timer = Time.time;
    }

    public override TaskStatus OnUpdate()
    {
        if (Time.time - timer > minStunTime && isGrounded())
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
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