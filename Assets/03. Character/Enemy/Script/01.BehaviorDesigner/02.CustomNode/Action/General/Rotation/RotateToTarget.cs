using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class RotateToTarget : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedGameObject targetObject;
    [SerializeField] private SharedGameObject modelObject;

    [Header("Rotate")]
    [SerializeField] private float rotateSpeed = 150;

    private Animator animator;

    public override void OnStart()
    {
        AnimationStart();
    }

    public override TaskStatus OnUpdate()
    {
        Rotation();

        if(isLookingAtPlayer())
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
        Vector3 directionToPlayer = targetObject.Value.transform.position - transform.position;
        float dotProduct = Vector3.Dot(transform.forward, directionToPlayer.normalized);

        if(dotProduct > 0.95f)
        {
            return true;
        }
        return false;
    }

    private void AnimationStart()
    {
        if (modelObject != null)
        {
           animator = modelObject.Value.GetComponent<Animator>();
        }
        if (animator != null)
        {
           animator.SetBool("isMove",true);
        }
    }

    public override void OnEnd()
    {
       if(animator != null)
       {
          animator.SetBool("isMove",false);
       }
    }
}