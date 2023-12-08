using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GroundSlam : Action
{
    [Header("SharedVariable")]
    [SerializeField] private SharedTransform behaviorObject;
    [SerializeField] private SharedGameObject targetObject;

    [Header("AttackObject")]
    [SerializeField] private GameObject groundSlamObject;

    [Header("Movement")]
    [SerializeField] private float forwardForce;

    private Transform groundSlamPoint;
    private Rigidbody rb;

    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();
        groundSlamPoint = behaviorObject.Value.Find("GroundSlamPoint");

        InstantiateAttackObject();
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    private void InstantiateAttackObject()
    {
        GameObject slamCollider = Object.Instantiate(groundSlamObject, groundSlamPoint.position, groundSlamPoint.rotation);
    }

    private void Movement()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
    }
}