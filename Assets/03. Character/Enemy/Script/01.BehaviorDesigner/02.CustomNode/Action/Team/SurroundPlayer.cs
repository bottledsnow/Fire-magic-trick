using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SurroundPlayer : Action
{
    public SharedGameObject targetObject;
    public SharedGameObjectList teammateEnemies;
    public SharedInt surroundingNumber;
    public SharedBool isSurrounding;
    public float moveSpeed;
    public float circleRadius;
    private Rigidbody rb;

    public override void OnStart()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override TaskStatus OnUpdate()
    {
        Vector3 targetPosition = CalculatePositionAroundPlayer(targetObject.Value.transform.position, surroundingNumber.Value);
        Debug.Log(targetPosition);
        Movement(targetPosition);
        return TaskStatus.Success;
    }

    Vector3 CalculatePositionAroundPlayer(Vector3 playerPosition, int index)
    {
        float angle = index * 2 * Mathf.PI / teammateEnemies.Value.Count;
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * circleRadius;

        return playerPosition + offset;
    }

    void Movement(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - rb.position).normalized;

        rb.AddForce(direction *moveSpeed* Time.deltaTime, ForceMode.Impulse);
    }
}
