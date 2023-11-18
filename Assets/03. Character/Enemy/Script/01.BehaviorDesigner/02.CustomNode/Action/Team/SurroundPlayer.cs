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
        Movement(targetPosition);
        return TaskStatus.Success;
    }

    Vector3 CalculatePositionAroundPlayer(Vector3 playerPosition, int index)
    {
        index++;
        int count = teammateEnemies.Value.Count + 4;
        float angle = index * 2 * Mathf.PI / count;
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * circleRadius;

        return playerPosition + offset;
    }

    void Movement(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - rb.position).normalized;
        direction.y=0;

        rb.AddForce(direction *moveSpeed* Time.deltaTime, ForceMode.Impulse);
    }
}
