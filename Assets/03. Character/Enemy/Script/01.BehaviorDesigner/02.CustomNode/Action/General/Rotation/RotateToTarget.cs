using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RotateToTarget : Action
{
    [Header("SharedVariable")]
    [SerializeField] SharedGameObject targetObject;
    [SerializeField] private SharedGameObject modelObject;

    [Header("Rotate")]
    [SerializeField] private float rotateSpeed = 150;

    [Header("PlayerDetect")]
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float radius = 20;
    [SerializeField] private float angle = 5;

    private Animator animator;

    public override void OnStart()
    {
        AnimationStart();
    }

    public override TaskStatus OnUpdate()
    {
        Rotation();

        if (isLookingAtPlayer())
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    private void Rotation()
    {
        Vector3 targetPosition = new Vector3(targetObject.Value.transform.position.x, transform.position.y, targetObject.Value.transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);

        float angle = Quaternion.Angle(transform.rotation, rotation);

        float maxRotationSpeed = rotateSpeed * Time.deltaTime;
        if (angle > maxRotationSpeed)
        {
            float t = maxRotationSpeed / angle;
            rotation = Quaternion.Slerp(transform.rotation, rotation, t);
        }
        transform.rotation = rotation;
    }

    bool isLookingAtPlayer()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerMask);

        if (rangeChecks.Length != 0)
        {
            foreach (Collider target in rangeChecks)
            {
                Vector3 directionToTarget = (target.gameObject.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void AnimationStart()
    {
        if (modelObject.Value != null)
        {
            animator = modelObject.Value.GetComponent<Animator>();
        }
        if (animator != null)
        {
            animator.SetBool("isMove", true);
        }
    }

    public override void OnEnd()
    {
        if (animator != null)
        {
            animator.SetBool("isMove", false);
        }
    }
}